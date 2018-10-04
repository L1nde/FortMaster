using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies
{
    public abstract class Enemy : MonoBehaviour {

        public float hp = 100f;
        public float speed = 1f;
        public float attackDelay = 1f;
        public float attackRange = 10f;

        protected CircleCollider2D attackRangeCollider;
        protected float attackAcc = 0f;
        protected Rigidbody2D rb2d;
        protected Animator anim;



        // Use this for initialization
        protected void Start()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            foreach (var child in GetComponentsInChildren<CircleCollider2D>())
            {
                if (child.gameObject.tag == "AttackRange")
                {
                    attackRangeCollider = child;
                    attackRangeCollider.radius = attackRange;
                    break;
                }
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            move();
            

            
        }

        protected Collider2D getTarget()
        {
            Collider2D[] colliders = new Collider2D[10];
            int count = attackRangeCollider.GetContacts(colliders);
            if (count != 0)
            {
                return colliders[0];
            }
            else
            {
                return null;
            }
        }

        protected void move(){
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }

        public abstract void attack();
    }
}
