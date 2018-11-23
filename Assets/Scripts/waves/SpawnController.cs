using System.Collections.Generic;
using Assets.Scripts.enemies;
using Boo.Lang.Runtime;
using UnityEngine;

namespace Assets.Scripts.waves {
    public class SpawnController : MonoBehaviour {
        public static int enemyCounter;
        public EnemyData[] enemiesData;

        private EnemySpawn[] spawns;
        private float currentWaveScore = 0f;
        private bool started = false;
        private int waveNr;
        private float maxScore;


        // Use this for initialization
        void Start () {
            spawns = GetComponentsInChildren<EnemySpawn>();
        
        }
	
        // Update is called once per frame
        void Update () {
            if (started) {
                foreach (var spawn in spawns) {
                    if (spawn.ready()) {
                        int i = getRandomWeightedInt();
                        if (i == -1)
                            return;
                        enemiesData[i].enemyPrefab.attachData(enemiesData[i]);
                        spawn.spawn(enemiesData[i].enemyPrefab);
                        currentWaveScore += enemiesData[i].enemyScore;
                    }
                }
            }
        }

        public bool allEnemiesSpawned(){
            return !started;
        }

        private float calculateScoreSum() {
            float scoreSum = 0f;
            foreach (var enemyData in enemiesData) {
                if (enemyData.minLevel >= waveNr && maxScore - currentWaveScore >= enemyData.enemyScore) {
                    scoreSum += enemyData.enemyScore;
                } 
            }
            return scoreSum;
        }

        private int getRandomWeightedInt() {
            float scoreSum = calculateScoreSum();
            float r = Random.value;
            float s = 0f;
            for (var i = 0; i < enemiesData.Length; i++) {
                var enemyData = enemiesData[i];
                if (enemyData.minLevel >= waveNr && maxScore - currentWaveScore >= enemyData.enemyScore) {
                    s += 1 - enemyData.enemyScore / scoreSum;
                    if (s >= r) {
                        return i;
                    }
                }
            }

            started = false;
            return -1;
        }

        public void startWave(WaveDetails waveDetails) {
            started = true;
            currentWaveScore = 0f;
            waveNr = waveDetails.waveNr;
            maxScore = waveDetails.waveScore;
            calculateScoreSum();
            foreach (var spawn in spawns) {
                spawn.setUp(waveDetails);
            }
        }

        
        
    }
}
