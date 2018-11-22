using UnityEngine;

namespace Assets.Scripts.enemies.ammo {
    public class Projectile : MonoBehaviour {
        public float shootingAngleOffset = 10f;

        public float damage = 10f;
        public float directionOffset;
        public Vector3 posOffset;

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
            if (!impacted) {
                if (!flying)
                    rb2d.velocity = Vector2.zero;
                else
                    transform.rotation = LookAt2D(rb2d.velocity);
            }
        }

        private Quaternion LookAt2D(Vector2 forward) {
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg - directionOffset);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Terrain") {
                flying = false;
            }

            if (collision.gameObject.tag == "StructureBlock" && !impacted) {
                if (collision.gameObject.GetComponent<Core>() != null) {
                    Core core = collision.gameObject.GetComponent<Core>();
                    if (core.isDead())
                    {
                        UIController.Instance.LoseWave();
                        Destroy(core);
                    }
                    core.doDamage(damage);
                }
                else if (collision.gameObject.GetComponent<StructureBlock>() != null) {
                    StructureBlock structure = collision.gameObject.GetComponent<StructureBlock>();
                    if (structure.isDead())
                        Destroy(structure);
                    structure.doDamage(damage);
                }
            }

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                CreateJoint(rb);
            Destroy(gameObject, 1);
            impacted = true;
        }

        private void CreateJoint(Rigidbody2D rb) {
            FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
            fj.connectedBody = rb.GetComponent<Rigidbody2D>();
        }

        public float calculateAngle(Vector3 target) {
            Vector2 offset = target - transform.position;
            float angle = Mathf.Atan(Mathf.Abs(offset.y / offset.x));
            return Mathf.Clamp(shootingAngleOffset + angle * Mathf.Rad2Deg, 10f, 89f);
        }

        public void shootProjectile(Vector3 target) {
            transform.position += posOffset;
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            float initialAngle = calculateAngle(target) + directionOffset;
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