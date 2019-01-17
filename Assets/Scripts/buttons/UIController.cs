using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Turrets;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Research;
using System;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    public GameObject overlay;
    public Button EnableStructureTabButton;
    public Button EnableTurretsTabButton;
    public Button researchButton;
    public Image WinScreen;
    public Image LoseScreen;
    public Text countdown;
    public GameObject countdownPanel;
    public GameObject StructureBlockPanel;
    public GameObject TurretPanel;
    public BlockCreate PlaceableBlockPrefab;
    public GameObject researchScreen;
    public Text goldText;
    public Text xpText;

    public GameObject ScrollableResearchView;

    private List<BlockCreate> placeableItemButtons;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
        placeableItemButtons = new List<BlockCreate>();
    }

    public void researchScreenActivation()
    {
        if (ScrollableResearchView == null) {
            ScrollableResearchView = ResearchController.instance.getScrollableResearchView();
            ScrollableResearchView.transform.SetParent(researchScreen.transform, false);
        }
        researchScreen.SetActive(!researchScreen.activeSelf);
    }


    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (WaveController.waveOver)
            researchButton.gameObject.SetActive(true);
        else
            researchButton.gameObject.SetActive(false);
	}

    public void CreateTurretButton(TurretData turret)
    {
        PlaceableBlockPrefab.cost = turret.cost;
        PlaceableBlockPrefab.name = turret.name;
        PlaceableBlockPrefab.sprite = turret.sprite;
        BlockCreate create = Instantiate(PlaceableBlockPrefab, TurretPanel.transform);
        create.transform.position = create.transform.position;
        placeableItemButtons.Add(create);
        
    }

    public void CreateStructureBlockButton(StructureBlockData block)
    {
        PlaceableBlockPrefab.cost = block.cost;
        PlaceableBlockPrefab.name = block.name;
        PlaceableBlockPrefab.sprite = block.sprite;
        BlockCreate structureBlock = Instantiate(PlaceableBlockPrefab, StructureBlockPanel.transform);
        placeableItemButtons.Add(structureBlock);
    }

    public void CreateTurretButtons(List<TurretData> turrets) {
        foreach (Transform child in TurretPanel.transform) 
            Destroy(child.gameObject);
        
        foreach (TurretData t in turrets)
        {
            PlaceableBlockPrefab.cost = t.cost;
            PlaceableBlockPrefab.name = t.name;
            PlaceableBlockPrefab.sprite = t.sprite;
            BlockCreate turret = Instantiate(PlaceableBlockPrefab, TurretPanel.transform);
            turret.transform.position = turret.transform.position;
            placeableItemButtons.Add(turret);
        }
    }

    public void CreateStructureBlockButtons(List<StructureBlockData> sblocks) {
        foreach (Transform child in StructureBlockPanel.transform) 
            Destroy(child.gameObject);
        
        foreach (StructureBlockData sb in sblocks) {
            PlaceableBlockPrefab.cost = sb.cost;
            PlaceableBlockPrefab.name = sb.name;
            PlaceableBlockPrefab.sprite = sb.sprite;
            BlockCreate structureBlock = Instantiate(PlaceableBlockPrefab, StructureBlockPanel.transform);
            placeableItemButtons.Add(structureBlock);
        }
    }

    public void WinWave()
    {
        WinScreen.gameObject.SetActive(true);
        DisableAllBuildingSubButtons();
        Time.timeScale = 0.0f;
    }

    public void LoseWave()
    {
        LoseScreen.gameObject.SetActive(true);
        DisableAllBuildingSubButtons();
        Time.timeScale = 0.0f;
    }


    public void DisableAllBuildingSubButtons()
    {
        EnableStructureTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
        EnableTurretsTabButton.gameObject.GetComponent<BuildingCatButton>().Disable();
    }

    public void hideCountdown() {
        countdownPanel.SetActive(false);
    }

    public void showCountdown() {
        countdownPanel.SetActive(true);
    }


    public void updateCountdown(float countdown) {
        this.countdown.text = countdown.ToString("0.0");
    }

    public void exitToMenu() {
        SceneManager.LoadScene("WaveSelection");
    }

    public void updateGoldText(float gold) {
        goldText.text = "Gold: " + gold;
        updateButtons();
    }

    public void updateXPText(float xp) {
        xpText.text = "XP: " + xp;
    }

    private void updateButtons() {
        BlockCreate[] sbs = StructureBlockPanel.GetComponentsInChildren<BlockCreate>();
        BlockCreate[] ts = TurretPanel.GetComponentsInChildren<BlockCreate>();
        foreach (BlockCreate blockCreate in sbs) {
            blockCreate.updateBlock();
        }
        foreach (BlockCreate blockCreate in ts) {
            blockCreate.updateBlock();
        }
    }

    public void callWave() {
        WaveController.instance.callWave();
    }


}
