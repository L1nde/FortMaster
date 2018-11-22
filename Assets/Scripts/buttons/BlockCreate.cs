using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockCreate : MonoBehaviour
{

    public int cost;
    public String name;

	// Use this for initialization
	void Start () {
        initTriggers();
        transform.Find("costText").GetComponent<Text>().text = cost + "$";
	    transform.Find("Text").GetComponent<Text>().text = name;
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
	
    void OnPointerDown() {
        // sets the selected block in ConstructionManager
        if (GameController.instance.canAfford(cost))
            ConstructionManager.instance.initBlock(name);
    }

    void OnPointerUp() {
        ConstructionManager.instance.PlaceBlock();
        if (name == "Core")
            Destroy(gameObject);
    }

    void Update()
    {

    }


}
