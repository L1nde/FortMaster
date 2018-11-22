using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
        public AnimatorController aniController;
    }
}
