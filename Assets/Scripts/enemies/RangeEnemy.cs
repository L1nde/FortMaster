using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {
    public class RangeEnemy : Enemy {
        public Projectile projectilePrefab;
        public float accuracySpread = 1;

        // Use this for initialization
        new void Start() {
            base.Start();
        }

        // Update is called once per frame
        new void Update() {
            base.Update();
            attack();
        }

        public override void attack() {
            // attacking
            if (!attackEnabled)
                return;

            Collider2D targetCollider = getTarget();
            if (targetCollider != null) {
                if (attackAcc > attackDelay) {
                    anim.SetBool("attacking", true);
                    attackAcc = 0f;
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
//                    projectile.setTarget(new Vector2(targetCollider.gameObject.transform.position.x + Random.Range(-1.0f, 1.0f) * accuracySpread, targetCollider.gameObject.transform.position.y));
                    projectile.shootProjectile(new Vector2(targetCollider.gameObject.transform.position.x, targetCollider.gameObject.transform.position.y) + Random.insideUnitCircle * accuracySpread);
                }

                stopMovement = true;
            }
            else {
                anim.SetBool("attacking", false);
                stopMovement = false;
            }


            attackAcc += Time.deltaTime;
        }
    }
}