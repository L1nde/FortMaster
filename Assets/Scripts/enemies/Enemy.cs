using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {
    public abstract class Enemy : MonoBehaviour {
        public float hp = 100;
        public float speed = 1f;
        public float attackDelay = 1f;
        public float attackRange = 10f;
        public float moneyOnDeath;

        protected CircleCollider2D attackRangeCollider;
        protected float attackAcc = 0f;
        protected Rigidbody2D rb2d;
        protected Animator anim;
        protected bool stopMovement = false;

        protected bool attackEnabled = false;

        // Use this for initialization
        protected void Start() {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            attackEnabled = checkIfInView(gameObject.transform.position);
            foreach (var child in GetComponentsInChildren<CircleCollider2D>()) {
                if (child.gameObject.tag == "AttackRange") {
                    attackRangeCollider = child;
                    attackRangeCollider.radius = attackRange;
                    break;
                }
            }
        }

        // Update is called once per frame
        protected void Update() {
            if (hp <= 0) {
                Destroy(gameObject);
            }
            attackEnabled = checkIfInView(gameObject.transform.position);
            move();
        }

        private bool checkIfInView(Vector3 position)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }

        private void OnDestroy() {
            GameController.instance.addGold(moneyOnDeath);
        }

        protected Collider2D getTarget() {
            Collider2D[] colliders = new Collider2D[10];
            int count = attackRangeCollider.GetContacts(colliders);
            if (count != 0) {
                return colliders[0];
            }
            else {
                return null;
            }
        }

        
        protected void move() {
            if (!stopMovement) {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            }
            else {
                rb2d.velocity = new Vector2(0, 0);
//                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.tag == "StructureBlock") {
                stopMovement = true;
            }
        }

        public abstract void attack();

        public void doDamage(float damage) {
            hp -= damage;
        }
    }


}