using UnityEngine;

namespace Assets.Scripts.enemies {
    public class MeeleEnemy : Enemy {
        public float damage = 10f;

        // Use this for initialization
        new void Start() {
            base.Start();
            applyDmgTrait();
        }

        // Update is called once per frame
        new void Update() {
            base.Update();
            attack();   
        }

        public override void attack() {
            if (!attackEnabled)
                return;

            Collider2D targetCollider = getTarget();
            if (targetCollider != null) {
                if (attackAcc > attackDelay) {
                    anim.SetBool("attacking", true);
                    attackAcc = 0f;
                    targetCollider.gameObject.GetComponent<StructureBlock>().doDamage(damage);
                    if (attackSound != null) {
                        attackSound.play();
                    }
                }
            }
            else {
                anim.SetBool("attacking", false);
                stopMovement = false;
            }

            attackAcc += Time.deltaTime;
        }

        public override void attachData(EnemyData data) {
            MeeleEnemyData meeleEnemyData = (MeeleEnemyData)data;
            damage = meeleEnemyData.meeleDamage;
            hp = meeleEnemyData.hp;
            speed = meeleEnemyData.speed;
            attackDelay = meeleEnemyData.attackDelay;
            attackRange = meeleEnemyData.attackRange;
            moneyOnDeath = meeleEnemyData.moneyOnDeath;
            attackSound = meeleEnemyData.attackSound;

        }

        private void applyDmgTrait() {
            DmgTrait t = GameController.instance.GetDmgTrait();
            if (t.isToggled)
                damage *= t.dmgmodifier;
        }
    }
}