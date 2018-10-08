using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager instance = null;

    private GameObject fortBase;
    private StructureBlock selectedSB;

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
        if (isStructureBlockSelected())
            moveSelectedStructureBlock();
	}

    public void PlaceBlock()
    {
        if (isStructureBlockSelected())
            PlaceStructureBlock();
    }

    private void DeSelectCurrent()
    {
        if (isStructureBlockSelected())
            deSelectStructureBlock();
        
    }

    private bool isAnythingSelected()
    {
        if (isStructureBlockSelected())
            return true;
        return false;
    }

    private void PlaceStructureBlock() {
        selectedSB.disableDragMode();
        selectedSB.CreateJoints();
        selectedSB.transform.parent = fortBase.transform;
        deSelectStructureBlock();
    }

    private void moveSelectedStructureBlock()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        selectedSB.transform.position = pos;
        GameObject closestAttatcher = getClosestAttatcherInRange(pos, 0.3f);
        if (Equals(closestAttatcher, null)) {
            selectedSB.transform.position = pos;
            selectedSB.transform.rotation = new Quaternion();
        }
        else
        {
            Transform parent = closestAttatcher.transform.parent.transform;
            float dir = Mathf.Deg2Rad * parent.rotation.eulerAngles.z + Mathf.Atan2(closestAttatcher.transform.localPosition.y, closestAttatcher.transform.localPosition.x);
            float dist = 0.5f;
            Vector3 pPos = parent.localPosition;
            pPos.x += dist * Mathf.Cos(dir);
            pPos.y += dist * Mathf.Sin(dir);
            selectedSB.transform.position = pPos;
            selectedSB.transform.rotation = parent.rotation;
        }
    }

    public void SelectStructureBlock(StructureBlock block)
    {
        DeSelectCurrent();
        selectedSB = block;
        selectedSB.activateDragMode();
    }

    private void deSelectStructureBlock()
    {
        selectedSB.disableDragMode();
        selectedSB = null;
    }

    private bool isStructureBlockSelected()
    {
        return !Equals(selectedSB, null);
    }

    public GameObject getClosestAttatcherInRange(Vector3 pos, float range)
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


}
