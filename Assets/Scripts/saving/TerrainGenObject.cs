namespace Assets.Scripts.waves {

    [System.Serializable]
    public class TerrainGenObject {
        public int seed;
        public int minX;
        public int maxX;
        public int minY;
        public int maxY;


        public TerrainGenObject(int maxX, int maxY, int minX, int minY, int seed) {
            this.maxX = maxX;
            this.maxY = maxY;
            this.minX = minX;
            this.minY = minY;
            this.seed = seed;
        }
    }
}