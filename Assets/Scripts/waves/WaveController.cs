using UnityEngine;
using Assets.Scripts.enemies;
using System.Collections.Generic;

namespace Assets.Scripts.waves {
    public class WaveController : MonoBehaviour {
        public static WaveDetails CurreWaveDetails;
        public static WaveController instance;
        public static bool waveOver = true;
        public SpawnController spawnController;
        private float buildTime = 30f;
        private float buildAcc;
        private bool nextWaveGenerated = true;

        // Use this for initialization
        void Start () {
            if (instance == null) {
                DontDestroyOnLoad(gameObject);
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }

        void OnEnable(){
            buildTime = CurreWaveDetails.buildTime;
            buildAcc = 0f;
        }

        void Update(){
            waveOver = isWaveEnded();
            if (waveOver) {
                if (!nextWaveGenerated){
                    CurreWaveDetails = genNextWave();
                    SaveController.instance.saveWave(CurreWaveDetails);
                }
                if (buildAcc >= buildTime) {
                    spawnController.startWave(CurreWaveDetails);
                    nextWaveGenerated = false;
                }
                else {
                    buildAcc += Time.deltaTime;
                    UIController.Instance.showCountdown();
                    UIController.Instance.updateCountdown(buildTime - buildAcc);
                }               
            }
            else {
                UIController.Instance.hideCountdown();
                buildTime = CurreWaveDetails.buildTime;
                buildAcc = 0f;
            }
        }

        private WaveDetails genNextWave(){
            var nextWaveDetails = Instantiate(CurreWaveDetails);
            nextWaveDetails.spawnDelay = Mathf.Max(nextWaveDetails.spawnDelay - 1, 1); //Todo
            var lastWaveEnemies = CurreWaveDetails.enemies;
            var nextWaveEnemies = new List<WaveEnemy>();
            foreach (var item in lastWaveEnemies)
            {
                nextWaveEnemies.Add(new WaveEnemy(item.enemy, item.count + 1));
            }
            nextWaveDetails.enemies = nextWaveEnemies;
            nextWaveGenerated = true;
            return nextWaveDetails;
        }

        private bool isWaveEnded(){
            return spawnController.allEnemiesSpawned() && EnemySpawn.enemyCounter == 0;
        }

    }
}
