using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : GridMover {

    public Constants.Color color = Constants.Color.BROWN;

    private Vector2Int axisDirection;
    private Vector2Int movingDirection;
    private bool sliding;

    // Start is called before the first frame update
    public override void ChildStart() {
        axisDirection = Vector2Int.zero;
        movingDirection = Vector2Int.zero;
        sliding = false;
        moveSpeed = Constants.playerSpeed;
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When the object reaches the cursor
    public override void ReachedCursorAction() {
        if (sliding) {
            MoveCursor(movingDirection);
        } else if (axisDirection != Vector2Int.zero) {
            movingDirection = axisDirection;
            MoveCursor(movingDirection);
        } else {
            movingDirection = Vector2Int.zero;
        }
    }

    public override bool CanSpawnWith(GameObject other) {
        return false;
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
    void OnMove(InputValue input) {
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
            MoveCursor(axisDirection);
        }
    }

    public void StartSliding(Vector2Int newDirection) {
        if (!sliding) {
            sliding = true;
            moveSpeed = Constants.playerSlidingSpeed;
            movingDirection = newDirection;
            MoveCursor(movingDirection);
        }
    }

    public void StopSliding() {
        if (sliding) {
            sliding = false;
            moveSpeed = Constants.playerSpeed;
            movingDirection = Vector2Int.zero;
        }
    }

}
