using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    [CreateAssetMenu(menuName = "ResearchItem")]
    public class ResearchItem : ScriptableObject
    {
        public List<ResearchItem> prerequisites;
        public string buttonText;
        public Placeable block;
        public float xpCost = 10f;
        public float newCost = -1;
    }
}
