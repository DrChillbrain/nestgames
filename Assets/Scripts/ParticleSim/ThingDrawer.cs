using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingDrawer : MonoBehaviour
{
    //prefab for the circle, standin for eventual particle effects
    public GameObject circle;
    //while a circle is being updated, it's stored here
    private GameObject currentCircle;
    //a vector by which each circle's size is increased each frame while squeezing
    private Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
    //coordinates of the thing that will be drawn
    //set every time a new squeeze is detected
    private float xCord, yCord;
    //Maximum value on either end for the X coordinate
    private float xMax = 7.3f;
    //Maximum value for the Y coordinate
    private float yMax = 3.6f;
    //Minimum value for the Y coordinate
    private float yMin = 0.0f;
    //checks if the squeeze has already been started
    private bool isSqueezing = false;

    // Start is called before the first frame update
    void Start()
    {
        isSqueezing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if a squeeze is being registered
        if (Input.GetKey("space")) {
            //if this is the first frame of the squeeze
            if (!isSqueezing) {
                randomizeCoordinates();
                //creates a new circle at the coordinates
                currentCircle = Instantiate(circle, new Vector3(xCord, yCord, 0), Quaternion.identity);
                isSqueezing = true;
            } else {
                //grow the circle
                currentCircle.transform.localScale += scaleChange;
            }
        //otherwise, release, if we were squeezing
        } else if (isSqueezing) {
            currentCircle.GetComponent<Circle>().release(currentCircle.transform.localScale.x);
            isSqueezing = false;
        }
    }

    //Sets xCord and yCord to new random values within the view
    void randomizeCoordinates() {
        xCord = Random.Range((0f - xMax), xMax);
        yCord = Random.Range(yMin, yMax);
    }
}
