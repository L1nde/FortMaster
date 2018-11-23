namespace Assets.Scripts.waves {
    [System.Serializable]
    public class WaveSaveObject {
        public float waveScore;
        public float spawnDelay = 5;
        public float buildTime = 30f;
        public int waveNr = 0;

        public WaveSaveObject(WaveDetails waveDetails) {
            this.waveScore = waveDetails.waveScore;
            this.spawnDelay = waveDetails.spawnDelay;
            this.buildTime = waveDetails.buildTime;
            this.waveNr = waveDetails.waveNr;
        }
    }
}
