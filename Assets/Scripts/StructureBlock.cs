using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBlock : MonoBehaviour {

    public float hp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
            DestroySelf();
	}

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
