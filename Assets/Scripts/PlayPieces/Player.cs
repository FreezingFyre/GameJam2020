﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : GridMover {

    public Constants.Color color;
    public event EventHandler HealthModified;

    private Vector2Int axisDirection;
    private Vector2Int movingDirection;
    private bool sliding;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    bool facingOverride;
    private int playerNumber;
    private GameObject paintBomb;
    private GameObject paintEmitter;
    private float gunHoldTime;
    private bool gunHolding;
    ParticleSystem.MainModule mainPs;
    private float bombDelay;
    private float health = 100f;

    public float Health { get { return health; } set { } }

    // Start is called before the first frame update
    public override void ChildStart() {
        axisDirection = Vector2Int.zero;
        movingDirection = Vector2Int.zero;
        Animator.SetFloat("MoveModifier", 0f);
        facing = Vector2Int.down;
        sliding = false;
        moveSpeed = Constants.playerSpeed;
        facingOverride = false;
        paintBomb = Resources.Load("Prefabs/Bomb") as GameObject;
        paintEmitter = Resources.Load("Prefabs/PaintEmitter") as GameObject;
        gunHoldTime = 0.0f;
        gunHolding = false;
        mainPs = GetComponent<ParticleSystem>().main;
        GetComponent<ParticleSystem>().Stop();

        var paintColor = Constants.paintColors[MapController.playerCount];
        GetComponent<SpriteRenderer>().color = paintColor;
        color = Constants.ColorToEnum[paintColor];
        bombDelay = Constants.bombDelay;

        MapController.playerCount++;
        playerNumber = MapController.playerCount;
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When the object reaches the cursor
    public override void ReachedCursorAction() {
        if (sliding) {
            ModifyHealth(Constants.paintDamage);
            MoveCursor(movingDirection);
        } else if (axisDirection != Vector2Int.zero) {
            movingDirection = axisDirection;
            Animator.SetFloat("MoveModifier", 1f);
            if (!facingOverride) SetFacing(movingDirection);
            MoveCursor(movingDirection);
        } else {
            movingDirection = Vector2Int.zero;
            Animator.SetFloat("MoveModifier", 0f);
        }
    }

    public override bool CanSpawnWith(GameObject other) {
        return true;
    }

    public override void HandleSpawn(GameObject other) {}

    public override bool HandleCollision(GameObject other, Vector2Int pos) {

        if (other.tag == "Wall") {
            StopSliding();
            return false;
        } else if (other.tag == "Player"){
            other.GetComponent<Player>().StopSliding();
            StopSliding();
            return false;
        } else if (other.tag == "Paint") {

            Paint paint = other.GetComponent<Paint>();

            if (paint.color == color || paint.color == Constants.Color.NONE) {
                StopSliding();
            } else {
                StartSliding(paint.gridPos - gridPos);
            }
            return true;

        } else if (other.tag == "Bomb") {
            StopSliding();
            if (other.GetComponent<Bomb>().color == color) {
                other.GetComponent<Bomb>().Push(pos - gridPos);
            }
            return false;
        } else if (other.tag == "Spike") {
            StopSliding();
            return false;
        } else if (other.tag == "PushEffect") {
            ModifyHealth(Constants.pushDamage);
            StopSliding();
            PushEffect push = other.GetComponent<PushEffect>();
            MoveCursor(push.direction * 2 + (pos - gridPos));
            Destroy(other);
            return true;
        } else {
            return true;
        }

    }

    // OnMove sets the internal notion of which way the joystick is pointing
    public void OnMove(Vector2 input) {
        Vector2 axes = input; 
        if (axes.x != 0.0f || axes.y != 0.0f) {
            if (Mathf.Abs(axes.x) > Mathf.Abs(axes.y)) {
                axisDirection = (axes.x > 0) ? Vector2Int.right : Vector2Int.left;
            } else {
                axisDirection = (axes.y > 0) ? Vector2Int.up : Vector2Int.down;
            }
        } else {
            axisDirection = Vector2Int.zero;
        }
        if (movingDirection == Vector2Int.zero && axisDirection != Vector2Int.zero) {
            movingDirection = axisDirection;
            Animator.SetFloat("MoveModifier", 1f);
            if (!facingOverride) SetFacing(movingDirection);
            MoveCursor(axisDirection);
        }
    }

    public void OnTrap(InputValue input)
    {
        return;
    }

    private void Update() {
        bombDelay += Time.deltaTime;
        if (gunHolding) {
            gunHoldTime += Time.deltaTime;
            int units = Mathf.FloorToInt(gunHoldTime / Constants.secondsPerGunUnit);
            units = Mathf.Min(units, Constants.maxGunUnit);
            mainPs.startSize = 0.1f * units;
            if (units == Constants.maxGunUnit) {
                mainPs.startColor = Constants.EnumToColor[color];
            }
        }
    }

    public void OnShootPress(InputValue input)
    {
        gunHolding = true;
        mainPs.startColor = Color.white;
        mainPs.startSize = 0.0f;
        GetComponent<ParticleSystem>().Play();
    }

    public void OnShootRelease(InputValue input) {
        gunHolding = false;
        GetComponent<ParticleSystem>().Stop();
        int units = Mathf.FloorToInt(gunHoldTime / Constants.secondsPerGunUnit);
        units = Mathf.Min(units, Constants.maxGunUnit);
        if (units > 0) {
            PaintEmitter emitter = Instantiate(paintEmitter, (Vector2)(gridPos + facing), Quaternion.identity).GetComponent<PaintEmitter>();
            emitter.Init(color, units, facing, Constants.bombPaintEmitterSpeed);
        }
        gunHoldTime = 0.0f;
    }

    public void OnLook(InputValue input) {
        Vector2Int direction = AxesToDirection(input);
        facingOverride = (direction != Vector2Int.zero);
        if (facingOverride) {
            SetFacing(direction);
        }
    }

    public void OnStoppedLook(InputValue input) {
        if (movingDirection == Vector2Int.zero && !facingOverride) {
            Vector2Int direction = AxesToDirection(input);
            SetFacing(direction);
        }
    }

    public void OnBomb(InputValue input) {
        if (bombDelay >= Constants.bombDelay) {
            bombDelay = 0.0f;
            Bomb bomb = Instantiate(paintBomb, (Vector2)(gridPos + facing), Quaternion.identity).GetComponent<Bomb>();
            bomb.Init(color, Constants.bombFuse, Constants.bombDistance);
        }
    }

    Vector2Int AxesToDirection(InputValue input) {
        Vector2 axes = input.Get<Vector2>();
        if (axes.x != 0.0f || axes.y != 0.0f) {
            if (Mathf.Abs(axes.x) > Mathf.Abs(axes.y)) {
                return (axes.x > 0) ? Vector2Int.right : Vector2Int.left;
            } else {
                return (axes.y > 0) ? Vector2Int.up : Vector2Int.down;
            }
        } else {
            return Vector2Int.zero;
        }
    }

    public void SetFacing(Vector2Int direction) {
        if (direction == Vector2Int.right) {
            SpriteRenderer.flipX = true;
            Animator.Play("Move_Horizontal");
        } else if (direction == Vector2Int.left) {
            SpriteRenderer.flipX = false;
            Animator.Play("Move_Horizontal");
        } else if (direction == Vector2Int.up) {
            Animator.Play("Move_Up");
        } else if (direction == Vector2Int.down) {
            Animator.Play("Move_Down");
        }
        if (direction != Vector2Int.zero) {
            facing = direction;

        }
    }

    public void StartSliding(Vector2Int newDirection) {
        if (!sliding) {
            sliding = true;
            moveSpeed = Constants.playerSlidingSpeed;
            movingDirection = newDirection;
            if (!facingOverride) Animator.SetFloat("MoveModifier", 1f);
            MoveCursor(movingDirection);
        }
    }

    public void StopSliding() {
        if (sliding) {
            sliding = false;
            moveSpeed = Constants.playerSpeed;
            movingDirection = Vector2Int.zero;
            Animator.SetFloat("MoveModifier", 0f);
        }
    }

    

    public void ModifyHealth(float delta)
    {
        if (health == 0)
        {
            return;
        }
        if (health + delta > 100) health = 100;
        else if (health + delta <= 0)
        {
            health = 0;
            
        }
        else health += delta;

        HealthModified?.Invoke(this, EventArgs.Empty);
        if (health == 0) {
            --PlayController.TotalPlayers;
            Destroy(gameObject);
            if (PlayController.TotalPlayers == 1)
            {
                StartCoroutine(GameEnd());
            }
        }
    }

    IEnumerator GameEnd() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

}
