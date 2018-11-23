using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research
{

    [CreateAssetMenu(menuName = "ResearchBlock")]
    public class ResearchBlock : ScriptableObject
    {
        public List<ResearchBlock> prerequisites;
        public string researchButtonText;
        public StructureBlockData block;
        public float xpCost = 10f;
    }
}
