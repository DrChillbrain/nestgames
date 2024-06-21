using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ButtonCommands : ScriptableObject
{
    public void backToMainMenu()
    {
        GameManager.ptr.endGame();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void calibrateAnalogueControl()
    {
        //Calibrate control
    }
}
