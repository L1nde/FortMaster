using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.waves;

namespace Assets.Scripts {
    [System.Serializable]
    public class WaveSaveObject {

        public List<KeyValuePair<string, float>> enemies = new List<KeyValuePair<string, float>>();
        public float spawnDelay = 5;
        public float buildTime = 30f;

        public WaveSaveObject(WaveDetails waveDetails) {
            this.spawnDelay = waveDetails.spawnDelay;
            this.buildTime = waveDetails.buildTime;
            foreach (var waveEnemy in waveDetails.enemies) {
                enemies.Add(waveEnemy.ToPair());
            }
        }
    }
}
