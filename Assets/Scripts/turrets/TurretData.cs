using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Turrets
{

    [CreateAssetMenu(menuName = "Turret")]
    public class TurretData : ScriptableObject
    {
        public String name;
        public int cost;
        public float reloadTime;
        public float attackRange;
        public float minxRange;
        public PlayerProjectile projectile;
        public RuntimeAnimatorController aniController;
        public AudioClipGroup fireSound;
        public float dps { get { if (projectile != null) { return projectile.damage / reloadTime; } else { return 0; } } }
        public float DpsPerCost { get { if (cost != 0) { return dps / cost; } else { return -1; } } }
    }
}
