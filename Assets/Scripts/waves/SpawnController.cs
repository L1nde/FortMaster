using UnityEngine;

namespace Assets.Scripts.waves {
    public class SpawnController : MonoBehaviour {

        private EnemySpawn[] spawns;

        // Use this for initialization
        void Start () {
            spawns = GetComponentsInChildren<EnemySpawn>();
        
        }
	
        // Update is called once per frame
        void Update () {
            
        }

        public bool allEnemiesSpawned(){
            foreach (var item in spawns)
            {
                if (!item.ended()){
                    return false;
                }
            }
            return true;
        }

        public void startWave(WaveDetails waveDetails) {
            if (UIController.Instance.researchScreen.activeSelf)
                UIController.Instance.researchScreen.SetActive(false);
            foreach (var spawn in spawns) {
                spawn.startWave(Instantiate(waveDetails));
            }
        }
    }
}
