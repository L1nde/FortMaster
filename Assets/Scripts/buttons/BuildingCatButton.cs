using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCatButton : MonoBehaviour {

    private bool isEnabled;
    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        Disable();
    }

    void TaskOnClick()
    {
        if (isEnabled)
            Disable();
        else
            Enable();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Enable()
    {
        
        UIController.Instance.DisableAllBuildingSubButtons();
        isEnabled = true;
        gameObject.transform.Find("SubButtons").gameObject.SetActive(true);
    }

    public void Disable()
    {
        isEnabled = false;
        gameObject.transform.Find("SubButtons").gameObject.SetActive(false);
    }


}
