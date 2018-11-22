using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research
{

    [CreateAssetMenu(menuName = "ResearchPlaceable")]
    public class ResearchPlaceable : ScriptableObject
    {
        public List<ResearchPlaceable> prerequisites;
        public string researchButtonText;
        public string buildButtonText;
        public Placeable block;
        public float xpCost = 10f;
    }
}
