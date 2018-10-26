﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public float gold;
    public Text goldText;

    // Use this for initialization
    void Start () {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addGold(float amount) {
        gold += amount;
        updateGoldText();
    }

    public bool canAfford(float amount) {
        return amount < gold;
    }

    public void removeGold(float amount) {
        gold -= amount;
        updateGoldText();
    }

    private void updateGoldText() {

    }
}