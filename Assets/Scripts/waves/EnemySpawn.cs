using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
    public static int enemyCounter;
    public static bool waveEnd = true;

    private float spawnDelay = 1f;
    private bool started = false;

    private float spawnAcc = 0f;
    private List<WaveEnemy> enemies;
    

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (started) {
            if (spawnAcc > spawnDelay) {
                if (enemies.Count == 0) {
                    started = false;
                    return;
                }
                spawnAcc = 0f;
                int i = Random.Range(0, enemies.Count);
                Instantiate(enemies[i].enemy, transform.position, transform.rotation, transform);
                enemies[i].count--;
                if (enemies[i].count <= 0) {
                    enemies.RemoveAt(i);
                }
            }

            spawnAcc += Time.deltaTime;
        }
        else {
            if (enemyCounter == 0) {
                waveEnd = true;
            }
        }
    }

    public void startWave(WaveDetails wave) {
        this.enemies = wave.enemies;
        this.spawnAcc = 0f;
        this.started = true;
        this.spawnDelay = wave.spawnDelay;
        waveEnd = false;
    }
}