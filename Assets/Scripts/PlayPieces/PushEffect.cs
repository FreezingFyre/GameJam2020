using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : GridMover {

    // Member variables
    private int distance;
    public Vector2Int direction;

    // Called by GridMover on object creation
    public override void ChildStart() {
        MoveCursor(direction * distance);
    }

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When this object reaches the cursor
    public override void ReachedCursorAction() {
        Destroy(gameObject);
    }

    // Whether this object can spawn within other
    public override bool CanSpawnWith(GameObject other) {
        return (other.tag == "Paint" || other.tag == "Bomb" || other.tag == "PushEffect" || other.tag == "PaintEmitter");
    }

    // What to do when this object spawns within other
    public override void HandleSpawn(GameObject other) {}

    // What to do when this object collides with other
    public override bool HandleCollision(GameObject other, Vector2Int pos) {

        if (other.tag == "Wall") {
            return false;
        } else if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            player.StopSliding();
            player.MoveCursor(direction * 2);
            return false;
        } else if (other.tag == "Bomb") {
            other.GetComponent<Bomb>().Push(direction);
            return false;
        } else if (other.tag == "Spike") {

            return false;
        }

        return true;

    }

    // Initializes this PaintBomb's variables after construction
    public void Init(Constants.Color color_, int size_, Vector2Int direction_, float speed_) {
        distance = size_ - 1;
        direction = direction_;
        moveSpeed = speed_;
    }

}
