using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    [Header("Claw Parts")]
    [SerializeField] private GameObject clawL;
    [SerializeField] private GameObject clawR;
    [SerializeField] private PrizeDetector prizeDetector;
  
    private bool moving = true;                               // State determining idle movement or action

    [Header("Claw Arm Properties")]
    [SerializeField] private float horizontalSpeed;           // Speed the claw moves left and right
    private float horizontalAccel = 1;                        // Controls direction the claw moves
    [SerializeField] private float horizontalAccelIncrement;  // How fast the claw changes direction
    private bool midMove = true;                              // State for if the claw is moving or changing direction

    [SerializeField] private float verticalSpeed;             // Speed the claw moves up and down
    private float verticalAccel = 1;                          // Controls direction the claw moves
    [SerializeField] private float verticalAccelIncrement;    // How fast the claw changes direction

    [Header("Boundaries")]
    [SerializeField] private float rightBound;                // Right edge of the claw's movement
    [SerializeField] private float leftBound;                 // Left edge of the claw's movement
    [SerializeField] private float topBound;                  // Top edge of the claw's movement
    [SerializeField] private float bottomBound;               // Bottom edge of the claw's movement

    [Header("Claw Grip Properties")]
    [SerializeField] private float clawSpeed;                 // Speed the claw's grips close
    [SerializeField] private float clawOpenPos;               // Rotation for the claw's open position
    [SerializeField] private float clawClosePos;              // Rotation for the claw's closed position
    [SerializeField] private float clawRigidity;              // How bouncy the claw's grips are
    private float clawRotation = -10;                         // Current rotation of claw

    private int dropSequence = -1;
    int side;

    // Start is called before the first frame update
    void Start()
    {
        //Makes values easier to edit in inspector
        horizontalSpeed /= 100;
        verticalSpeed /= 100;
        clawSpeed /= 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move control
        if (moving)
        {
            if (midMove)
            {
                move(true);

                //Check if needs to change direction
                if (transform.position.x > rightBound || transform.position.x < leftBound)
                {
                    midMove = false;
                    side = (horizontalAccel > 0) ? 1 : -1;
                }
            }
            else
            {
                //Change direction
                if (stopMovement(true, ref horizontalAccel, ref horizontalAccelIncrement)) {
                    if (startMovement(true, ref horizontalAccel, ref horizontalAccelIncrement)) {
                        move(true);
                        midMove = true;
                    }
                }
            }

            //User input
            if (Input.GetKey(KeyCode.Space))
            {
                moving = false;
                dropSequence = 0;
                side = (horizontalAccel > 0) ? 1 : -1;
            }
        } else {
            switch (dropSequence) {
                case 0: //Stop claw
                    if (stopMovement(true, ref horizontalAccel, ref horizontalAccelIncrement))
                    {
                        //Advance sequence after a delay
                        dropSequence = -1;
                        StartCoroutine(Defs.delay(0.3f, () => {
                            dropSequence = 1;
                            side = 1;
                            verticalAccel = 0;
                        }));
                    }
                    break;

                case 1: //Start drop
                    if (startMovement(false, ref verticalAccel, ref verticalAccelIncrement))
                        dropSequence++;
                    break;

                case 2: //Move down
                    move(false);

                    if (transform.position.y < bottomBound)
                    {
                        dropSequence++;
                        side = -1;
                    }
                    break;

                case 3: //Stop drop
                    if (stopMovement(false, ref verticalAccel, ref verticalAccelIncrement))
                    {
                        dropSequence = -1;
                        StartCoroutine(Defs.delay(0.2f, () => {
                            dropSequence = 4;
                        }));
                    }
                    break;

                case 4: //Close claw
                    if (closeClaw()) {
                        dropSequence = -1;
                        StartCoroutine(Defs.delay(0.2f, () =>
                        {
                            dropSequence = 5;
                        }));
                    }
                    break;

                case 5: //Raise claw
                    if (startMovement(false, ref verticalAccel, ref verticalAccelIncrement))
                        dropSequence++;
                    break;

                case 6: //Move claw up
                    move(false);
                    //Gonna have to mess with this value here
                    if (transform.position.y >= topBound) {
                        dropSequence++;
                        side = 1;
                    }
                    break;

                case 7: //Stop claw
                    if (stopMovement(false, ref verticalAccel, ref verticalAccelIncrement))
                    {
                        dropSequence = -1;
                        prizeDetector.checkForPrizes();
                    }
                    break;

                case 8: //Open claw
                    if (openClaw())
                    {
                        dropSequence = -1;
                        StartCoroutine(Defs.delay(0.2f, () =>
                        {
                            moving = true;
                            midMove = false;
                            side = (transform.position.x > rightBound + leftBound / 2) ? 1 : -1;
                            horizontalAccel = 0;
                        }));
                    }
                    break;
                default: //Nothing
                    break;
            }
        }
    }

    //Stop movement smoothly
    private bool stopMovement(bool horizontal, ref float accel, ref float accelIncrement)
    {
        if (accel * side > 0) {
            accel -= (side * accelIncrement);
            move(horizontal);
        } else {
            return true;
        }

        return false;
    }

    //Start movement smoothly
    private bool startMovement(bool horizontal, ref float accel, ref float accelIncrement)
    {
        if (accel * side > -1) {
            accel -= (side * accelIncrement);
            move(horizontal);
        } else {
            accel = -side;
            return true;
        }

        return false;
    }

    private void move(bool horizontal)
    {
        if (horizontal) {
            //Update position
            transform.position = new Vector2(transform.position.x + (horizontalSpeed * horizontalAccel), transform.position.y);
        } else {
            //Update position
            transform.position = new Vector2(transform.position.x, transform.position.y + (verticalSpeed * verticalAccel));
        }
    }

    private bool closeClaw()
    {
        //Check rotation
        if (clawRotation < clawClosePos) {
            //Change rotation
            clawRotation += clawSpeed;
            clawL.transform.rotation = Quaternion.Euler(new Vector3(0, 0, clawRotation));
            clawR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -clawRotation));
        } else {
            return true;
        }

        return false;
    }

    private bool openClaw()
    {
        //Check rotation
        if (clawRotation > clawOpenPos)
        {
            //Change rotation
            clawRotation -= clawSpeed;
            clawL.transform.rotation = Quaternion.Euler(new Vector3(0, 0, clawRotation));
            clawR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -clawRotation));
        }
        else
        {
            return true;
        }

        return false;
    }

    public void prizeSequenceOver()
    {
        dropSequence = 8;
    }
}
