using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterGridMover : GridMover {

    private Vector2Int axisDirection;
    private bool canMove;

    // Start is called before the first frame update
    public override void ChildStart() {
        canMove = true;
        moveSpeed = 4.0f;
    }

    // When the object reaches the cursor
    public override void ReachedCursorAction() {
        if (axisDirection != Vector2Int.zero) {
            canMove = false;
            MoveCursor(axisDirection);
        } else {
            canMove = true;
        }
    }

    public override bool CanSpawnWith(GameObject other) {
        return true;
    }
    public override void HandleSpawn(GameObject other) {

    }
    public override bool HandleCollision(GameObject other) {
        return false;
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
        if (canMove && axisDirection != Vector2Int.zero) {
            canMove = false;
            Debug.Log("Moving " + axisDirection);
            MoveCursor(axisDirection);
        }
    }

}
