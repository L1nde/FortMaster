using System.Collections.Generic;

namespace Assets.Scripts.saving {

    [System.Serializable]
    public class GameSaveObject {
        public float xp;
        public List<string> researchedTree;

        public GameSaveObject(List<string> researchedTree, float xp) {
            this.researchedTree = researchedTree;
            this.xp = xp;
        }
    }
}