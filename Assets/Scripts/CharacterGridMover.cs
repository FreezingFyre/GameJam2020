using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterGridMover : GridMover
{

    private Direction axisDirection;
    private Direction prevDirection;
    private bool canMove;

    // Start is called before the first frame update
    public override void ChildStart() {
        axisDirection = Direction.None;
        prevDirection = Direction.None;
        canMove = true;
    }

    // When the object reaches the cursor
    public override void ReachedCursorAction() {
        if (axisDirection != Direction.None) {
            canMove = !MoveCursor(directions[axisDirection]);
            prevDirection = canMove ? Direction.None : axisDirection;
        } else {
            canMove = true;
            prevDirection = Direction.None;
        }
    }

    // OnMove sets the internal notion of which way the joystick is pointing
    void OnMove(InputValue input) {
        Vector2 axes = input.Get<Vector2>();
        if (axes.x != 0.0f || axes.y != 0.0f) {
            if (Mathf.Abs(axes.x) > Mathf.Abs(axes.y)) {
                axisDirection = (axes.x > 0) ? Direction.Right : Direction.Left;
            } else {
                axisDirection = (axes.y > 0) ? Direction.Up : Direction.Down;
            }
        } else {
            axisDirection = Direction.None;
        }
        if ((canMove || axisDirection == opposite[prevDirection]) && axisDirection != Direction.None) {
            canMove = !MoveCursor(directions[axisDirection]);
            prevDirection = canMove ? Direction.None : axisDirection;
        }
    }

}
