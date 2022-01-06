using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

    public GameObject cube;
    public List<GameObject> cubes;
    public int height;
    public int width;

    private float interval;

    void Awake()
    {
        interval = TileManager.interval;
    }

	// Use this for initialization
	void Start () {
		for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cubes.Add(Instantiate(cube, new Vector3(i * interval, 0f, j * interval), Quaternion.identity));
            }
        }
        //for (int i = 0; i < cubes.Count; i++)
        //{
        //    GameObject c = cubes[0];
        //    Destroy(c);
        //    cubes.Remove(c);
        //}
	}
}
