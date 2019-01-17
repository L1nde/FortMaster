using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Turrets;
using Assets.Scripts.waves;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance = null;

    public GameObject fortBase;
    private Placeable selected;

    private ResearchTreeNode researchRoot;

//    public List<TurretData> turrets;
//    public List<StructureBlockData> structureBlocks;
    public Core core;

    public Turret turretPrefab;
    public StructureBlock structureBlockPrefab;

    public int numberOfGoldBlocks;


    void Start() {
        if (instance == null)
            instance = this;

        else if (instance != this) 
            Destroy(gameObject);
        researchRoot = ResearchController.instance.getResearchTreeRoot();
        UIController.Instance.CreateTurretButtons(researchRoot.getResearchedTurrets().ToList());
        UIController.Instance.CreateStructureBlockButtons(researchRoot.getResearchedSB().ToList());
//        fortBase = new GameObject("FortBase");
        fortBase.tag = "FortBase";
//        DontDestroyOnLoad(gameObject);
        
    }

    void Update () {
        if (isAnythingSelected())
            selected.move();
	}


    private void createCore() {
        Core c = Instantiate(core, fortBase.transform);
    }

    public void initBlock(String name) {
        foreach (TurretData t in researchRoot.getResearchedTurrets().ToList()) { // not good probably
            if (t.name == name) {
                initTurret(t);
                return;
            }
        }
        foreach (StructureBlockData sb in researchRoot.getResearchedSB().ToList()) { // not good probably
            if (sb.name == name) {
                initStructureBlock(sb);
                return;
            }
        }

    }

    public void loadBuilding(List<PlaceableSaveObject> fort) {
        if (fort == null) {
            createCore();
            return;
        }
        foreach (var saveObject in fort) {
            
            if (saveObject.placeableName == "Core") {
                Instantiate(core, saveObject.position.toVector3(), Quaternion.Euler(saveObject.rotation.toVector3()), fortBase.transform);
                continue;
            }
            
            foreach (TurretData t in researchRoot.getResearchedTurrets().ToList()) {
                if (t.name == saveObject.placeableName) {
                    var block = Instantiate(attachTurretData(t), saveObject.position.toVector3(), Quaternion.Euler(saveObject.rotation.toVector3()));
                    block.setAnimController(t.aniController);
                    block.placeFree(fortBase.transform);
                    break;
                }
            }
            foreach (StructureBlockData sb in researchRoot.getResearchedSB().ToList()) {
                if (sb.name == saveObject.placeableName) {
                    var block = Instantiate(attachStructrueBlockData(sb), saveObject.position.toVector3(), Quaternion.Euler(saveObject.rotation.toVector3()));
                    block.placeFree(fortBase.transform);
                    break;
                }
            }
        }
    }

    private StructureBlock attachStructrueBlockData(StructureBlockData sbd) {
        structureBlockPrefab.name = sbd.name;
        structureBlockPrefab.cost = sbd.cost;
        structureBlockPrefab.jointBreakTorque = sbd.jointBreakTorque;
        structureBlockPrefab.hp = sbd.hp;
        structureBlockPrefab.GetComponent<SpriteRenderer>().sprite = sbd.sprite;
        structureBlockPrefab.placeSound = sbd.placeSoundGroup;
        return structureBlockPrefab;
    }

    private void initStructureBlock(StructureBlockData sbd)
    {
        Select(Instantiate(attachStructrueBlockData(sbd)));
    }

    private void initTurret(TurretData td) {
        var t = Instantiate(attachTurretData(td));
        t.setAnimController(td.aniController);
        Select(t);
    }

    private Turret attachTurretData(TurretData td) {
        turretPrefab.name = td.name;
        turretPrefab.cost = td.cost;
        turretPrefab.attackRange = td.attackRange;
        turretPrefab.projectile = td.projectile;
        turretPrefab.reloadTime = td.reloadTime;
        turretPrefab.minxRange = td.minxRange;
        turretPrefab.fireSound = td.fireSound;
        return turretPrefab;
    }

    public void PlaceBlock() {
        if (isAnythingSelected()) {
            selected.place(fortBase.transform);
            DeSelectCurrent();
        }
    }

    private void DeSelectCurrent() {
        selected = null;
    }

    private bool isAnythingSelected() {
        return !Equals(selected, null);
    }

    public void Select(Placeable block) {
        DeSelectCurrent();
        selected = block;
        if (selected.name == "Golden")
            selected.gameObject.AddComponent<GoldBlock>();
        
        block.activateDragMode();
    }

    public Attacher getClosestStructureAttacherInRange(Vector3 pos, float range)
    {
        if (fortBase == null)
            findFortBase();
        Placeable[] blocks = fortBase.GetComponentsInChildren<StructureBlock>();
        range = Mathf.Pow(range, 2);
        Attacher closest = null;
        for (int i = 0; i < blocks.Length; i++) {
            Attacher[] Attachers = blocks[i].getAttachPoints();
            for (int j = 0; j < Attachers.Length; j++) {
                Attacher a = Attachers[j];
                Vector3 aPos = a.transform.position;
                float d = Mathf.Pow(aPos.x - pos.x, 2) + Mathf.Pow(aPos.y - pos.y, 2);
                if (range > d) {
                    closest = a;
                    range = d;
                }
            }
        }
        return closest;
    }

    private void findFortBase() {
        fortBase = GameObject.FindGameObjectWithTag("FortBase");
        if (fortBase == null) {
            fortBase = new GameObject("FortBase");
        } 
    }

    public GameObject getClosestTurretAttacherInRange(Vector3 pos, float range) {
        StructureBlock[] blocks = fortBase.GetComponentsInChildren<StructureBlock>();
        range = Mathf.Pow(range, 2);
        GameObject closest = null;
        for (int i = 0; i < blocks.Length; i++) {
            GameObject a = blocks[i].getTurretAttachPoint();
            if (Equals(a, null))
                continue;
            Vector3 aPos = a.transform.position;
            float d = Mathf.Pow(aPos.x - pos.x, 2) + Mathf.Pow(aPos.y - pos.y, 2);
            if (range > d) {
                closest = a;
                range = d;
            }
            
        }
        return closest;
    }


}
