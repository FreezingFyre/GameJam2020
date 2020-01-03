using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : GridMover {

    public Constants.Color color;

    // Called by GridMover on object creation
    public override void ChildStart() { }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When this object reaches the cursor
    public override void ReachedCursorAction() {}

    // Whether this object can spawn within other
    public override bool CanSpawnWith(GameObject other) {
        return true;
    }

    // What to do when this object spawns within other
    public override void HandleSpawn(GameObject other) {
        if (other.tag == "Paint") {
            Destroy(other);
        }
    }

    // What to do when this object collides with other
    public override bool HandleCollision(GameObject other) {
        return false;
    }

    // Initializes this PaintBomb's variables after construction
    public void Init(Constants.Color color_) {
        color = color_;
        if (color == Constants.Color.NONE) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f); ;
        }
    }

}
