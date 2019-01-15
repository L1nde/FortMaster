using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {

    [CreateAssetMenu(menuName = "StructureBlock")]
    public class StructureBlockData : ScriptableObject {
        public String name;
        public int cost;
        public float hp;
        public Sprite sprite;
        public float jointBreakTorque;
        public AudioClipGroup placeSoundGroup;
        

    }
}
