using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockCreate : MonoBehaviour {

    public GameObject block;

	// Use this for initialization
	void Start () {
        initTriggers();
    }

    private void initTriggers()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => OnPointerDown());
        trigger.triggers.Add(pointerDown);

        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => OnPointerUp());
        trigger.triggers.Add(pointerUp);
    }
	
    void OnPointerDown()
    {
        // sets the selected block in ConstructionManager
        ConstructionManager.instance.SelectStructureBlock(Instantiate(block).GetComponent<StructureBlock>());
    }

    void OnPointerUp()
    {
        ConstructionManager.instance.PlaceBlock();
    }

    void Update()
    {

    }

    private void tryToCreateJoint()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        //tryToCreateJointWithStructure(pz);
    }

    private void tryToCreateJointWithStructure(Vector3 pos)
    {
        /*
        GameObject closestAttatcher = getClosestAttatcherInRange(pos, 0.3f);
        if (!Equals(closestAttatcher, null)) {
            Transform p = closestAttatcher.transform.parent;
            //FixedJoint2D a1 = selected.AddComponent<FixedJoint2D>();
            selected.GetComponent<FixedJoint2D>().dampingRatio = 1f;
            selected.GetComponent<FixedJoint2D>().connectedBody = p.GetComponent<Rigidbody2D>();
        }
        */
    }


    

}
