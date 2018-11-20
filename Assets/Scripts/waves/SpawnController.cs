using UnityEngine;

namespace Assets.Scripts.waves {
    public class SpawnController : MonoBehaviour {

        private EnemySpawn[] spawns;
        private float buildTime = 30f;
        private float buildAcc;
        private WaveDetails currentWave;

        // Use this for initialization
        void Start () {
            currentWave = WaveController.CurreWaveDetails;
            spawns = GetComponentsInChildren<EnemySpawn>();
            buildTime = currentWave.buildTime;
        }
	
        // Update is called once per frame
        void Update () {
            if (EnemySpawn.waveEnd) {
                if (buildAcc >= buildTime) {
                    nextWave();
                }
                else {
                    buildAcc += Time.deltaTime;
                    UIController.Instance.showCountdown();
                    UIController.Instance.updateCountdown(buildTime - buildAcc);
                }               
            }
            else {
                UIController.Instance.hideCountdown();
            }
        }

        private void nextWave() {
            foreach (var spawn in spawns) {
                spawn.startWave(Instantiate(currentWave));
            }

            buildAcc = 0f;
        }
    }
}
