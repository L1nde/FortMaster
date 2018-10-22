﻿using System.Collections;
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
	
    void OnPointerDown() {
        // sets the selected block in ConstructionManager
        ConstructionManager.instance.Select(Instantiate(block).GetComponent<Placeable>());
    }

    void OnPointerUp() {
        ConstructionManager.instance.PlaceBlock();
        if (block.GetComponent<MonoBehaviour>().GetType().Name == "Core")
            Destroy(gameObject);
    }

    void Update()
    {

    }


}
