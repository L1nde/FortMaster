using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour {
    public static ResearchController instance = null;

    public List<ResearchItem> researchedItems = new List<ResearchItem>();

    public List<ResearchItem> possibleItems = new List<ResearchItem>();

    // Use this for initialization
    void Start() {
        

    }

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void loadResearches(List<string> researches) {
        foreach (var research in researches) {
            foreach (var possibleItem in possibleItems) {

                if (possibleItem.name == research) {
                    researchedItems.Add(possibleItem);
                    possibleItems.Remove(possibleItem);
                    break;
                }
            }
        }
    }

    public void generateButtons() {
//        clearButtons();
        initializeButtons();
    }

    private void clearButtons() {
        GameObject screen = UIController.Instance.researchScreen;
        for (var i = screen.transform.childCount - 1; i >= 0; i--) {
            var child = screen.transform.GetChild(i);
            child.transform.parent = null;
            Destroy(child);
        }
    }

    private void initializeButtons() {
        foreach (var item in possibleItems) {
            if (item.prerequisites.Count == 0)
                UIController.Instance.createResearchButton(item);
            else {
                bool reqs = true;
                foreach (var item2 in item.prerequisites) {
                    if (!researchedItems.Contains(item2))
                        reqs = false;
                }

                if (reqs)
                    UIController.Instance.createResearchButton(item);
            }
        }

        
    }
}