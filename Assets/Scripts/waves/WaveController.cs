using UnityEngine;
using Assets.Scripts.enemies;
using System.Collections.Generic;
using Assets.Scripts.saving;

namespace Assets.Scripts.waves {
    public class WaveController : MonoBehaviour {
        public static WaveController instance;
        public static bool waveOver = true;
        public SpawnController spawnController;
        public GameObject fortBase;


        private float buildTime = 30f;
        private float buildAcc;
        private bool nextWaveGenerated = true;
        private bool waveCheck = true;
        private WaveDetails currentWaveDetails;


        // Use this for initialization
        void Start() {
            if (instance == null) {
                instance = this;
            }
            else if (instance != this) {
                Destroy(gameObject);
            }
            GameController.instance.setUpWave();
        }

        void OnEnable() {
            currentWaveDetails = GameController.CurrentWaveDetails;
            buildTime = currentWaveDetails.buildTime;
            buildAcc = 0f;
        }

        void Update() {
            waveOver = isWaveEnded();
            if (waveOver) {
                if (!nextWaveGenerated) {
                    currentWaveDetails = genNextWave();
                    saveWave();
                    GameController.instance.saveData();
                }

                if (buildAcc >= buildTime) {
                    spawnController.startWave(currentWaveDetails);
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
                buildTime = currentWaveDetails.buildTime;
                buildAcc = 0f;
            }

            // Todo this doesn't belong to this class
            // TODO fix this, currently it adds 10-20xp at the beginning of the first wave.
            if (waveOver && waveCheck) {
                float wavexp = 10;
                foreach (Trait trait in GameController.instance.getAllTraits()) {
                    if (trait.isToggled)
                        wavexp *= trait.xpmodifier;
                }
                GameController.instance.addXP(wavexp);
                waveCheck = false;
            }
        }

        private void saveWave() {
            // Wave saving
            List<PlaceableSaveObject> placeableSaveObjects = new List<PlaceableSaveObject>();
            foreach (var block in fortBase.GetComponentsInChildren<Placeable>()) {
                placeableSaveObjects.Add(new PlaceableSaveObject(block.name, block.transform.position, block.transform.eulerAngles));
            }

            currentWaveDetails.fortObjects = placeableSaveObjects;
            currentWaveDetails.terrainGenObject = new TerrainGenObject(MapGeneration.instance.maxX, MapGeneration.instance.maxY, MapGeneration.instance.minX, MapGeneration.instance.minY, MapGeneration.instance.seed);
            currentWaveDetails.gold = GameController.instance.gold;
            SaveController.instance.saveWave(currentWaveDetails, currentWaveDetails.waveNr);
        }

        private WaveDetails genNextWave() {
            var nextWaveDetails = new WaveDetails(currentWaveDetails.buildTime,null, 0, Mathf.Max(currentWaveDetails.spawnDelay - 1, 1), currentWaveDetails.waveNr + 1, currentWaveDetails.waveScore + 1);
            //Todo balancing
            nextWaveGenerated = true;
            return nextWaveDetails;
        }

        private bool isWaveEnded() {
            return spawnController.allEnemiesSpawned() && SpawnController.enemyCounter == 0;
        }
    }
}