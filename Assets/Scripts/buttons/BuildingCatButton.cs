using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCatButton : MonoBehaviour {

    private bool isEnabled;
    public GameObject buttonsToShow;
    public bool enableOnStart;
    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        if (!enableOnStart)
            Disable();
        else
            Enable();
    }

    void TaskOnClick()
    {
        UIController.Instance.DisableAllBuildingSubButtons();
        Enable();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Enable()
    {
        UIController.Instance.tutorialPart(3);
        isEnabled = true;
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        buttonsToShow.SetActive(true);

    }

    public void Disable()
    {
        isEnabled = false;
        GetComponent<Image>().color = new Color(1, 1, 1);
        buttonsToShow.SetActive(false);
    }


}
