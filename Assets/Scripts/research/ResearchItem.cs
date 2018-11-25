using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research {
    public class ResearchItem : ScriptableObject {
        public List<ResearchItem> prerequisites;
        public string researchName;
        public float xpCost = 10f;
    }
}