using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.waves {

    [CreateAssetMenu(menuName = "Wave")]
    public class WaveDetails : ScriptableObject {
        public float waveScore;
        public float spawnDelay = 5;
        public float buildTime = 30f;
        public int waveNr = 0;
        public float gold;

        public WaveSaveObject ToWaveSaveObject() {
            return new WaveSaveObject(this);
        }

        public WaveDetails ToWaveDetails(WaveSaveObject waveSaveObject) {
            waveScore = waveSaveObject.waveScore;
            spawnDelay = waveSaveObject.spawnDelay;
            buildTime = waveSaveObject.buildTime;
            waveNr = waveSaveObject.waveNr;
            return this;
        }
    }
}
