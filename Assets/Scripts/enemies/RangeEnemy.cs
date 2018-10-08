using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {
    public class RangeEnemy : Enemy {
        public Projectile projectilePrefab;

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
            Collider2D targetCollider = getTarget();
            if (targetCollider != null) {
                if (attackAcc > attackDelay) {
                    anim.SetBool("attacking", true);
                    attackAcc = 0f;
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    projectile.setTarget(targetCollider.gameObject.transform.position);
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