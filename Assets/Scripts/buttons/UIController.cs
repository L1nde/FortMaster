using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    public GameObject overlay;
    public Button EnableStructureTabButton;
    public Button EnableTurretsTabButton;
    public Button researchButton;
    public Image WinScreen;
    public Image LoseScreen;
    public Text countdown;
    public GameObject countdownPanel;

    // Use this for initialization
    void Start () {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnLevelLoading;
    }
	
	// Update is called once per frame
	void Update () {
        if (EnemySpawn.waveEnd)
            researchButton.gameObject.SetActive(true);
        else
            researchButton.gameObject.SetActive(false);
	}

    public void WinWave()
    {
        WinScreen.gameObject.SetActive(true);
        DisableAllBuildingSubButtons();
        Time.timeScale = 0.0f;
    }

    public void LoseWave()
    {
        LoseScreen.gameObject.SetActive(true);
        DisableAllBuildingSubButtons();
        Time.timeScale = 0.0f;
    }


    public void DisableAllBuildingSubButtons()
    {
        EnableStructureTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
        EnableTurretsTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
    }

    public void hideCountdown() {
        countdownPanel.SetActive(false);
    }

    public void showCountdown() {
        countdownPanel.SetActive(true);
    }


    public void updateCountdown(float countdown) {
        this.countdown.text = countdown.ToString("0.0");
    }

    public void exitToMenu() {
        SceneManager.LoadScene("WaveSelection");
    }

    void OnLevelLoading(Scene scene, LoadSceneMode mode) {
        if (scene.name == "WaveSelection") {
            overlay.SetActive(false);
        } else {
            overlay.SetActive(true);
        }
    }
}
