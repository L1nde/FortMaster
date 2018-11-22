using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public float gold;
    public Text goldText;

    public float xp;
    public Text xpText;

    public ResearchPlaceable test;
    private List<ResearchPlaceable> researchedPlaceables = new List<ResearchPlaceable>();

    // Use this for initialization
    void Start() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        updateGoldText();
    }
	
	// Update is called once per frame
	void Update () {
        UIController.Instance.instantiateButton(test);
        if (EnemySpawn.waveEnd) {
//	        UIController.Instance.WinWave();
        }            
// 	    if (WaveController.waveOver) {
// //	        UIController.Instance.WinWave();
//         }            
	}

    public void addResearchedPlaceable(ResearchPlaceable item)
    {
        UIController.Instance.instantiateButton(item);
    }

    public void addGold(float amount) {
        gold += amount;
        updateGoldText();
    }

    public void addXP(float amount)
    {
        xp += amount;
        updateXPText();
    }

    public bool canAfford(float amount)
    {
        return amount <= gold;
    }

    public bool canAffordResearch(float amount) {
        return amount <= xp;
    }

    public void removeGold(float amount) {
        gold -= amount;
        updateGoldText();
    }

    public void removeXP(float amount)
    {
        xp -= amount;
        updateXPText();
    }

    private void updateGoldText() {
        goldText.text = "Gold: " + gold;
    }

    private void updateXPText() {
        xpText.text = "XP: " + xp;
    }
}
