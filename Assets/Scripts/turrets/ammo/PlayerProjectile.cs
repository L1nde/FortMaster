﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.enemies.ammo;
using Assets.Scripts.enemies;

public class PlayerProjectile : Projectile {

    public float noGravityTime;
    public float accuracySpread = 0.8f;
    public bool stayStuck;
    private bool dead;
    private Quaternion lastRot;
    // Use this for initialization
    void Start () {
        dead = false;
        lastRot = new Quaternion();
        rb2d = GetComponent<Rigidbody2D>();

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy" && !dead)
            collision.gameObject.GetComponent<Enemy>().doDamage(damage);
        dead = true;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && stayStuck)
            CreateJoint(rb);
        //Destroy(GetComponent<Rigidbody2D>());
        if (deathDelay == 0)
            Destroy(gameObject);
        else
            Destroy(gameObject, deathDelay);
        transform.rotation = lastRot;
    }

    private void CreateJoint(Rigidbody2D rb) {
        FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
        fj.connectedBody = rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (!dead) { 
            Vector2 vel = transform.GetComponent<Rigidbody2D>().velocity;
            lastRot = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*Mathf.Atan2(vel.y, vel.x));
            if (noGravityTime <= 0 && affectedByGravity) {
                transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
            } else {
                noGravityTime -= Time.deltaTime;
            }
        }
    }

    public void init(float z, Vector3 pos, Vector3 target) {
        //float r = Mathf.Deg2Rad * z;
        //Vector2 vel = new Vector2(Mathf.Cos(r)*speed, Mathf.Sin(r)*speed);
        //transform.GetComponent<Rigidbody2D>().velocity = vel;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, z);
        float xdist = Mathf.Abs(target.x - pos.x)/10;
        shootProjectile(new Vector2(target.x, target.y) + Random.insideUnitCircle * accuracySpread * xdist);
    }
    
}
