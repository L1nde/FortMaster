using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts {
    public class MenuController : MonoBehaviour {

        public Button play;

        public Animator anim;

        public void playButton() {
            anim.Play("InitialMenu");
        }

        public void save0() {
            anim.Play("TraitsMenu");
        }

        public void newGame() {
            SceneManager.LoadScene("LindeScene");
        }
    }


}
