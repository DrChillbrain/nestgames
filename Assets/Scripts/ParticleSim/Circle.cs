using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //the circle's sprite renderer
    private SpriteRenderer rend;
    //the circle's 2d rigid body
    private Rigidbody2D body;
    //if a squeeze is currently being registered
    private bool isHeld;
    //size of the circle
    private float size;
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();
        isHeld = true;
        rend.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void release(float xSize) {
        size = xSize;
        isHeld = false;
        body.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //once we're released, start disappearing
        if (!isHeld) {
            Color temp = rend.color;
            //larger circles disappear slower
            temp.a -= (0.01f / size);
            //once we're invisible, destroy this object
            if (temp.a <= 0.0f) {
                Destroy(gameObject);
            }
            rend.color = temp;
        }
    }
}
