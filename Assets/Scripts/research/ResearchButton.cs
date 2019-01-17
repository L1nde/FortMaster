using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Research;
using Assets.Scripts.Turrets;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour {
    public Text researchText;
    public Text costText;
    public float cost;
    public UnityEngine.Object researchable;
    public ResearchTreeNode nodeData;

    public Image toolTipPrefab;

    private Image currentToolTip;

    public void setResearchText(string text)
    {
        researchText.text = text;
    }

    public void setCostText(float cost)
    {
        costText.text = cost.ToString();
    }

    internal void setItem(UnityEngine.Object item)
    {
        researchable = item;
    }

    internal void setCost(float cost)
    {
        this.cost = cost;
    }

    public void clicked() // Could use maybe some refactoring
    {
        if (GameController.instance.canAffordResearch(cost))
        {
            
            if (researchable is ResearchBlock)
            {
                ResearchBlock item = (ResearchBlock)researchable;
                nodeData.researched = true;
//                UIController.Instance.CreateStructureBlockButton(item.block);
                GetComponent<Button>().interactable = false;
            }
            else if (researchable is ResearchTurret)
            {
                ResearchTurret item = (ResearchTurret)researchable;
                nodeData.researched = true;
                //                UIController.Instance.CreateTurretButton(item.block);
                GetComponent<Button>().interactable = false;
            }
            else
                return;
            GameController.instance.removeXP(cost);

        }
    }

    public void setNodeData(ResearchTreeNode node) {
        var n = node.FindNode(researchText.text);
        if (n != null) {
            nodeData = n;
            GetComponent<Button>().interactable = !nodeData.researched && nodeData.prerequisites.ToList().TrueForAll(prerequisitesResearched);
        }

    }

    public void hoverEnter() {
        if (currentToolTip != null)
            Destroy(currentToolTip.gameObject);

        currentToolTip = Instantiate(toolTipPrefab);
        currentToolTip.transform.SetParent(FindObjectOfType<Canvas>().transform);
        currentToolTip.transform.position = Input.mousePosition + new Vector3(60,-60,0);
        //ja ja ja polümorfismiga on parem
        if (researchable.GetType() == typeof(ResearchBlock)) {
            createToolTipForResearchBlock((ResearchBlock)researchable);
        } else if (researchable.GetType() == typeof(ResearchTurret)) {
            createToolTipForTurret((ResearchTurret)researchable);
        }
    }

    public void hoverExit() {
        Destroy(currentToolTip.gameObject);
        currentToolTip = null;
    }

    private void createToolTipForResearchBlock(ResearchBlock rb) {
        addTextLineToParent("Type: structure block", currentToolTip.transform);
        addTextLineToParent("Gold cost: " + rb.block.cost, currentToolTip.transform);
        addTextLineToParent("HP: " + rb.block.hp, currentToolTip.transform);
    }

    private void createToolTipForTurret(ResearchTurret turret) {
        addTextLineToParent("Type: structure block", currentToolTip.transform);
        addTextLineToParent("Gold cost: " + turret.block.cost, currentToolTip.transform);
        addTextLineToParent("Damage: " + turret.block.projectile.damage, currentToolTip.transform);
        addTextLineToParent("Reload: " + turret.block.reloadTime, currentToolTip.transform);
        addTextLineToParent("Range: " + turret.block.attackRange, currentToolTip.transform);
    }

    private void addTextLineToParent(string text, Transform parent) {
        GameObject textObj = new GameObject("myTextGO");
        textObj.transform.SetParent(parent);

        Text myText = textObj.AddComponent<Text>();
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        myText.font = ArialFont;
        myText.rectTransform.sizeDelta = new Vector2(100, 18);
        myText.color = new Color(0, 0, 0);
        myText.text = text;
    }

    private bool prerequisitesResearched(ResearchTreeNode item) {
        return item.researched;
    }

    public void alertButtons() {

    }
}
