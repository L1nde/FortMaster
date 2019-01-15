using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBlock : MonoBehaviour {


	private void Start () {
        ConstructionManager.instance.numberOfGoldBlocks++;
	}

    private void OnDestroy() {
        ConstructionManager.instance.numberOfGoldBlocks--;
    }
}
