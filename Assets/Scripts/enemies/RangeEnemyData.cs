using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {

    [CreateAssetMenu(menuName = "Enemy/Range")]
    public class RangeEnemyData : EnemyData {
        public Projectile ProjectilePrefab;
        public float AccuracySpread = 1;
    }
}
