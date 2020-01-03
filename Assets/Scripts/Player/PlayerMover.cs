using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : GridMover {

    private int playerNumber;
    private Constants.Color color;
    private Vector2Int axisDirection;
    private bool canMove;
    private GameObject paint;
    private GameObject paintBomb;

    // Start is called before the first frame update
    public override void ChildStart() {
        canMove = true;
        moveSpeed = Constants.playerSpeed;
        paint = Resources.Load("Prefabs/Paint") as GameObject;
        paintBomb = Resources.Load("Prefabs/PaintBomb") as GameObject;

        var paintColor = Constants.paintColors[MapController.playerCount];
        GetComponent<SpriteRenderer>().color = paintColor;
        this.color = Constants.ColorToEnum[paintColor];

        MapController.playerCount++;
        playerNumber = MapController.playerCount;
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
    void OnMainPMove(InputValue input) {
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
            MoveCursor(axisDirection);
        }
    }

    void OnMainPSquirt(InputValue input)
    {

    }

    void OnMainPBomb(InputValue input)
    {
        PaintBomb bomb = Instantiate(paintBomb, transform.position, Quaternion.identity).GetComponent<PaintBomb>();
        bomb.DelayStart(paint, 2, .05f, 4);
    }

}
