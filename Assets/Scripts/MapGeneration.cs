using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour {

    // Width and height of the texture in pixels.
    // -30 x 30
    // -17 y -7
    // Map generation is based on algorithm described in:
    // https://blogs.unity3d.com/2018/05/29/procedural-patterns-you-can-use-with-tilemaps-part-i/
    public int minX = -30;
    public int maxX = 30;
    public int minY = -17;
    public int maxY = -7;

    public Tilemap tilemap;
    public List<TileBase> tilebase = new List<TileBase>();

    public void Start()
    {
        int width = Mathf.Abs((minX - maxX));
        int height = Mathf.Abs((minY - maxY));
        int[,] emptyMap = generateArray(width, height);
        int[,] generatedMap = randomizeMap(emptyMap, 1, 10);
        renderMap(generatedMap);
    }

    public int[,] generateArray(int width, int height)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x, y] = 0;
            }
        }
        return map;
    }

    public void renderMap(int[,] map)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x, y] == 1)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[0]);
                else if (map[x, y] == 2)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[1]);
                else if (map[x, y] == 3)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[2]);
            }
        }
    }

    public int[,] randomizeMap(int[,] map, float seed, int minSectionWidth)
    {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Determine the start position
        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth)
            {
                map[x, lastHeight] = 2;
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth)
            {
                map[x, lastHeight] = 3;
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }

        //Return the modified map
        return map;
    }
}
