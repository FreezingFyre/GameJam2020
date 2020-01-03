using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GridMover {

    // Called by GridMover on object creation
    public override void ChildStart() {
        moveSpeed = 0.0f;
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When this object reaches the cursor
    public override void ReachedCursorAction() {}

    // Whether this object can spawn within other
    public override bool CanSpawnWith(GameObject other) {
        return false;
    }

    // What to do when this object spawns within other
    public override void HandleSpawn(GameObject other) {}

    // What to do when this object collides with other
    public override bool HandleCollision(GameObject other, Vector2Int pos) {
        return false;
    }

}
