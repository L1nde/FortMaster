using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies
{
    public class RangeEnemy : MonoBehaviour
    {
        public float hp = 100f;
        public float speed = 1f;
        public float attackDelay = 1f;
        public float attackRange = 1f;
        public float radius = 10f;
        public Projectile projectilePrefab;

        private CircleCollider2D attackRangeCollider;
        private float attackAcc = 0f;
        private Rigidbody2D rb2d;
        private Animator anim;
        


        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            foreach (var child in GetComponentsInChildren<CircleCollider2D>())
            {
                if (child.gameObject.tag == "AttackRange")
                {
                    attackRangeCollider = child;
                    attackRangeCollider.radius = radius;
                    break;
                }
            }
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
            if (attackAcc > attackDelay)
            {
                Transform target = getTarget();
                if (target != null)
                {
                    anim.SetBool("attacking", true);
                    attackAcc = 0f;
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    projectile.setTarget(target.position);
                }
                else
                {
                    anim.SetBool("attacking", false);
                }
                Debug.Log(target);
               
            }
            

            attackAcc += Time.deltaTime;
        }

        private Transform getTarget()
        {
            Collider2D[] colliders = new Collider2D[10];
            int count = attackRangeCollider.GetContacts(colliders);
            if (count != 0)
            {
                return colliders[0].gameObject.transform;
            }
            else
            {
                return null;
            }
        }
    }
}