using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies
{
    public class RangeEnemy : MonoBehaviour
    {
        public float hp = 100f;
        public float speed = 1f;
        public float attackDelay = 1f;

        private float attackAcc = 0f;
        private Rigidbody2D rb2d;
        public Projectile projectilePrefab;

        // Use this for initialization
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        public void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

            // attacking
            if (attackAcc > attackDelay && GameObject.FindGameObjectWithTag("StructureBlock") != null)
            {
                attackAcc = 0f;
                Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.setTarget(GameObject.FindGameObjectWithTag("StructureBlock").transform.position);
            }

            attackAcc += Time.deltaTime;
        }
    }
}