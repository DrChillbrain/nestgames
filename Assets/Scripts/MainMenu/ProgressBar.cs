using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barSprite;


    public void setFill(float fill) {
        barSprite.fillAmount = fill;
    }

}
