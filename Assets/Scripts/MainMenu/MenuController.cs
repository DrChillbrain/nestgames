using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private SqueezeControl squeezeControl;
    [SerializeField] private ProgressBar squeezeBar;
    //Vector3 positions for each of the 3 places the arrow can be
    [SerializeField] private Vector3 squeezeColorPosition;
    [SerializeField] private Vector3 craneGamePosition;
    [SerializeField] private Vector3 goneFishinPosition;
    [SerializeField] private int frameThreshold;
    //tracks the number of frames it's been since a squeeze started
    public int squeezeFrames = 0;
    //current selected game
    private int selectedGame;
    void Start() {
        moveArrow(0);
    }
    void FixedUpdate() {
        if (Input.GetKey("space") && squeezeControl.squeezeControl == true) {
            if (squeezeFrames <= 0) {
                //show squeeze timer bar
                squeezeFrames = 1;
            } else {
                squeezeFrames++;
                //float between 0 and 1 for the fill of the bar
                squeezeBar.setFill((float)squeezeFrames / (float)frameThreshold);
            }
        } else {
            //this happens when a squeeze is released
            if (squeezeFrames > 0) {
                squeezeBar.setFill(0f);
                if (squeezeFrames >= frameThreshold) {
                    squeezeFrames = 0;
                    selectGame();
                } else {
                    squeezeFrames = 0;
                    nextGame();
                }
            }
        }
    }

    public void nextGame() {
        if (selectedGame < 2) {
            moveArrow(selectedGame + 1);
        } else {
            moveArrow(0);
        }
    }

    public void moveArrow(int id) {
        selectedGame = id;
        switch(selectedGame) 
        {
        //squeeze color
        case 0:
            transform.position = squeezeColorPosition;
            break;
        //crane game
        case 1:
            transform.position = craneGamePosition;
            break;
        //gone fishin
        case 2:
            transform.position = goneFishinPosition;
            break;
        //should never happen
        default:
            Debug.Log("Error: Unknown Arrow Position");
            break;
        }
    }

    public void selectGame() {
        switch (selectedGame)
        {
        //if you change the name of a scene, change it here too
        case 0:
            SceneManager.LoadScene("ParticleSim");
            break;
        case 1:
            SceneManager.LoadScene("ClawGame");
            break;
        case 2:
            SceneManager.LoadScene("FishingGame");
            break;
        default:
            Debug.Log("Error: Unknown Game ID");
            return;
        }
    }
}
