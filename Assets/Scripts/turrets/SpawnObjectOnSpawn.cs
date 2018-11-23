using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnSpawn : MonoBehaviour {

    public GameObject objectToSpawn;
    public float rotationOffset;
    public Vector3 posOffset;
	// Use this for initialization
	void Start () {
        GameObject o = Instantiate(objectToSpawn);
        Vector3 rot = transform.rotation.eulerAngles + new Vector3(0, 0, rotationOffset);
        o.transform.rotation = Quaternion.Euler(rot);
        o.transform.position = transform.rotation * posOffset + transform.position;
	}
}
