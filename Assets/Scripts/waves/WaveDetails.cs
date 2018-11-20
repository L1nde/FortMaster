using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.waves {

    [CreateAssetMenu(menuName = "Wave")]
    public class WaveDetails : ScriptableObject {
        public List<WaveEnemy> enemies;
        public float spawnDelay = 5;
        public float buildTime = 30f;
    }
}
