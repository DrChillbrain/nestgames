using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.LowLevel;

public class PrizeDisplayer : MonoBehaviour
{
    [Header("Display Info")]
    [SerializeField] private ClawController claw;
    [SerializeField] private CapsuleSpawner spawner;
    [SerializeField] private Vector2[] prizeDisplayLocations;
    [SerializeField] private GameObject winBG;
    private SpriteRenderer winBGSprite;
    private int prizesWon = 0;

    [Header("Sprite Info")]
    [SerializeField] private Sprite[] capsuleSprites;
    [SerializeField] private Sprite[] prizeSprites;
    [SerializeField] private GameObject prizePrefab;
    private PrizeClearer[] prizesWonList = new PrizeClearer[5];
    private SpriteRenderer capsuleSprite;
    private SpriteRenderer prizeSprite;

    [Header("Display Sequence")]
    [SerializeField] private int[] displayLengths;
    private float displayTimer = 0;
    private float displayAccel = 0;
    private float displayThreshold = -1;
    private float displayRotation;
    private Vector2 prizeStartLocation;
    private int displaySequence = -1;
    private GameObject currentCapsule = null;
    private GameObject currentPrize = null;

    // Start is called before the first frame update
    void Start()
    {
        winBGSprite = winBG.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (displaySequence)
        {
            case 0: //Move to center of screen and enlarge
                if (displayTimer < displayLengths[0]) {
                    //Smooth movement
                    displayTimer += updateTimer(0);

                    //Update position
                    currentCapsule.transform.position =
                        prizeStartLocation - (prizeStartLocation * ((float) displayTimer / (float) displayLengths[0]));
                        
                    winBG.transform.position =
                        prizeStartLocation - (prizeStartLocation * ((float)displayTimer / (float)displayLengths[0]));

                    currentPrize.transform.position =
                        prizeStartLocation - (prizeStartLocation * ((float)displayTimer / (float)displayLengths[0]));


                    //Update rotation
                    currentCapsule.transform.rotation = Quaternion.Euler(0, 0, 
                        displayRotation - (displayRotation * ((float)displayTimer / (float)displayLengths[0])));

                    currentPrize.transform.rotation = Quaternion.Euler(0, 0,
                        displayRotation - (displayRotation * ((float)displayTimer / (float)displayLengths[0])));

                    //Update scale
                    currentCapsule.transform.localScale = new Vector2(
                        1 + ((float)displayTimer / (float)displayLengths[0]),
                        1 + ((float)displayTimer / (float)displayLengths[0])
                    );

                    currentPrize.transform.localScale = new Vector2(
                        1 + ((float)displayTimer / (float)displayLengths[0]),
                        1 + ((float)displayTimer / (float)displayLengths[0])
                    );

                    winBG.transform.localScale = new Vector2(
                        0.75f + (0.25f * ((float)displayTimer / (float)displayLengths[0])),
                        0.75f + (0.25f * ((float)displayTimer / (float)displayLengths[0]))
                    );

                    //Update opacity
                    winBGSprite.color = new Color32(255, 255, 255,
                        (byte)(255 * ((float)displayTimer / (float)displayLengths[0])));

                    capsuleSprite.color = new Color32(255, 255, 255,
                        (byte)(255 - (255 * ((float)displayTimer / (float)displayLengths[0]))));

                    prizeSprite.color = new Color32(255, 255, 255,
                        (byte)(255 * ((float)displayTimer / (float)displayLengths[0])));

                } else {
                    displaySequence = -1;
                    displayThreshold = -1;
                    StartCoroutine(Defs.delay(0.5f, () =>
                    {
                        Destroy(currentCapsule);
                        displaySequence = 1;
                        displayTimer = 0;
                        prizeSprite.sortingLayerName = "UI";
                        winBGSprite.sortingLayerName = "UI";
                    }));
                }   
                break;
            case 1: //Move to prize location
                if (displayTimer < displayLengths[1]) {
                    //Smooth movement
                    displayTimer += updateTimer(1);

                    //Update location
                    currentPrize.transform.position =
                        prizeDisplayLocations[prizesWon] * ((float)displayTimer / (float)displayLengths[1]);
                    
                    //Update location
                    winBG.transform.position =
                        prizeDisplayLocations[prizesWon] * ((float)displayTimer / (float)displayLengths[1]);

                    //Update scale
                    currentPrize.transform.localScale = new Vector2(
                        0.8f + (1.2f - (1.2f * ((float)displayTimer / (float)displayLengths[1]))),
                        0.8f + (1.2f - (1.2f * ((float)displayTimer / (float)displayLengths[1])))
                    );

                    winBG.transform.localScale = new Vector2(
                        0.35f + (0.65f - (0.65f * ((float)displayTimer / (float)displayLengths[1]))),
                        0.35f + (0.65f - (0.65f * ((float)displayTimer / (float)displayLengths[1])))
                    );

                    //Update opacity
                    winBGSprite.color = new Color32(255, 255, 255,
                        (byte)(255 - (255 * ((float)displayTimer / (float)displayLengths[0]))));

                } else {
                    displaySequence = -1;
                    displayTimer = 0;
                    displayThreshold = -1;
                    prizesWon++;
                    
                    if (prizesWon == 5)
                    {
                        foreach (PrizeClearer pc in prizesWonList)
                        {
                            pc.startLeave();
                        }

                        spawner.spawnCapsules(9);
                        prizesWon = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            prizesWonList[i] = null;
                        }
                    }

                    StartCoroutine(Defs.delay(0.2f, () =>
                    {
                        claw.prizeSequenceOver();
                    }));
                }
                break;
            default: //Nothing
                break;
        }
    }

    //Function to smoothly update the timer while keeping positioning consistent
    private float updateTimer(int sequence)
    {
        if (displayThreshold == -1) {
            if (displayAccel < 1) {
                displayAccel += 0.02f;
            } else {
                displayAccel = 1;
                displayThreshold = displayTimer;
            }
        } else {
            if (displayTimer >= displayLengths[sequence] - displayThreshold) {
                if (displayAccel > 0.1) {
                    displayAccel -= 0.02f;
                } else {
                    displayAccel = 0.1f;
                }
            }
        }

        return 1.0f * displayAccel;
    }

    public void winPrize(GameObject prize)
    {
        //Disable collision of prize
        prize.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        prize.GetComponent<CircleCollider2D>().enabled = false;
        capsuleSprite = prize.GetComponent<SpriteRenderer>();
        capsuleSprite.sortingLayerName = "FG";
        capsuleSprite.sortingOrder = 1;
        winBGSprite.sortingLayerName = "FG";
        winBGSprite.sortingOrder = 2;

        //Set transition values
        displayRotation = setDisplayRotation(prize);

        winBG.transform.position = prize.transform.position;
        prizeStartLocation = prize.transform.position;
        currentCapsule = prize;
        displaySequence = 0;

        //Create prize object
        for (int i = 0; i < capsuleSprites.Length; i++)
        {
            if (capsuleSprite.sprite == capsuleSprites[i])
            {
                currentPrize = Instantiate(prizePrefab, prize.transform.position, prize.transform.rotation);
                prizeSprite = currentPrize.GetComponent<SpriteRenderer>();
                prizeSprite.sprite = prizeSprites[i];
                prizesWonList[prizesWon] = currentPrize.GetComponent<PrizeClearer>();
                break;
            }
        }
    }

    //Sets the rotation that's the closest to 0,
    //least distance to rotate, looks natural
    private float setDisplayRotation(GameObject prize)
    {
        float tempRot = prize.transform.rotation.eulerAngles.z;

        //Bring within -360 and 360
        tempRot %= 360;

        //Check for -180 to 0
        if (tempRot > Mathf.Abs(tempRot - 360))
        {
            tempRot -= 360;
        }

        //Check for 0 to 180
        if (tempRot > tempRot + 360)
        {
            tempRot += 360;
        }

        return tempRot;
    }
}
