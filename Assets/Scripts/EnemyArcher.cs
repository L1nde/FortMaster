using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyArcher : MonoBehaviour
    {
        public float hp = 100f;
        public float speed = 1f;

        private Rigidbody2D rb2d;

        private bool facingRight = true;

        public float attackDelay = 1f;
        private float attackAcc = 0f;
        public Arrow arrowPreFab;

        // Use this for initialization
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

            // attacking
            if (attackAcc > attackDelay)
            {
                attackAcc = 0f;
                Instantiate(arrowPreFab, transform.position, transform.rotation);
            }

            attackAcc += Time.deltaTime;
        }


        // Flips the sprite
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}