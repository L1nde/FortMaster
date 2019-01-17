using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research {
    [CreateAssetMenu(menuName = "Research/Base")]
    public class ResearchItem : ScriptableObject {
        public List<ResearchItem> prerequisites;
        public string researchName;
        public float xpCost = 10f;
        public bool researched = false;

        private HashSet<ResearchItem> childs = new HashSet<ResearchItem>();

        public void OnEnable() {
            foreach (var item in prerequisites) {
                item.addChild(this);
               
            }
        }

        public void addChild(ResearchItem item) {
            childs.Add(item);
        }

        public HashSet<ResearchItem> getChilds() {
            return childs;
        }
    }
}