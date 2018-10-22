﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Placeable : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    protected abstract void CreateJoints();

    public abstract void move();

    public abstract void activateDragMode();

    public abstract void disableDragMode();
    public abstract GameObject[] getAttachPoints();
    public abstract void place(Transform parent);

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
}
