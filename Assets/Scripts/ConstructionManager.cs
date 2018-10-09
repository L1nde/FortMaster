using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance = null;

    private GameObject fortBase;
    private Placeable selected;


    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        fortBase = new GameObject("FortBase");
        DontDestroyOnLoad(gameObject);
        
    }

    void Update () {
        if (isAnythingSelected())
            selected.move();
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

    private void deSelect() {
        selected.disableDragMode();
        selected = null;
    }

    public GameObject getClosestStructureAttatcherInRange(Vector3 pos, float range)
    {
        StructureBlock[] blocks = fortBase.GetComponentsInChildren<StructureBlock>();
        range = Mathf.Pow(range, 2);
        GameObject closest = null;
        for (int i = 0; i < blocks.Length; i++) {
            GameObject[] attatchers = blocks[i].getAttatchPoints();
            for (int j = 0; j < attatchers.Length; j++) {
                GameObject a = attatchers[j];
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

    public GameObject getClosestTurretAttatcherInRange(Vector3 pos, float range) {
        StructureBlock[] blocks = fortBase.GetComponentsInChildren<StructureBlock>();
        range = Mathf.Pow(range, 2);
        GameObject closest = null;
        for (int i = 0; i < blocks.Length; i++) {
            GameObject a = blocks[i].getTurretAttatchPoint();
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
