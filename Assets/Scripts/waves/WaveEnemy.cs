using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.enemies;
using UnityEngine;

[System.Serializable]
public class WaveEnemy {

    public Enemy enemy;
    public float count;

    public WaveEnemy(Enemy enemy, float count) {
        this.enemy = enemy;
        this.count = count;
    }

    public KeyValuePair<String, float> ToPair() {
        return new KeyValuePair<string, float>(enemy.name, count);
    }
}
