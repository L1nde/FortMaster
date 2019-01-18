using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.waves {

    [CreateAssetMenu(menuName = "Wave")]
    public class BaseWaveDetails : ScriptableObject {
        public float waveScore;
        public float spawnDelay = 5;
        public float buildTime = 30f;
        public int waveNr = 0;
        public float gold;

        public WaveDetails ToWaveDetails() {
            return new WaveDetails(buildTime, null, gold, spawnDelay, waveNr, waveScore);
        }
    }
}
