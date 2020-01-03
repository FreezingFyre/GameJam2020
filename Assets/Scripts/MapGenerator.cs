using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField] Camera mainCamera;

    // Start is called before the first frame update
    void Start() {

        GameObject wall = Resources.Load("Prefabs/Wall") as GameObject;
        GameObject floor = Resources.Load("Prefabs/Floor") as GameObject;
        GameObject paint = Resources.Load("Prefabs/Paint") as GameObject;
        int topBorder = Constants.height + 1;
        int rightBorder = Constants.width + 1;

        // Instantiate the four corners
        Instantiate(wall, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(wall, new Vector3(rightBorder, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(wall, new Vector3(0.0f, topBorder, 0.0f), Quaternion.identity);
        Instantiate(wall, new Vector3(rightBorder, topBorder, 0.0f), Quaternion.identity);

        // Instantiate the blocks at the borders
        for (int i = 1; i < rightBorder; ++i) {
            Instantiate(wall, new Vector3(i, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(wall, new Vector3(i, topBorder, 0.0f), Quaternion.identity);
        }
        for (int i = 1; i < topBorder; ++i) {
            Instantiate(wall, new Vector3(0.0f, i, 0.0f), Quaternion.identity);
            Instantiate(wall, new Vector3(rightBorder, i, 0.0f), Quaternion.identity);
        }

        // Randomly instantiate some obstacle blocks
        for (int x = 1; x < rightBorder; ++x) {
            for (int y = 1; y < topBorder; ++y) {
                if (Random.Range(0.0f, 1.0f) < Constants.autogenWallRate && (x != 1 || y != 1)) {
                    Instantiate(wall, new Vector3(x, y, 0.0f), Quaternion.identity);
                } else {
                    Instantiate(floor, new Vector3(x, y, 0.0f), Quaternion.identity);
                    Paint paintBit = Instantiate(paint, new Vector3(x, y, 0.0f), Quaternion.identity).GetComponent<Paint>();
                    paintBit.Init(Constants.Color.NONE);
                }
            }
        }

        // Need to orient the camera to be able to view the whole playing field
        mainCamera.transform.position = new Vector3((Constants.width + 1) / 2.0f, (Constants.height + 1) / 2.0f, -10.0f);
        float screenRatio = Screen.width * 1.0f / Screen.height;
        float mapRatio = Constants.width * 1.0f / Constants.height;
        if (mapRatio > screenRatio) {
            mainCamera.orthographicSize = ((Constants.width + 2) / 2.0f) / screenRatio;
        } else {
            mainCamera.orthographicSize = (Constants.height + 2) / 2.0f;
        }

        var bomb = Instantiate(Resources.Load("Prefabs/Bomb") as GameObject, new Vector3(5,5,0), Quaternion.identity).GetComponent<Bomb>();
        bomb.Init(Constants.Color.BLUE, 3.0f, 6);

    }

}
