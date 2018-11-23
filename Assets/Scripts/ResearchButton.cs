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

    public void clicked()
    {
        if (GameController.instance.canAffordResearch(cost))
        {
            if (researchable is ResearchBlock)
            {
                ResearchBlock item = (ResearchBlock)researchable;
                ConstructionManager.instance.structureBlocks.Add(item.block);
                UIController.Instance.CreateStructureBlockButton(item.block);
                ResearchController.instance.possibleBlocks.Remove(item);
                ResearchController.instance.researchedBlocks.Add(item);
            }
            else if (researchable is ResearchTurret)
            {
                ResearchTurret item = (ResearchTurret)researchable;
                ConstructionManager.instance.turrets.Add(item.block);
                UIController.Instance.CreateTurretButton(item.block);
                ResearchController.instance.possibleTurrets.Remove(item);
                ResearchController.instance.researchedTurrets.Add(item);
            }
            else
                return;
            GameController.instance.removeXP(cost);
            Destroy(gameObject);
        }
    }
}
