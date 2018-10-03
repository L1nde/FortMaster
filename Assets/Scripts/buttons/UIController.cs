using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    public Button EnableStructureTabButton;
    public Button EnableTurretsTabButton;

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

    public void DisableAllBuildingSubButtons()
    {
        EnableStructureTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
        EnableTurretsTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
    }
}
