using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Trait : MonoBehaviour {

    public string name;
    public bool isToggled;
    public float xpmodifier;

    void Start () {
        isToggled = false;
	}
	
	public void toggleButton() {
        isToggled = !isToggled;
        Image i = GetComponent<Image>();
        if (isToggled)
            i.color = new Color(0.5f, 0.5f, 0.5f);
        else
            i.color = new Color(1f, 1f, 1f);
    }

    public abstract void apply();
}
