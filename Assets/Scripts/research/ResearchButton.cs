using System;
using System.Collections;
using System.Collections.Generic;
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
        }

        
    }

   

}
