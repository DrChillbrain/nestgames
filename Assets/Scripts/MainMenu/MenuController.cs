using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    //Vector3 positions for each of the 3 places the arrow can be
    [SerializeField] private Vector3 squeezeColorPosition;
    [SerializeField] private Vector3 craneGamePosition;
    [SerializeField] private Vector3 goneFishinPosition;
    //tracks which game is currently selected.
    private int selectedGame;
    void Start() {
        moveArrow(0);
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
            SceneManager.LoadScene("ParticleSim", LoadSceneMode.Single);
            break;
        case 1:
            SceneManager.LoadScene("ClawGame", LoadSceneMode.Single);
            break;
        case 2:
            //go to gone fishin scene
            break;
        default:
            Debug.Log("Error: Unknown Game ID");
            break;
        }
    }
}
