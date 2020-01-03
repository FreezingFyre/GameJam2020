using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : GridMover {

    public Constants.Color color;

    private Vector2Int axisDirection;
    private Vector2Int movingDirection;
    private bool sliding;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    bool facingOverride;
    private int playerNumber;
    private GameObject paintBomb;

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

        var paintColor = Constants.paintColors[MapController.playerCount];
        GetComponent<SpriteRenderer>().color = paintColor;
        color = Constants.ColorToEnum[paintColor];

        MapController.playerCount++;
        playerNumber = MapController.playerCount;
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When the object reaches the cursor
    public override void ReachedCursorAction() {
        if (sliding) {
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

    public override bool HandleCollision(GameObject other) {

        if (other.tag == "Wall" || other.tag == "Player") {
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
            return false;
        } else if (other.tag == "Spike") {
            StopSliding();
            return false;
        } else if (other.tag == "PushEffect") {
            StopSliding();
            PushEffect push = other.GetComponent<PushEffect>();
            MoveCursor(push.direction * 2);
            return false;
        } else {
            return true;
        }

    }

    // OnMove sets the internal notion of which way the joystick is pointing
    public void OnMove(InputValue input) {
        Vector2 axes = input.Get<Vector2>();
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

    public void OnShoot(InputValue input)
    {
        return;
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
        Bomb bomb = Instantiate(paintBomb, (Vector2)(gridPos + facing), Quaternion.identity).GetComponent<Bomb>();
        bomb.Init(color, Constants.bombFuse, Constants.bombDistance);
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

}
