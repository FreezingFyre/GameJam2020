using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField] Vector2Int size;
    [SerializeField] float obstacleRate;
    [SerializeField] Camera mainCamera;

    private List<GameObject>[,] objects;
    private Dictionary<GameObject, Vector2Int> positions;

    // Generates the game board; this is only for testing purposes!
    void Generate() {

        GameObject border = Resources.Load("Prefabs/Border") as GameObject;
        GameObject floor = Resources.Load("Prefabs/Floor") as GameObject;
        int topBorder = size.y + 1;
        int rightBorder = size.x + 1;

        // Instantiate the four corners
        Instantiate(border, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(rightBorder, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(0.0f, topBorder, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(rightBorder, topBorder, 0.0f), Quaternion.identity);

        // Instantiate the blocks at the borders
        for (int i = 1; i < rightBorder; ++i) {
            Instantiate(border, new Vector3(i, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(border, new Vector3(i, topBorder, 0.0f), Quaternion.identity);
        }
        for (int i = 1; i < topBorder; ++i) {
            Instantiate(border, new Vector3(0.0f, i, 0.0f), Quaternion.identity);
            Instantiate(border, new Vector3(rightBorder, i, 0.0f), Quaternion.identity);
        }

        // Randomly instantiate some obstacle blocks
        for (int x = 1; x < rightBorder; ++x) {
            for (int y = 1; y < topBorder; ++y) {
                if (Random.Range(0.0f, 1.0f) < obstacleRate && (x != 1 || y != 1)) {
                    Instantiate(border, new Vector3(x, y, 0.0f), Quaternion.identity);
                } else {
                    Instantiate(floor, new Vector3(x, y, 0.0f), Quaternion.identity);
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start() {

        // Only for testing purposes!
        Generate();

        objects = new List<GameObject>[size.x + 2, size.y + 2];
        positions = new Dictionary<GameObject, Vector2Int>();

        // Need to orient the camera to be able to view the whole playing field
        mainCamera.transform.position = new Vector3((size.x + 1) / 2.0f, (size.y + 1) / 2.0f, -10.0f);
        float screenRatio = Screen.width * 1.0f / Screen.height;
        float mapRatio = size.x * 1.0f / size.y;
        if (mapRatio > screenRatio) {
            mainCamera.orthographicSize = ((size.x + 2) / 2.0f) / screenRatio;
        } else {
            mainCamera.orthographicSize = (size.y + 2) / 2.0f;
        }

        // Need to loop through the playable area and find the walls
        Collider2D collision;
        Vector2Int probe = new Vector2Int();
        for (int x = 0; x <= size.x + 1; ++x) {
            probe.x = x;
            for (int y = 0; y <= size.y + 1; ++y) {
                probe.y = y;
                objects[x, y] = new List<GameObject>();
                if (collision = Physics2D.OverlapPoint(probe, LayerMask.GetMask("Map"))) {
                    RegisterObject(collision.gameObject, probe);
                }
            }
        }

    }

    // Registers the given GameObject with the coordinate manager so that collisions
    // and whatnot can be dealt with properly
    // Returns true if the object can be spawned there, false otherwise
    public bool RegisterObject(GameObject obj, Vector2Int pos) {

        // If we have a GridCollider, need to make sure that we are allowed to spawn here
        GridCollider collider;
        if ((collider = obj.GetComponent<GridCollider>()) != null) {

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

    // Used to set a new position for the given GameObject, with delta being
    // a unit vector in a direction
    // Returns true if the position can be changed to there, false otherwise
    public bool MoveObject(GameObject obj, Vector2Int delta) {

        Vector2Int currentPos = positions[obj];
        Vector2Int nextPos = currentPos + delta;

        bool canMove = true;
        GridCollider collider = obj.GetComponent<GridCollider>();
        for (int i = 0; i < objects[nextPos.x, nextPos.y].Count; ++i) {
            canMove &= collider.HandleCollision(objects[nextPos.x, nextPos.y][i]);
        }

        if (canMove) {
            objects[currentPos.x, currentPos.y].Remove(obj);
            objects[nextPos.x, nextPos.y].Add(obj);
            positions[obj] = nextPos;
        }

        return canMove;

    }

}
