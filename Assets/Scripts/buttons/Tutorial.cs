using Assets.Scripts.waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject tutorialObj;
    public GameObject part1Obj;
    public GameObject part2Obj;
    public GameObject part3Obj;
    public GameObject part4Obj;
    public GameObject part5Obj;
    public GameObject part6Obj;
    public GameObject part7Obj;

    public int stepnr;

    public void doTutorial() {
        part1Obj.SetActive(false);
        part2Obj.SetActive(true);
        stepnr = 2;
    }

    public void skipTutorial() {
        endTutorial();
    }

    public void completeStepnr(int stepnr) {
        if (stepnr == 2)
            blockPlaced();
        else if (stepnr == 3)
            turretsOpened();
        else if (stepnr == 4)
            turretPlaced();
        else if (stepnr == 5)
            researchWindowOpened();
        else if (stepnr == 6)
            researchWindowHower();
        else if (stepnr == 7)
            endTutorial();

    }

    private void blockPlaced() {
        stepnr = 3;
        part2Obj.SetActive(false);
        part3Obj.SetActive(true);
    }

    private void turretsOpened() {
        stepnr = 4;
        part3Obj.SetActive(false);
        part4Obj.SetActive(true);
    }

    private void turretPlaced() {
        stepnr = 5;
        part4Obj.SetActive(false);
        part5Obj.SetActive(true);
    }

    private void researchWindowOpened() {
        stepnr = 6;
        part5Obj.SetActive(false);
        part6Obj.SetActive(true);
    }

    private void researchWindowHower() {
        stepnr = 7;
        part6Obj.SetActive(false);
        part7Obj.SetActive(true);
    }

    private void endTutorial() {
        tutorialObj.SetActive(false);
        UIController.Instance.tutorialInProgress = false;
    }


}
