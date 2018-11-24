using Assets.Scripts.saving;
using UnityEngine;

namespace Assets.Scripts.waves {

    [System.Serializable]
    public class PlaceableSaveObject {
        public string placeableName;
        public Vec3 position;
        public Vec3 rotation;
        


        public PlaceableSaveObject(string placeableName, Vector3 position, Vector3 rotation) {
            this.placeableName = placeableName;
            this.position = new Vec3(position);
            this.rotation = new Vec3(rotation);
        }
    }
}