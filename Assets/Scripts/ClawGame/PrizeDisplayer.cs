using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PrizeDisplayer : MonoBehaviour
{
    [Header("Display Info")]
    [SerializeField] private Vector2[] prizeDisplayLocations;
    [SerializeField] private GameObject winBG;
    [SerializeField] private int prizesWon = 0;

    [Header("Display Sequence")]
    [SerializeField] private int[] displayLengths;
    private int displayTimer;
    private Vector2 prizeStartLocation;
    private int displaySequence = -1;
    private GameObject currentPrize = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (displaySequence)
        {
            case 0: //Move to center of screen and enlarge
                if (displayTimer < displayLengths[0]) {
                    displayTimer++;
                    currentPrize.transform.position = 
                        prizeStartLocation - (prizeStartLocation * (displayLengths[0] - displayTimer));
                    winBG.transform.position = currentPrize.transform.position;
                } else {
                    displaySequence++;
                }   
                break;
            case 1: //Move to prize location
                break;
            default: //Nothing
                break;
        }
    }

    public void winPrize(GameObject prize)
    {
        //Disable collision of prize
        prize.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        prize.GetComponent<CircleCollider2D>().enabled = false;
        prize.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

        winBG.transform.position = prize.transform.position;
        currentPrize = prize;
        displaySequence = 0;
    }
}
