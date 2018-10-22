using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    public Button EnableStructureTabButton;
    public Button EnableTurretsTabButton;
    public Image WinScreen;
    public Image LoseScreen;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WinWave()
    {
        WinScreen.gameObject.SetActive(true);
    }

    public void LoseWave()
    {
        LoseScreen.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void DisableAllBuildingSubButtons()
    {
        EnableStructureTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
        EnableTurretsTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
    }
}
