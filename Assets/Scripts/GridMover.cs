﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridMover : MonoBehaviour, GridCollider {

    // Members inherited from GridCollider that need to be implemented in the child,
    // along with new handlers for this object
    public abstract void ChildStart();
    public abstract void ReachedCursorAction();
    public abstract bool CanSpawnWith(GameObject other);
    public abstract void HandleSpawn(GameObject other);
    public abstract bool HandleCollision(GameObject other);

    // Data members
    protected Vector2Int facing;
    protected Vector2Int gridPos;
    protected float moveSpeed;

    // Used for movement
    private Rigidbody2D rigidBody;
    private Vector2Int cursor;
    private MapController controller;

    [SerializeField] GameObject mapController;

    // Called when the object is created
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        cursor = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        gridPos = cursor;
        controller = mapController.GetComponent<MapController>();
        controller.RegisterObject(gameObject, cursor);
        ChildStart();
    }

    // Updates the position of the object to move towards the cursor
    void FixedUpdate() {

        // First case is if we're aligned with the gridPos and needn't move,
        // we simply do nothing
        if (Aligned() && cursor == gridPos) {
            return;
        }

        // If we're aligned right now, we need to set a new grid position to move to
        if (Aligned()) {
            Vector2Int delta = (cursor - gridPos);
            if (delta.x != 0) {
                delta.x = (int)Mathf.Sign(delta.x);
            } else {
                delta.y = (int)Mathf.Sign(delta.y);
            }
            if (!controller.MoveObject(gameObject, delta)) {
                cursor = gridPos;
                ReachedCursorAction();
                return;
            }
            gridPos += delta;
        }

        // Step to the grid position
        Vector2 realPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 remainingDelta = gridPos - realPos;
        Vector2 moveDelta = moveSpeed * Time.deltaTime * remainingDelta.normalized;
        if (moveDelta.magnitude < remainingDelta.magnitude) {
            rigidBody.MovePosition(realPos + moveDelta);
        } else {
            rigidBody.MovePosition(gridPos);
            ReachedCursorAction();
        }

    }

    // Updates the location of the cursor by transforming it the given distance
    public void MoveCursor(Vector2Int delta) {
        cursor = gridPos;
        cursor += delta;
    }

    // Returns true if the real position is equal to gridPos
    private bool Aligned() {
        Vector3 gridPosFloat = new Vector3(gridPos.x, gridPos.y, 0.0f);
        return transform.position.Equals(gridPosFloat);
    }

}
