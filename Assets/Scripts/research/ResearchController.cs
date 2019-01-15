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

    public ResearchItem ResearchRoot;

    public ResearchButton researchButtonPrefab;
    public GameObject TreeLevel;
    public GameObject TreeRoot;

    private GameObject researchTree;

    // Use this for initialization
    void Start() {
        researchTree = createTree(ResearchRoot);

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
//        foreach (var item in possibleItems) {
//            if (item.prerequisites.Count == 0)
//                UIController.Instance.createResearchButton(item);
//            else {
//                bool reqs = true;
//                foreach (var item2 in item.prerequisites) {
//                    if (!researchedItems.Contains(item2))
//                        reqs = false;
//                }
//
//                if (reqs)
//                    UIController.Instance.createResearchButton(item);
//            }
//        }        
        

    }

    private GameObject createTree(ResearchItem root) {
        Queue<ResearchItem> queue = new Queue<ResearchItem>();
        Queue<ResearchItem> nextQueue = new Queue<ResearchItem>();
        nextQueue.Enqueue(root);

        HashSet<ResearchItem> closed = new HashSet<ResearchItem>();

        var level = Instantiate(TreeRoot);
        var rootObject = level;
        level.SetActive(false);
        
        while (queue.Count != 0 || nextQueue.Count != 0) {
            if (queue.Count == 0) {
                if (nextQueue.Count == 0) {
                    break;
                }
                queue = new Queue<ResearchItem>(nextQueue);
                nextQueue.Clear();
                var nextLevel = Instantiate(TreeLevel, rootObject.transform);
                level = nextLevel;

            }

            var currentItem = queue.Dequeue();

            if (!closed.Contains(currentItem)) {
                createButton(currentItem, level.transform);
                closed.Add(currentItem);
                foreach (var child in currentItem.getChilds()) {
                    nextQueue.Enqueue(child);

                }
            }
            

            
        }

        return rootObject;
    }

    void createButton(ResearchItem item, Transform parent) {
        ResearchButton button = Instantiate(researchButtonPrefab, parent);
        if (item.researched) {
            button.GetComponent<Button>().interactable = false;
        }
        button.setResearchText(item.researchName);
        button.setCostText(item.xpCost);
        button.setItem(item);
        button.setCost(item.xpCost);
    }

    public GameObject getResearchTreeObject() {
        return researchTree; 
    }

}