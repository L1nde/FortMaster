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
        setAttachPoints();
        canPlace = true;
        isPlaced = false;
        name = "Core";
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

    private void setAttachPoints() {
        AttachPoints = GetComponentsInChildren<Attacher>();
    }

    public override Attacher[] getAttachPoints() {
        return AttachPoints;
    }


}

