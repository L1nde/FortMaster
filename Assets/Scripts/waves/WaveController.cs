using UnityEngine;

namespace Assets.Scripts.waves {
    public class WaveController : MonoBehaviour {
        public static WaveDetails CurreWaveDetails;
        public static WaveController instance;

        // Use this for initialization
        void Start () {
            if (instance == null) {
                DontDestroyOnLoad(gameObject);
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }

    }
}
