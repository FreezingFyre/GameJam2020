using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] Vector2Int mapSize;
    [SerializeField] Camera camera;

    private GameObject border;
    private GameObject floor;

    // Start is called before the first frame update
    void Start() {

        border = Resources.Load("Prefabs/Border") as GameObject;
        floor = Resources.Load("Prefabs/Floor") as GameObject;

        int topBorder = mapSize.y + 1;
        int rightBorder = mapSize.x + 1;

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
                if (Random.Range(0.0f, 1.0f) < 0.1f && (x != 1 && y != 1)) {
                    Instantiate(border, new Vector3(x, y, 0.0f), Quaternion.identity);
                } else {
                    Instantiate(floor, new Vector3(x, y, 0.0f), Quaternion.identity);
                }
            }
        }

        // Need to orient the camera to be able to view the whole playing field
        camera.transform.position = new Vector3(rightBorder / 2.0f, topBorder / 2.0f, -10.0f);
        float screenRatio = Screen.width * 1.0f / Screen.height;
        float mapRatio = mapSize.x * 1.0f / mapSize.y;
        if (mapRatio > screenRatio) {
            camera.orthographicSize = ((mapSize.x + 2) / 2.0f) / screenRatio;
        } else {
            camera.orthographicSize = (mapSize.y + 2) / 2.0f;
        }

    }

}
