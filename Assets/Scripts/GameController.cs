using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static WaveDetails CurrentWaveDetails;

    public float gold;
    public Text goldText;
    public WaveController waveController;
    

    public float xp;
    public Text xpText;

    // Use this for initialization
    void Start() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

//        DontDestroyOnLoad(gameObject);
        updateGoldText();
        loadSave();


    }

    void OnEnable() {
        
    }
	
	// Update is called once per frame
	void Update () {         
	}


    public void loadSave() {
        ConstructionManager.instance.loadBuilding(CurrentWaveDetails.fortObjects);
        gold = CurrentWaveDetails.gold;
        updateGoldText();
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
