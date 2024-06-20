using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] public MenuController arrow;
    [SerializeField] public int buttonID;
    void OnMouseEnter()
    {
        arrow.moveArrow(buttonID);
    }
    void OnMouseDown() {
        arrow.selectGame();
    }
}
