using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockCreate : MonoBehaviour {

    public GameObject block;
    public GameObject fortBase;
    private GameObject selected;

    private float initialGravity; 

	// Use this for initialization
	void Start () {
        selected = null;
        // creates triggers for pointer up and down
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
        // i had to change gravity to 0 or it would have accumulated speed and used this speed to move for 1 tick even if i set it's velocity to 0
        selected = Instantiate(block);
        selected.GetComponent<BoxCollider2D>().enabled = false;
        initialGravity = selected.GetComponent<Rigidbody2D>().gravityScale;
        selected.GetComponent<Rigidbody2D>().gravityScale = 0f;
        setSelectedAlpha(0.5f);
        
    }

    void OnPointerUp()
    {
        enableComponents();
        tryToCreateJoint();
        selected.transform.parent = fortBase.transform;
        selected = null;
    }

    private void tryToCreateJoint()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        GameObject closestAttatcher = getClosestAttatcherInRange(pz, 0.3f);
        if (!Equals(closestAttatcher, null)){
            Transform p = closestAttatcher.transform.parent;
            selected.AddComponent<FixedJoint2D>();
            selected.GetComponent<FixedJoint2D>().connectedBody = p.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void enableComponents()
    {
        selected.GetComponent<BoxCollider2D>().enabled = true;
        selected.GetComponent<Rigidbody2D>().velocity = new Vector3();
        selected.GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        setSelectedAlpha(1f);
    }

    private void setSelectedAlpha(float a)
    {
        Color c = selected.GetComponent<SpriteRenderer>().color;
        c.a = a;
        selected.GetComponent<SpriteRenderer>().color = c;
    }

    // Update is called once per frame
    void Update () {

        if (!Equals(selected, null))
            UpdatePos();
    }

    private void UpdatePos()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        GameObject closestAttatcher = getClosestAttatcherInRange(pz, 0.3f);
        if (Equals(closestAttatcher, null))
            selected.transform.position = pz;
        else
        {
            Transform parent = closestAttatcher.transform.parent.transform;
            float dir = Mathf.Deg2Rad*parent.rotation.eulerAngles.z + Mathf.Atan2(closestAttatcher.transform.localPosition.y, closestAttatcher.transform.localPosition.x);
            float dist = 0.5f;
            Vector3 pos = parent.localPosition;
            pos.x += dist * Mathf.Cos(dir);
            pos.y += dist * Mathf.Sin(dir);
            selected.transform.position = pos;
            selected.transform.rotation = parent.rotation;
        }
    }

    private GameObject getClosestAttatcherInRange(Vector3 pos, float range)
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
                if (range > d){
                    closest = a;
                    range = d;
                }
                
            }

        }
        return closest;
    }
}
