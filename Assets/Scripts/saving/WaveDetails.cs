using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.waves {
    [System.Serializable]
    public class WaveDetails {
        public float waveScore;
        public float spawnDelay;
        public float buildTime;
        public int waveNr;
        public float gold;
        public List<PlaceableSaveObject> fortObjects;
        public TerrainGenObject terrainGenObject;
        public float xpEarned;


        public WaveDetails(float buildTime, List<PlaceableSaveObject> fortObjects, float gold, float spawnDelay, int waveNr, float waveScore) {
            this.buildTime = buildTime;
            this.fortObjects = fortObjects;
            this.gold = gold;
            this.spawnDelay = spawnDelay;
            this.waveNr = waveNr;
            this.waveScore = waveScore;
        }
    }
}
