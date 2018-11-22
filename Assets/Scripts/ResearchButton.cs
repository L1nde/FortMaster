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

    internal void setData(UnityEngine.Object data)
    {
        researchable = data;
    }

    internal void setCost(float cost)
    {
        this.cost = cost;
    }

    public void clicked()
    {
        if (GameController.instance.canAffordResearch(cost))
        {
            // TODO for prerequisities update available researches and done researches
            if (researchable is StructureBlockData)
            {
                ConstructionManager.instance.structureBlocks.Add((StructureBlockData)researchable);
                UIController.Instance.CreateStructureBlockButton((StructureBlockData)researchable);
            }
            else if (researchable is TurretData)
            {
                ConstructionManager.instance.turrets.Add((TurretData)researchable);
                UIController.Instance.CreateTurretButton((TurretData)researchable);
            }
            else
                return;
            GameController.instance.removeXP(cost);
            Destroy(gameObject);
        }
    }
}
