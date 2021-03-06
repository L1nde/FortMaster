﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Research;
using Assets.Scripts.saving;
using Assets.Scripts.waves;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public static WaveDetails CurrentWaveDetails;

    public float gold;
    public float xp;

    // Use this for initialization
    void Start() {
        updateGoldText();
        loadData();
    }

    void Awake() {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        
    }
	
	// Update is called once per frame
	void Update () {     

	}

    public void setUpWave() {
//        loadResearch();
        loadWave();
    }

    public void saveData() {
        var gameSaveObject = new GameSaveObject(ResearchController.instance.getResearchTreeRoot().getResearchedItemsNames(), xp);

        SaveController.instance.saveData(gameSaveObject);
    }

    public void loadData() {
        var gameSaveObject = SaveController.instance.LoadData();
        if (gameSaveObject == null) {
            return;
        }
        xp = gameSaveObject.xp;
        updateXPText();
        
        ResearchController.instance.loadResearchTree(gameSaveObject.researchedTree);
    }

//    private void loadResearch() {
//        foreach (var researchItem in ResearchController.instance.researchedItems) {
//            if (researchItem is ResearchBlock) {
//                var researchBlock = researchItem as ResearchBlock;
//                ConstructionManager.instance.structureBlocks.Add(researchBlock.block);
//                UIController.Instance.CreateStructureBlockButton(researchBlock.block);
//            } else if (researchItem is ResearchTurret) {
//                var researchTurret = researchItem as ResearchTurret;
//                ConstructionManager.instance.turrets.Add(researchTurret.block);
//                UIController.Instance.CreateTurretButton(researchTurret.block);
//            }
//        }
//    }

    public void loadWave() {
        if (ConstructionManager.instance == null)
            return;
        ConstructionManager.instance.loadBuilding(CurrentWaveDetails.fortObjects);
        gold = CurrentWaveDetails.gold;
        updateGoldText();
    }

    public void addGold(float amount) {
        gold += amount * (1 + (ConstructionManager.instance.numberOfGoldBlocks / 10f)); // every gold block increases gold gain by 10%
        updateGoldText();
    }

    public void addXP(float amount) {
        xp += amount;
        updateXPText();
    }

    public bool canAfford(float amount) {
        return amount <= gold;
    }

    public bool canAffordResearch(float amount) {
        return amount <= xp;
    }

    public void removeGold(float amount) {
        gold -= amount;
        updateGoldText();
    }

    public void removeXP(float amount) {
        xp -= amount;
        updateXPText();
    }

    private void updateGoldText() {
        if (UIController.Instance != null)
            UIController.Instance.updateGoldText(Mathf.Floor(gold));
    }

    private void updateXPText() {
        if (UIController.Instance != null)
            UIController.Instance.updateXPText(xp);
    }

    public Trait[] getAllTraits() {
        return GetComponentsInChildren<Trait>();
    }

    public HPTrait getHPTrait() {
        return GetComponentInChildren<HPTrait>();
    }

    public DmgTrait GetDmgTrait() {
        return GetComponentInChildren<DmgTrait>();
    }
}
