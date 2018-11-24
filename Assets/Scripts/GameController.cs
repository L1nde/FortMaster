using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static WaveDetails CurrentWaveDetails;

    public List<Trait> traits;
    public float gold;
    public float xp;

    // Use this for initialization
    void Start() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        updateGoldText();
        loadSave();
    }

    void OnEnable() {
        
    }
	
	// Update is called once per frame
	void Update () {         
	}


    public void loadSave() {
        if (ConstructionManager.instance == null)
            return;
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
        if (UIController.Instance != null)
            UIController.Instance.updateGoldText(gold);
    }

    private void updateXPText() {
        if (UIController.Instance != null)
            UIController.Instance.updateXPText(xp);
    }

    public Trait getTrait(string name) {
        foreach (Trait trait in traits) {
            if (trait.name == name)
                return trait;
        }
        return null;
    }
}
