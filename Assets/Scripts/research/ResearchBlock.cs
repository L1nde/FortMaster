using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research {
    [CreateAssetMenu(menuName = "Research/Block")]
    public class ResearchBlock : ResearchItem {
        public StructureBlockData block;
    }
}