using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Research;
using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour {
    public static ResearchController instance = null;

    public List<ResearchItem> researchedItems = new List<ResearchItem>();

    public List<ResearchItem> possibleItems = new List<ResearchItem>();

    public ResearchItem ResearchBase;

    public ResearchButton researchButtonPrefab;
    public GameObject TreeLevel;
    public GameObject TreeRoot;

    [SerializeField]
    private GameObject ScrollableResearchView;

    private ResearchTreeNode researchTree;

    // Use this for initialization
    void Start() {
        researchTree = new ResearchTreeNode(ResearchBase).createTree();
        createTreeGameObject(researchTree);

    }

public GameObject getScrollableResearchView() {
        var inst = Instantiate(ScrollableResearchView, transform);
        inst.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        inst.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        foreach (var button in inst.GetComponentsInChildren<ResearchButton>()) {
            button.setNodeData(researchTree);
        }

        return inst;
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


    private void clearButtons() {
        GameObject screen = UIController.Instance.researchScreen;
        for (var i = screen.transform.childCount - 1; i >= 0; i--) {
            var child = screen.transform.GetChild(i);
            child.transform.parent = null;
            Destroy(child);
        }
    }


    private GameObject createTreeGameObject(ResearchTreeNode node) {
        return createTreeGameObject(researchTree, 0, new Dictionary<int, GameObject>());
    }

        private GameObject createTreeGameObject(ResearchTreeNode node, int depth, Dictionary<int, GameObject> levels) {
            if (!levels.ContainsKey(depth)) {
                var o = Instantiate(TreeLevel);
                o.transform.SetParent(TreeRoot.transform);
                levels.Add(depth, o);
            }

            var b = createButton(node, levels[depth].transform);

            foreach (var child in node.Childs) {
                var trans = createTreeGameObject(child, depth + 1, levels);
            }

            return b.gameObject;
    }


    private ResearchButton createButton(ResearchTreeNode node, Transform parent) {
        ResearchButton button = Instantiate(researchButtonPrefab, parent);
        if (node.researched) {
            button.GetComponent<Button>().interactable = false;
        }

        if (node.prerequisites.ToList().TrueForAll(prerequisitesResearched)) {
            button.GetComponent<Button>().interactable = false;
        }
        button.setResearchText(node.researchName);
        button.setCostText(node.xpCost);
        button.setItem(node.Item);
        button.setCost(node.xpCost);
        return button;
    }

    private bool prerequisitesResearched(ResearchTreeNode item) {
        return !item.researched;
    }

    public ResearchTreeNode getResearchTreeRoot() {
        return researchTree;
    }

}