using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Level level;
    public int width;
    public int height;
    public Tile[,] tiles2;
    public float einterval;
    public static float interval;
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    public LayerMask tileLayer;
    [Header("GameManager stuff")]
    public LayerMask receiverLayer;
    private GameObject satelite;

    public void Awake()
    {
        interval = einterval;
    }

    public void Start()
    {
        tiles2 = new Tile[height, width];
        satelite = new GameObject("Satelite");
        satelite.transform.position = Vector3.up * 2;
        ScanForTiles();
    }

    //YUCK. Each tile should have instead reported own position and property to the tile manager
    void ScanForTiles()
    {
        satelite.transform.parent = transform;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                satelite.transform.localPosition = new Vector3(i * interval, 50f, j * interval);
                RaycastHit rayHit;
                if (Physics.Raycast(satelite.transform.position, Vector3.down, out rayHit, Mathf.Infinity, tileLayer))
                {
                    if (rayHit.transform.GetComponent<Tile>() != null)
                    {
                        tiles2[i, j] = rayHit.transform.GetComponent<Tile>();
                        tiles2[i, j].i = i;
                        tiles2[i, j].j = j;
                        if (tiles2[i, j].goal)
                        {
                            level.maxGoals++;
                        }
                    } else
                    {
                        print("NOOO");
                    }
                }
            }
        }
    }

    public Tile GetAdjacentTile(Tile fromTile, Vector3 direction)
    {
        int i =  fromTile.i + (int)direction.x;
        int j =  fromTile.j + (int)direction.z;
        if (i < 0 || j < 0 || i >= height || j >= width)
        {
            return null;
        }
        else
        {
            return tiles2[i, j];
        }
    }

    //public Tile GetAdjacentTile2(Tile fromTile, Vector3 direction)
    //{
    //    Tile foundTile = fromTile;
    //    int increment = 0;
    //    if (direction == Vector3.right)
    //    {
    //        increment = height;
    //    }
    //    if (direction == Vector3.left)
    //    {
    //        increment = -height;
    //    }
    //    if (direction == Vector3.forward)
    //    {
    //        increment = 1;
    //    }
    //    if (direction == Vector3.back)
    //    {
    //        increment = -1;
    //    }
    //    int target = fromTile.tileNumber + increment;
    //    if (target > tiles.Length || target < 0)
    //        return null;
    //    else
    //    {
    //        return tiles[target];
    //    }
    //}

    public Tile FindLastUnoccupiedTile(Tile initialTile, Vector3 direction)
    {
        Tile foundTile = initialTile;
        int iIncrement = (int) direction.x;
        int jIncrement = (int) direction.z;
        //prevent accidental infinite loops
        int iTarget = foundTile.i + iIncrement;
        int jTarget = foundTile.j + jIncrement;
        int count = 0;
        while (iTarget >=0 && jTarget >=0 && iTarget < height && jTarget <width && !tiles2[iTarget, jTarget].occupied && count < 50)
        {
            foundTile = tiles2[iTarget, jTarget];
            iTarget += iIncrement;
            jTarget += jIncrement;
            count++;
        }
        return foundTile;
    }

    //public Tile FindLastUnoccupiedTile2(Tile initialTile, Vector3 direction)
    //{
    //    Tile foundTile = initialTile;
    //    int increment = 0;
    //    print(direction);
    //    if (direction == Vector3.right)
    //    {
    //        increment = height;
    //    }
    //    if (direction == Vector3.left)
    //    {
    //        increment = -height;
    //    }
    //    if (direction == Vector3.forward)
    //    {
    //        increment = 1;
    //    }
    //    if (direction == Vector3.back)
    //    {
    //        increment = -1;
    //    }
    //    int targetIndex = foundTile.tileNumber + increment;
    //    //prevent accidental infinite loops
    //    int count = 0;
    //    while (targetIndex >= 0 && targetIndex < tiles.Length && tiles[targetIndex] != null && !tiles[targetIndex].occupied && count < 50)
    //    {
    //        foundTile = tiles[targetIndex];
    //        targetIndex += increment;
    //        count++;
    //    }
    //    print("increment = " + increment + " from index = " + initialTile.tileNumber + " targetIndex = " + targetIndex + " count = " + count);

    //    return foundTile;
    //}
}
