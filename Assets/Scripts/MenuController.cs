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
        public BaseWaveDetails waveZero;
        public Animator anim;
        private string waveSaveName = "null";
        private List<GameObject> saves = new List<GameObject>();
        public Button deleteSaves;
        public GameObject traitsPanel;
        public Button traitButtonPrefab;
        public GameObject Researches;

        void OnEnable() {
            deleteSaves.onClick.AddListener(() => SaveController.instance.deleteAll());
        }

        public void playButton() {
            enableTraits();
            anim.SetFloat("speed", -1f);
            anim.SetBool("options", false);
            anim.SetFloat("speed", 1f);
            anim.SetBool("playButton", true);
            foreach (var o in saves) {
                Destroy(o);
            }
            foreach (var fileInfo in SaveController.GetFilePaths(SaveController.waveFolderName)) {
                var save = Instantiate(saveObject, savesParent.transform);
                save.GetComponentInChildren<Text>().text = "Wave " + fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                save.GetComponentInChildren<Button>().onClick.AddListener(() => loadSave(fileInfo.Name));
                saves.Add(save);

            }
            
            ResearchController.instance.getResearchTreeObject().transform.SetParent(Researches.transform, false);
            ResearchController.instance.getResearchTreeObject().SetActive(true);
        }

        public void loadSave(string waveSaveName) {
            this.waveSaveName = waveSaveName;
            anim.SetFloat("speed", 1f);
            anim.SetBool("traits", true);
        }

        public void startGame() {
            
            if (waveSaveName == "null") {
                GameController.CurrentWaveDetails = waveZero.ToWaveDetails();
            }
            else {
                GameController.CurrentWaveDetails = SaveController.instance.LoadWave(waveSaveName);
            }
            
            SceneManager.LoadScene("LIndeScene");
            
        }

        public void optionsButton() {
            anim.SetFloat("speed", 1f);
            anim.SetBool("traits", false);
            anim.SetBool("playButton", false);
            anim.SetBool("options", true);
        }

        public void exitGame()
        {
            Application.Quit();
        }

        private void enableTraits() {

            if (traitsPanel.GetComponentsInChildren<Button>().Length != 0)
                return;
            foreach (Trait t in GameController.instance.getAllTraits()) {
                Button b = Instantiate(traitButtonPrefab, traitsPanel.transform);
                b.onClick.AddListener(() => t.toggleButton());
                b.GetComponentInChildren<Text>().text = t.name;
                t.setImage(b.GetComponent<Image>());
                t.updateImage();
            }
        }
    }
}
