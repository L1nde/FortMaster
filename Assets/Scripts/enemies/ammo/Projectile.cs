using UnityEngine;

namespace Assets.Scripts.enemies.ammo {
    public class Projectile : MonoBehaviour {
        public float shootingAngleOffset = 10f;
        
        public float damage = 10f;

        protected Rigidbody2D rb2d;

        private bool flying = true;
        private bool impacted = false;

        // Use this for initialization
        void Start() {
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            // if arrow got new target

            if (!flying) {
                rb2d.velocity = Vector2.zero;
            }
            else {
                transform.rotation = LookAt2D(rb2d.velocity);
            }


            Destroy(gameObject, 10f);
        }

        private Quaternion LookAt2D(Vector2 forward) {
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
            
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.tag == "Terrain") {
                flying = false;
            }

            if (collision.gameObject.tag == "StructureBlock" && !impacted) {
                collision.gameObject.GetComponent<StructureBlock>().doDamage(damage);
            }

            impacted = true;

//            gameObject.AddComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            
        }

        public float calculateAngle(Vector3 target) {
            Vector2 offset = target - transform.position;
            float angle = Mathf.Atan(Mathf.Abs(offset.y / offset.x));
            return Mathf.Clamp(shootingAngleOffset + angle * Mathf.Rad2Deg, 10f, 89f);

        }

        public void shootProjectile(Vector3 target) {
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            float initialAngle = calculateAngle(target);
            float gravity = Physics.gravity.magnitude;
            // Selected angle in radians
            float angle = initialAngle * Mathf.Deg2Rad;


            // Planar distance between objects
            float distance = Mathf.Abs(target.x - transform.position.x);
            // Distance along the y axis between objects
            float yOffset = transform.position.y - target.y;

            float initialVelocity = (1f / Mathf.Cos(angle)) *
                                    Mathf.Sqrt((0.5f * gravity * 1f * Mathf.Pow(distance, 2f)) /
                                               (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.Angle(Vector3.forward, target - transform.position);
            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
            if (target.x - transform.position.x < 0f) {
                finalVelocity = new Vector3(-finalVelocity.x, finalVelocity.y);
            }
            body.AddForce(finalVelocity * body.mass, ForceMode2D.Impulse);

            transform.rotation = LookAt2D(body.velocity);
        }
    }
}