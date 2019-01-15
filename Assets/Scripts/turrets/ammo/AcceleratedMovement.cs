using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMovement : MonoBehaviour {

    public float force;
    private Rigidbody2D rb2d;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update () {
        rb2d.AddForce(rb2d.velocity.normalized * Time.deltaTime * force);
    }
}
