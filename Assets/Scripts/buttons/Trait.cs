using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Trait : MonoBehaviour {

    public string name;
    public bool isToggled;
    public float xpmodifier;
    private Image image;

    void Start () {
        isToggled = false;
	}
	
	public void toggleButton() {
        isToggled = !isToggled;
        updateImage();
    }

    public abstract void apply();

    public void setImage(Image i) {
        image = i;
    }

    public void updateImage() {
        if (isToggled)
            image.color = new Color(0.5f, 0.5f, 0.5f);
        else
            image.color = new Color(1f, 1f, 1f);
    }
}

