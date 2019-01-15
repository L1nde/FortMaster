using UnityEngine;

namespace Assets.Scripts.enemies.ammo {
    public class Projectile : MonoBehaviour {
        public float shootingAngleOffset = 10f;

        public float damage = 10f;
        public Vector3 posOffset;
        public bool parabola;
        public float speed;
        public float deathDelay;
        public bool affectedByGravity;

        protected Rigidbody2D rb2d;

        private bool flying = true;
        private bool impacted = false;

        // Use this for initialization
        void Start() {
            rb2d = GetComponent<Rigidbody2D>();
            if (!affectedByGravity)
                rb2d.gravityScale = 0;
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
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg - shootingAngleOffset);
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
            Destroy(gameObject, deathDelay);
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
            if (!affectedByGravity)
                shootStraight(target);
            else if (parabola)
                shootWithParabola(target);
            else
                shootWithMinParabola(target);
        }

        private void shootStraight(Vector3 target) {
            float dirToTarget = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            Vector3 dirV3 = new Vector3(0, 0, dirToTarget);
            Quaternion dir = Quaternion.Euler(dirV3);
            body.transform.rotation = dir;
            body.velocity = new Vector3(Mathf.Cos(dirToTarget) * speed, Mathf.Sin(dirToTarget) * speed);
            transform.rotation = dir;
        }


        private void shootWithMinParabola(Vector3 target) {
            float x = target.x - transform.position.x;
            float y = target.y + 0.5f - transform.position.y; //the 0.5 is small adjustment so the turrets would aim a bit higher

            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();
            float ySpeed = speed * y / x + Physics.gravity.magnitude * x / (2 * speed);

            Vector3 velVector = new Vector3(speed, ySpeed);
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.AddForce(velVector * body.mass, ForceMode2D.Impulse);

            transform.rotation = LookAt2D(body.velocity);

        }

        private void shootWithParabola(Vector3 target) {
            transform.position += transform.rotation * posOffset;
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