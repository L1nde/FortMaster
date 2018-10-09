using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {


    public float speed;
    public float noGravityTime;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 vel = transform.GetComponent<Rigidbody2D>().velocity;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*Mathf.Atan2(vel.y, vel.x));
        if (noGravityTime <= 0) {
            transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
        } else {
            noGravityTime -= Time.deltaTime;
        }
	}

    public void init(float z, Vector3 pos) {
        float r = Mathf.Deg2Rad * z;
        Vector2 vel = new Vector2(Mathf.Cos(r)*speed, Mathf.Sin(r)*speed);
        transform.GetComponent<Rigidbody2D>().velocity = vel;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
