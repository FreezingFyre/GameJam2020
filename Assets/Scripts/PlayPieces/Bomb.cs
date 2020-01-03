using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : GridMover {

    // Member variables
    public Constants.Color color;
    private float fuse;
    private int size;
    private float currTime;
    private GameObject paintEmitter;
    private GameObject pushEffect;

    public Vector2Int sliding;

    // Called by GridMover on object creation
    public override void ChildStart() {}

    // When this object is destroyed
    public override void ChildOnDestroy() {}

    // When this object reaches the cursor
    public override void ReachedCursorAction() {
        if (currTime >= fuse) {
            Detonate();
        } else if (sliding != Vector2Int.zero) {
            MoveCursor(sliding);
        }
    }

    // Whether this object can spawn within other
    public override bool CanSpawnWith(GameObject other) {
        return (other.tag == "Paint" || other.tag == "PushEffect" || other.tag == "PaintEmitter");
    }

    // What to do when this object spawns within other
    public override void HandleSpawn(GameObject other) {
        if (other.tag == "PushEffect") {
            // TODO: Modify sliding direction
        }
    }

    // What to do when this object collides with other
    public override bool HandleCollision(GameObject other, Vector2Int pos) {
        if (other.tag == "Wall" || other.tag == "Player" || other.tag == "Bomb") {
            sliding = Vector2Int.zero;
            return false;
        } else if (other.tag == "Paint") {
            if (other.GetComponent<Paint>().color != color) {
                sliding = Vector2Int.zero;
            }
        }
        // TODO: Implement collisions
        return true;
    }

    public void Push(Vector2Int direction) {
        if (MapController.PosColor(gridPos) == color) {
            sliding = direction;
            MoveCursor(direction);
        }
    }

    // Initializes this PaintBomb's variables after construction
    public void Init(Constants.Color color_, float fuse_, int size_) {
        color = color_;
        fuse = fuse_;
        size = size_;
        currTime = 0;
        moveSpeed = Constants.bombSlidingSpeed;

        GetComponent<SpriteRenderer>().color = Constants.EnumToColor[color];

        paintEmitter = Resources.Load("Prefabs/PaintEmitter") as GameObject;
        pushEffect = Resources.Load("Prefabs/PushEffect") as GameObject;
        sliding = Vector2Int.zero;
    }

    // Update is called once per frame
    void Update() {
        currTime += Time.deltaTime;
        if (Stopped() && currTime >= fuse) {
            Detonate();
        }
    }

    // Detonates the bomb under different circumstances
    private void Detonate() {
        Vector2Int[] directions = new[] { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
        foreach (Vector2Int direction in directions) {
            PaintEmitter emitter = Instantiate(paintEmitter, (Vector2)gridPos, Quaternion.identity).GetComponent<PaintEmitter>();
            emitter.Init(color, size, direction, Constants.bombPaintEmitterSpeed);
            PushEffect push = Instantiate(pushEffect, (Vector2)gridPos, Quaternion.identity).GetComponent<PushEffect>();
            push.Init(color, size, direction, Constants.bombPushEffectSpeed);
        }
        Destroy(gameObject);
    }

}
