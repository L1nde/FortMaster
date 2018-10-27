using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance = null;

    private GameObject fortBase;
    private Placeable selected;
    public Core core;


    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        fortBase = new GameObject("FortBase");
        createCore();
        DontDestroyOnLoad(gameObject);
        
    }

    void Update () {
        if (isAnythingSelected())
            selected.move();
	}

    private void createCore() {
        Core c = Instantiate(core, fortBase.transform);
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
        block.activateDragMode();
    }

    public Attacher getClosestStructureAttacherInRange(Vector3 pos, float range)
    {
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
