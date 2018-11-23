using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {

    [CreateAssetMenu(menuName = "Enemy")]
    public class EnemyData : ScriptableObject {
        public float hp = 100;
        public float speed = 1f;
        public float attackDelay = 1f;
        public float attackRange = 10f;
        public float moneyOnDeath;
        public float enemyScore;
        public int minLevel;

        public Enemy enemyPrefab;




    }
}
