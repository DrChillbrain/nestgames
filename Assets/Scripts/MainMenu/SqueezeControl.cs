using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezeControl : MonoBehaviour
{
    //the two different sprites. 0 is on, 1 is off
    [SerializeField] private Sprite[] options;
    //the bar, to be hidden or shown
    [SerializeField] private GameObject squeezeBar;
    public bool squeezeControl;
    //this object's sprite renderer
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        setSqueeze(true);
    }

    void OnMouseDown() {
        if (squeezeControl == false) {
            setSqueeze(true);
        } else {
            setSqueeze(false);
        }
    }

    void setSqueeze(bool target) {
        if (target == true) {
            squeezeControl = true;
            mySpriteRenderer.sprite = options[0];
            squeezeBar.SetActive(true);
        } else {
            squeezeControl = false;
            mySpriteRenderer.sprite = options[1];
            squeezeBar.SetActive(false);
        }
    }
}
