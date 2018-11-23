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
        private bool waveCheck = true;

        // Use this for initialization
        void Start () {
            if (instance == null) {
//                DontDestroyOnLoad(gameObject);
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
                    SaveController.instance.saveWave(CurreWaveDetails, CurreWaveDetails.waveNr);
                }
                if (buildAcc >= buildTime) {
                    spawnController.startWave(CurreWaveDetails);
                    nextWaveGenerated = false;
                    waveCheck = true;
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

            // TODO fix this, currently it adds 10-20xp at the beginning of the first wave.
            if (waveOver && waveCheck)
            {
                GameController.instance.addXP(10f);
                waveCheck = false;
            }
        }

        private WaveDetails genNextWave(){
            var nextWaveDetails = Instantiate(CurreWaveDetails);
            nextWaveDetails.spawnDelay = Mathf.Max(nextWaveDetails.spawnDelay - 1, 1); //Todo balancing
            nextWaveDetails.buildTime = nextWaveDetails.buildTime;
            nextWaveDetails.waveScore += 1f;
            nextWaveDetails.waveNr += 1;
            nextWaveGenerated = true;
            return nextWaveDetails;
        }

        private bool isWaveEnded(){
            return spawnController.allEnemiesSpawned() && SpawnController.enemyCounter == 0;
        }

    }
}
