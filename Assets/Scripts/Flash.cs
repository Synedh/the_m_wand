using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

    public Image defaultImage;
    public bool flash;

    public Flash Instance;

	// Use this for initialization
	void Start () {
        Instance = this;
        defaultImage = this.GetComponent<Image>();
        flash = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (flash) {
            Color white = new Color(1, 1, 1, 1);
            defaultImage.color = Color.Lerp(defaultImage.color, white, 20 * Time.deltaTime);
            if (defaultImage.color.a >= 0.8) {
                flash = false;
            }
        }
        else {
            Color Transparent = new Color(1, 1, 1, 0);
            defaultImage.color = Color.Lerp(defaultImage.color, Transparent, 20 * Time.deltaTime);
        }
    }
}
