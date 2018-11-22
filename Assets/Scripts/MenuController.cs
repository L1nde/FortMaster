using System;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts {
    public class MenuController : MonoBehaviour {

        public GameObject saveObject;
        public GameObject savesParent;
        public WaveDetails waveZero;
        public Button play;
        public Animator anim;
        private string waveSaveName = "null";

        public void playButton() {
            anim.Play("InitialMenu");
            var saveController = SaveController.instance;
            foreach (var fileInfo in saveController.GetFilePaths()) {
                var save = Instantiate(saveObject, savesParent.transform);
                save.GetComponentInChildren<Text>().text = "Wave " + fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);


            }
        }

        public void loadSave(string waveSaveName) {
            this.waveSaveName = waveSaveName;
            anim.Play("TraitsMenu");
        }

        public void startGame() {
            if (waveSaveName == "null") {
                WaveController.CurreWaveDetails = waveZero;
            }
            else {
                WaveController.CurreWaveDetails = SaveController.instance.LoadWave(waveSaveName);
            }
            
            SceneManager.LoadScene("testmap");
        }
    }


}
