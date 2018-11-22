using System;
using System.Collections.Generic;
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
        private List<GameObject> saves = new List<GameObject>();

        public void playButton() {
            anim.SetFloat("speed", -1f);
            anim.SetBool("options", false);
            anim.SetFloat("speed", 1f);
            anim.SetBool("playButton", true);
            foreach (var o in saves) {
                Destroy(o);
            }
            foreach (var fileInfo in SaveController.GetFilePaths()) {
                var save = Instantiate(saveObject, savesParent.transform);
                save.GetComponentInChildren<Text>().text = "Wave " + fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                save.GetComponentInChildren<Button>().onClick.AddListener(() => loadSave(fileInfo.Name));
                saves.Add(save);

            }
        }

        public void loadSave(string waveSaveName) {
            Debug.Log("Loaded wave " + waveSaveName);
            this.waveSaveName = waveSaveName;
            anim.SetFloat("speed", 1f);
            anim.SetBool("traits", true);
        }

        public void startGame() {
            
            if (waveSaveName == "null") {
                WaveController.CurreWaveDetails = waveZero;
            }
            else {
                WaveController.CurreWaveDetails = SaveController.instance.LoadWave(waveSaveName);
                Debug.Log(WaveController.CurreWaveDetails.enemies[0].count);
            }
            
            SceneManager.LoadScene("LIndeScene");
        }

        public void optionsButton() {
            anim.SetFloat("speed", 1f);
            anim.SetBool("traits", false);
            anim.SetBool("playButton", false);
            anim.SetBool("options", true);
        }
    }


}
