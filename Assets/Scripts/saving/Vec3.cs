using UnityEngine;

namespace Assets.Scripts.saving {

    [System.Serializable]
    public class Vec3 {
        private float x;
        private float y;
        private float z;

        public Vec3(float z, float x, float y) {
            this.z = z;
            this.x = x;
            this.y = y;
        }

        public Vec3(Vector3 vector) {
            this.z = vector.z;
            this.x = vector.x;
            this.y = vector.y;
        }

        public Vector3 toVector3() {
            return new Vector3(x, y, z); ;
            
        }

    }
}