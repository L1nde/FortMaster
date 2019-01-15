using Assets.Scripts.enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour {


    public float range;
    public float dmg;
    public float lifetime;

    private CircleCollider2D trigger;

    // Use this for initialization
    void Start() {
        trigger = GetComponent<CircleCollider2D>();
        trigger.radius = range;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            Enemy e = collision.GetComponent<Enemy>();
            e.doDamage(dmg);
        }
    }
}
