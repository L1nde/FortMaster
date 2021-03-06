﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour {
    public static MapGeneration instance;

    // Width and height of the texture in pixels.
    // Map generation is based on algorithm described in:
    // https://blogs.unity3d.com/2018/05/29/procedural-patterns-you-can-use-with-tilemaps-part-i/
    public int minX = -30;
    public int maxX = 30;
    public int minY = -17;
    public int maxY = -7;

    public Tilemap tilemap;
    public List<TileBase> tilebase = new List<TileBase>();

    public Camera cam;
    public GameObject fortBase;

    public int seed;

    private int[] coordsCore = new int[2];

    public void Start() {
        coordsCore[0] = -1;
        coordsCore[1] = -1;
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        if (GameController.CurrentWaveDetails.terrainGenObject != null) {
            var terrainGenObject = GameController.CurrentWaveDetails.terrainGenObject;
            seed = terrainGenObject.seed;
            minX = terrainGenObject.minX;
            maxX = terrainGenObject.maxX;
            minY = terrainGenObject.minY;
            maxY = terrainGenObject.maxY;
        }
        else {
            // Generate seed
            seed = generateSeed();
        }


        // Generate map
        generateMap(this.seed);


        fortBase.GetComponentInChildren<Core>().gameObject.transform.position = new Vector3(coordsCore[0], coordsCore[1]);
    }

    public int generateSeed() {
        System.Random rnd = new System.Random();
        return rnd.Next(0, 100000);
    }

    public void generateMap(int seed) {
        this.seed = seed;
        // Calculate width and height
        int width = Mathf.Abs((minX - maxX));
        int height = Mathf.Abs((minY - maxY));

        // Generate and render map
        int[,] emptyMap = generateArray(width, height);
        int[,] generatedMap = randomizeMap(emptyMap, seed, 5);
        renderMap(generatedMap);
    }

    private int[,] generateArray(int width, int height) {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                map[x, y] = 0;
            }
        }

        return map;
    }

    private void renderMap(int[,] map) {
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (map[x, y] == 1)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[0]);
                else if (map[x, y] == 2)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[1]);
                else if (map[x, y] == 3)
                    tilemap.SetTile(new Vector3Int(minX + x, minY + y, 0), tilebase[2]);
            }
        }
    }

    private int[,] randomizeMap(int[,] map, int seed, int minSectionWidth) {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());
        Random.InitState(seed + 100);

        //Determine the start position
        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        bool lastPlus = false;
        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++) {
            //Determine the next move
            nextMove = rand.Next(2);
            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth) {
                map[x, lastHeight] = 2;
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth) {
                map[x, lastHeight] = 3;
                lastHeight++;
                sectionWidth = 0;
                lastPlus = true;
            }

            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--) {
                map[x, y] = 1;
            }

            if (coordsCore[0] == -1 && coordsCore[1] == -1)
            {
                coordsCore[0] = minX + x + 1;
                coordsCore[1] = minY + lastHeight + 2;
            }
            else
            {
                Vector3 temp = cam.WorldToViewportPoint(new Vector3(coordsCore[0], coordsCore[1], 0));
                if (temp.x > 1 || temp.x < 0 || temp.y > 1 || temp.y < 0)
                {
                    coordsCore[0] = minX + x + 1;
                    coordsCore[1] = minY + lastHeight + 2;
                }
            }

            if (sectionWidth == 1 && lastPlus)
            {
                map[x, lastHeight] = 3;
            }
            lastPlus = false;
        }

        //Return the modified map
        return map;
    }
}