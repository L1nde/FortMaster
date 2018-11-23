using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.enemies;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
    private float spawnDelay = 1f;
    private float spawnAcc = 0f;
    

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() { 
        if (spawnAcc < spawnDelay) {
            spawnAcc += Time.deltaTime;
        }
    }

    public bool ready() {
        return spawnAcc >= spawnDelay;
    }


    public bool spawn(Enemy enemy) {
        if (spawnAcc >= spawnDelay) {
            this.spawnAcc = 0f;
            Instantiate(enemy, transform.position, transform.rotation, transform);
            return true;
        }
        return false;
    }

    public void setUp(WaveDetails wave) {
        this.spawnDelay = wave.spawnDelay;
    }
}