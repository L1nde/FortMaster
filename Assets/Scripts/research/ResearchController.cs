using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Research;
using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour {
    public static ResearchController instance = null;

    public ResearchItem ResearchBase;

    public ResearchButton researchButtonPrefab;
    public GameObject TreeLevel;
    public GameObject TreeRoot;

    [SerializeField]
    private GameObject ScrollableResearchView;

    private ResearchTreeNode researchTree;

    public ResearchItem[] RItems;

    // Use this for initialization
    void Start() {
        resetTree();
        createTreeGameObject(researchTree);

    }

    public void resetTree() {
        
        foreach (var item in RItems) {
            item.Init();
        }
        researchTree = new ResearchTreeNode(ResearchBase).createTree();
    }

public GameObject getScrollableResearchView() {

        var inst = Instantiate(ScrollableResearchView, transform);
        inst.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        inst.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        foreach (var button in inst.GetComponentsInChildren<ResearchButton>()) {
            Debug.Log("kek");
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



    private GameObject createTreeGameObject(ResearchTreeNode node) {
        return createTreeGameObject(node, 0, new Dictionary<int, GameObject>());
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
        node.button = button;
        button.setResearchText(node.researchName);
        button.setCostText(node.xpCost);
        button.setItem(node.Item);
        button.setCost(node.xpCost);
        return button;
    }


    public ResearchTreeNode getResearchTreeRoot() {
        return researchTree;
    }

    public void loadResearchTree(List<string> names) {
        foreach (var name in names) {
            var node = researchTree.FindNode(name);
            if (node != null) {
                node.researched = true;
            }

            
        }
    }

}