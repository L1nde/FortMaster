﻿using Assets.Scripts.enemies.ammo;
using UnityEngine;

namespace Assets.Scripts.enemies {
    public class RangeEnemy : Enemy {
        public Projectile projectilePrefab;
        public float accuracySpread = 1;

        // Use this for initialization
        new void Start() {
            base.Start();
            Vector3 scale = gameObject.transform.localScale;
            if (scale.x > 0 && gameObject.name.Contains("Soldier"))
                scale.x *= -1;
            transform.transform.localScale = scale;
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
                    // projectile.setTarget(new Vector2(targetCollider.gameObject.transform.position.x + Random.Range(-1.0f, 1.0f) * accuracySpread, targetCollider.gameObject.transform.position.y));
                    applyDmgTrait(projectile);
                    projectile.shootProjectile(new Vector2(targetCollider.gameObject.transform.position.x, targetCollider.gameObject.transform.position.y) + Random.insideUnitCircle * accuracySpread);
                    if (attackSound != null) {
                        attackSound.play();
                    } 
                }

                stopMovement = true;
            }
            else {
                anim.SetBool("attacking", false);
                stopMovement = false;
            }


            attackAcc += Time.deltaTime;
        }

        public override void attachData(EnemyData data) {
            RangeEnemyData rangeEnemyData = (RangeEnemyData) data;
            accuracySpread = rangeEnemyData.AccuracySpread;
            projectilePrefab = rangeEnemyData.ProjectilePrefab;
            hp = rangeEnemyData.hp;
            speed = rangeEnemyData.speed;
            attackDelay = rangeEnemyData.attackDelay;
            attackRange = rangeEnemyData.attackRange;
            moneyOnDeath = rangeEnemyData.moneyOnDeath;
            attackSound = rangeEnemyData.attackSound;
        }

        private void applyDmgTrait(Projectile p) {
            DmgTrait t = GameController.instance.GetDmgTrait();
            if (t.isToggled)
                p.damage *= t.dmgmodifier;
        }
    }
}