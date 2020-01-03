using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintEmitter : GridMover {

    // Member variables
    private Constants.Color color;
    private int distance;
    private Vector2Int direction;
    private GameObject paint;

    // Called by GridMover on object creation
    public override void ChildStart() {
        SummonPaint();
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When this object reaches the cursor
    public override void ReachedCursorAction() {
        SummonPaint();
    }

    // Whether this object can spawn within other
    public override bool CanSpawnWith(GameObject other) {
        return other.tag != "Wall";
    }

    // What to do when this object spawns within other
    public override void HandleSpawn(GameObject other) {}

    // What to do when this object collides with other
    public override bool HandleCollision(GameObject other, Vector2Int pos) {
        return other.tag != "Wall";
    }

    // Initializes this PaintBomb's variables after construction
    public void Init(Constants.Color color_, int size_, Vector2Int direction_, float speed_) {
        color = color_;
        distance = size_;
        direction = direction_;
        moveSpeed = speed_;
        paint = Resources.Load("Prefabs/Paint") as GameObject;
    }

    // Summonds paint at the emitter's current location
    private void SummonPaint() {

        // Summon the paint of the appropriate color
        Paint paintBit = Instantiate(paint, (Vector2)gridPos, Quaternion.identity).GetComponent<Paint>();
        paintBit.Init(color);

        // If there's more distance to go, move the cursor again
        --distance;
        if (distance > 0) {
            MoveCursor(direction);
        } else {
            Destroy(gameObject);
        }

    }

}
