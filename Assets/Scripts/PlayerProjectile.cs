using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.enemies.ammo;

public class PlayerProjectile : Projectile {


    public float speed;
    public float noGravityTime;
    private bool dead;
    private Quaternion lastRot;
    // Use this for initialization
    void Start () {
        dead = false;
        lastRot = new Quaternion();
        rb2d = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        dead = true;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 5);
        transform.rotation = lastRot;
    }

    // Update is called once per frame
    void Update () {
        if (!dead) { 
            Vector2 vel = transform.GetComponent<Rigidbody2D>().velocity;
            lastRot = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*Mathf.Atan2(vel.y, vel.x));
            if (noGravityTime <= 0) {
                transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
            } else {
                noGravityTime -= Time.deltaTime;
            }
        }
    }

    public void init(float z, Vector3 pos, Vector3 target) {
        float r = Mathf.Deg2Rad * z;
        //Vector2 vel = new Vector2(Mathf.Cos(r)*speed, Mathf.Sin(r)*speed);
        //transform.GetComponent<Rigidbody2D>().velocity = vel;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, z);
        shootProjectile(target);
    }
    
}
