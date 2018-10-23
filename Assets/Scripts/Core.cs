using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Core : StructureBlock
{


    // Use this for initialization
    void Start()
    {
        setAttatchPoints();
        canPlace = true;
        isPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            DestroySelf();
    }

    private void DestroySelf()
    {
        UIController.Instance.LoseWave();
        Destroy(gameObject);
    }

    private void setAttatchPoints() {
        attatchPoints = GetComponentsInChildren<Attatcher>();
    }

    public override Attatcher[] getAttatchPoints() {
        return attatchPoints;
    }


}

