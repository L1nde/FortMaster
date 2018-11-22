using UnityEngine;

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
                    // Todo
                }
                if (buildAcc >= buildTime) {
                    spawnController.startWave(CurreWaveDetails);
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

        private void genNextWave(){

        }

        private bool isWaveEnded(){
            return spawnController.allEnemiesSpawned() && EnemySpawn.enemyCounter == 0;
        }

    }
}
