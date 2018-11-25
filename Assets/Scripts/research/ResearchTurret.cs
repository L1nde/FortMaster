using System.Collections.Generic;
using Assets.Scripts.Turrets;
using UnityEngine;

namespace Assets.Scripts.Research {
    [CreateAssetMenu(menuName = "Research/Turret")]
    public class ResearchTurret : ResearchItem {
        public TurretData block;
    }
}