using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Turrets;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private List<BlockCreate> turretButtons;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
        turretButtons = new List<BlockCreate>();
    }

    void Start () {

        SceneManager.sceneLoaded += OnLevelLoading;
    }
	
	// Update is called once per frame
	void Update () {
        if (WaveController.waveOver)
            researchButton.gameObject.SetActive(true);
        else
            researchButton.gameObject.SetActive(false);
	}

    public void CreateTurretButtons(List<TurretData> turrets)
    {
        int x = 0;
        foreach (TurretData t in turrets)
        {
            PlaceableBlockPrefab.cost = t.cost;
            PlaceableBlockPrefab.name = t.name;
            BlockCreate turret = Instantiate(PlaceableBlockPrefab, TurretPanel.transform);
            turret.transform.position = turret.transform.position + new Vector3(x * 50,0,0);
            turretButtons.Add(turret);
            x += 1;
        }
    }

    public void CreateStructureBlockButtons(List<StructureBlockData> sblocks) {
        int x = 0;
        foreach (StructureBlockData sb in sblocks) {
            PlaceableBlockPrefab.cost = sb.cost;
            PlaceableBlockPrefab.name = sb.name;
            BlockCreate structureBlock = Instantiate(PlaceableBlockPrefab, StructureBlockPanel.transform);
            //structureBlock.transform.position = structureBlock.transform.position + new Vector3(x * 50, 0, 0);
            turretButtons.Add(structureBlock);
            x += 1;
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

    void OnLevelLoading(Scene scene, LoadSceneMode mode) {
        if (scene.name == "WaveSelection") {
            overlay.SetActive(false);
        } else {
            overlay.SetActive(true);
        }
    }
}
