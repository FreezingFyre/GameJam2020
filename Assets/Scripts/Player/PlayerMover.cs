using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : GridMover {

    private Vector2Int axisDirection;
    private bool canMove;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;

    // Start is called before the first frame update
    public override void ChildStart() {
        canMove = true;
        moveSpeed = Constants.playerSpeed;
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
            Animator.SetFloat("Modifier", 1f);
            if (Mathf.Abs(axes.x) > Mathf.Abs(axes.y)) {
                axisDirection = (axes.x > 0) ? Vector2Int.right : Vector2Int.left;
                Animator.Play("Player_Move_Horizontal");
                SpriteRenderer.flipX = (axes.x > 0) ? true : false;

            } else {
                axisDirection = (axes.y > 0) ? Vector2Int.up : Vector2Int.down;
                if (axisDirection == Vector2Int.up)
                {
                    Animator.Play("Player_Move_Up");
                }
                else
                {
                    Animator.Play("Player_Move_Down");
                }
            }
        } else {
            axisDirection = Vector2Int.zero;
            Animator.SetFloat("Modifier", 0f);
        }
        if (canMove && axisDirection != Vector2Int.zero) {
            canMove = false;
            MoveCursor(axisDirection);
        }
    }

}
