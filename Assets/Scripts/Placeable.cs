using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public abstract class Placeable : MonoBehaviour {

    public float cost;
    protected bool researched = false;

	void Start () {
		
	}
	
	void Update () {
		
	}

    protected abstract void CreateJoints();

    public abstract void move();

    public abstract void activateDragMode();

    public abstract void disableDragMode();
    public abstract Attacher[] getAttachPoints();
    public abstract void place(Transform parent);

    public void research()
    {

    }

    protected void setSelectedAlpha(float a) {
        Color c = gameObject.GetComponent<SpriteRenderer>().color;
        c.a = a;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    protected void setSelectedRedColor(float g) {
        Color c = gameObject.GetComponent<SpriteRenderer>().color;
        c.g = g;
        c.b = g;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    internal bool isResearched()
    {
        return researched;
    }
}
