using UnityEngine;

namespace Assets.Scripts.enemies {
    public class MeeleEnemy : Enemy {
        public float damage = 10f;

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
            Collider2D targetCollider = getTarget();
            if (targetCollider != null) {
                if (attackAcc > attackDelay) {
                    anim.SetBool("attacking", true);
                    attackAcc = 0f;
                    targetCollider.gameObject.GetComponent<StructureBlock>().doDamage(damage);
                }
            }
            else {
                anim.SetBool("attacking", false);
                stopMovement = false;
            }

            attackAcc += Time.deltaTime;
        }
    }
}