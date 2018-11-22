﻿using System;
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
            anim.SetFloat("speed", -1f);
            anim.SetBool("options", false);
            anim.SetFloat("speed", 1f);
            anim.SetBool("playButton", true);
            var saveController = SaveController.instance;
            foreach (var fileInfo in saveController.GetFilePaths()) {
                var save = Instantiate(saveObject, savesParent.transform);
                save.GetComponentInChildren<Text>().text = "Wave " + fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                save.GetComponentInChildren<Button>().onClick.AddListener(() => loadSave(fileInfo.Name));

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
