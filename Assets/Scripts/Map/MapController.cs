﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapController {

    public static int playerCount = 0;
    private static List<GameObject>[,] objects;
    private static Dictionary<GameObject, Vector2Int> positions;

    // Constructor to initialize lists
    static MapController() {
        objects = new List<GameObject>[Constants.width + 2, Constants.height + 2];
        positions = new Dictionary<GameObject, Vector2Int>();
        for (int x = 0; x <= Constants.width + 1; ++x) {
            for (int y = 0; y <= Constants.height + 1; ++y) {
                objects[x, y] = new List<GameObject>();
            }
        }
    }

    // Registers the given GameObject with the coordinate manager so that collisions
    // and whatnot can be dealt with properly
    // Returns true if the object can be spawned there, false otherwise
    public static bool RegisterObject(GameObject obj, Vector2Int pos) {

        // If we have a GridCollider, need to make sure that we are allowed to spawn here
        GridMover collider;
        if ((collider = obj.GetComponent<GridMover>()) != null) {

            // Determine if we can even spawn here
            for (int i = 0; i < objects[pos.x, pos.y].Count; ++i) {
                if (!collider.CanSpawnWith(objects[pos.x, pos.y][i])) {
                    return false;
                }
            }

            // Handle the spawn collisions
            for (int i = 0; i < objects[pos.x, pos.y].Count; ++i) {
                collider.HandleSpawn(objects[pos.x, pos.y][i]);
            }

        }

        // Register the object
        objects[pos.x, pos.y].Add(obj);
        positions[obj] = pos;
        return true;

    }

    // Removes the given object from being managed by the MapController
    public static void DeregisterObject(GameObject obj) {
        if (positions.ContainsKey(obj))
        {
            objects[positions[obj].x, positions[obj].y].Remove(obj);
            positions.Remove(obj);
        }
    }

    // Used to set a new position for the given GameObject, with delta being
    // a unit vector in a direction
    // Returns true if the position can be changed to there, false otherwise
    public static bool MoveObject(GameObject obj, Vector2Int delta) {

        Vector2Int currentPos = positions[obj];
        Vector2Int nextPos = currentPos + delta;

        bool canMove = true;
        GridMover collider = obj.GetComponent<GridMover>();
        for (int i = 0; i < objects[nextPos.x, nextPos.y].Count; ++i) {
            canMove &= collider.HandleCollision(objects[nextPos.x, nextPos.y][i], nextPos);
        }

        if (canMove) {
            objects[currentPos.x, currentPos.y].Remove(obj);
            objects[nextPos.x, nextPos.y].Add(obj);
            positions[obj] = nextPos;
        }

        return canMove;

    }

    public static Constants.Color PosColor(Vector2Int pos) {
        Constants.Color color = Constants.Color.NONE;
        foreach (GameObject obj in objects[pos.x, pos.y]) {
            Paint onTile = obj.GetComponent<Paint>();
            if (onTile != null) {
                color = onTile.color;
                break;
            }
        }
        return color;
    }

}
