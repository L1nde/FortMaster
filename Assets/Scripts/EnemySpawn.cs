using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {
    public float spawnDelay = 1f;

    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<int> enemyCount = new List<int>();

    private float spawnAcc = 0f;

    // Use this for initialization
    void Start() {
        if (enemyPrefabs.Count != enemyCount.Count)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (enemyCount.Count == 0)
            Destroy(gameObject);

        if (spawnAcc > spawnDelay) {
            spawnAcc = 0f;

            int n;
            while (true)
            {
                n = Random.Range(0, enemyPrefabs.Count);
                if (enemyCount[n] != 0)
                    break;
                enemyPrefabs.RemoveAt(n);
                enemyCount.RemoveAt(n);

                if (enemyCount.Count == 0)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            enemyCount[n]--;
            Instantiate(enemyPrefabs[n], transform.position, transform.rotation);
        }

        spawnAcc += Time.deltaTime;
    }
}