using System.Collections.Generic;

namespace Assets.Scripts.saving {

    [System.Serializable]
    public class GameSaveObject {
        public float xp;
        public List<string> researchedItems;

        public GameSaveObject(List<string> researchedItems, float xp) {
            this.researchedItems = researchedItems;
            this.xp = xp;
        }
    }
}