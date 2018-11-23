using UnityEngine;

namespace Assets.Scripts.waves {

    [System.Serializable]
    public class PlaceableSaveObject {
        public string placeableName;
        public float x;
        public float y;
        public float z;


        public PlaceableSaveObject(string placeableName, Vector3 position) {
            this.placeableName = placeableName;
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }
    }
}