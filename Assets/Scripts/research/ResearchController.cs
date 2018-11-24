using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour {
    public static ResearchController instance = null;

    public List<ResearchBlock> researchedBlocks = new List<ResearchBlock>();
    public List<ResearchTurret> researchedTurrets = new List<ResearchTurret>();

    public List<ResearchBlock> possibleBlocks = new List<ResearchBlock>();
    public List<ResearchTurret> possibleTurrets = new List<ResearchTurret>();

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        generateButtons();
    }

    public void generateButtons()
    {
//        clearButtons();
        initializeButtons();
    }

    private void clearButtons()
    {
        GameObject screen = UIController.Instance.researchScreen;
        for (var i = screen.transform.childCount - 1; i >= 0; i--)
        {
            var child = screen.transform.GetChild(i);
            child.transform.parent = null;
            Destroy(child);
        }
    }

    private void initializeButtons()
    {
        foreach (ResearchBlock item in possibleBlocks)
        {
            if (item.prerequisites.Count == 0)
                UIController.Instance.createResearchButton(item);
            else
            {
                bool reqs = true;
                foreach (ResearchBlock item2 in item.prerequisites)
                {
                    if (!researchedBlocks.Contains(item2))
                        reqs = false;
                }
                if (reqs)
                    UIController.Instance.createResearchButton(item);
            }
        }

        foreach (ResearchTurret item in possibleTurrets)
        {
            if (item.prerequisites.Count == 0)
                UIController.Instance.createResearchButton(item);
            else
            {
                bool reqs = true;
                foreach (ResearchTurret item2 in item.prerequisites)
                {
                    if (!researchedTurrets.Contains(item2))
                        reqs = false;
                }
                if (reqs)
                    UIController.Instance.createResearchButton(item);
            }
        }
    }
}
