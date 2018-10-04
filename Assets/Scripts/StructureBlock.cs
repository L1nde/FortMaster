using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StructureBlock : MonoBehaviour {

    public float hp;
    private GameObject[] attachPoints;

	// Use this for initialization
	void Start () {
        attachPoints = new GameObject[4];
        setAttatchPoints();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
            DestroySelf();
	}

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void setAttatchPoints()
    {
        attachPoints[0] = gameObject.transform.Find("StructureAttatchPoint1").gameObject;
        attachPoints[1] = gameObject.transform.Find("StructureAttatchPoint2").gameObject;
        attachPoints[2] = gameObject.transform.Find("StructureAttatchPoint3").gameObject;
        attachPoints[3] = gameObject.transform.Find("StructureAttatchPoint4").gameObject;
    }

    public GameObject[] getAttatchPoints()
    {
        return attachPoints;
    }

    public void doDamage(float damage)
    {
        hp -= damage;
    }


}

