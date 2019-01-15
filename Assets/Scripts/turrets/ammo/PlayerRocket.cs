using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour {


    public AudioClipGroup explosionSound;
    public RocketExplosion explosion;

    void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        explosionSound.play();
        RocketExplosion exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
