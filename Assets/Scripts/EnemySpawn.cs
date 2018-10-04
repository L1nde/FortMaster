using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public float spawnDelay = 1f;
    public GameObject enemyPrefab;

    private float spawnAcc = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (spawnAcc > spawnDelay)
	    {
	        spawnAcc = 0f;
	        Instantiate(enemyPrefab, transform.position, transform.rotation);
	    }

	    spawnAcc += Time.deltaTime;

	}
}
