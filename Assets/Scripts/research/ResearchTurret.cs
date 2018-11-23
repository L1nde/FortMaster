using System.Collections.Generic;
using Assets.Scripts.Turrets;
using UnityEngine;

namespace Assets.Scripts.Research
{

    [CreateAssetMenu(menuName = "ResearchTurret")]
    public class ResearchTurret : ScriptableObject
    {
        public List<ResearchTurret> prerequisites;
        public string researchButtonText;
        public TurretData block;
        public float xpCost = 10f;
    }
}
