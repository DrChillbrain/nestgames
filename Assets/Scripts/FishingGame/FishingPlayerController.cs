using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingPlayerController : MonoBehaviour
{
  [SerializeField] private Transform goalAreaController;
  [SerializeField] private ProgressBar catchBar;
  [SerializeField] private GameObject fishPrefab;

  [Header("Game Values")]
  [SerializeField] private float timeUntilCatch;
  private float catchProgress;


  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    //Show controller input values
    //Debug.Log("Mapped value: " + AnalogueInput.getValue() + ", Raw value: " + Gamepad.current.leftStick.up.value);

    //Update cursor position
    transform.position = new Vector2(transform.position.x,
      AnalogueInput.Map(AnalogueInput.getValue(), 0, 1, -3.35f, 3.35f));

    //Check if in range of goal area
    if (transform.position.y > goalAreaController.position.y - (goalAreaController.localScale.y) && 
        transform.position.y < goalAreaController.position.y + (goalAreaController.localScale.y)) {
      catchProgress += 1.0f * Time.deltaTime;
    } else {
      catchProgress -= 1.0f * Time.deltaTime;
      catchProgress = Mathf.Max(catchProgress, 0.0f);
    }

    //Update catch progress UI
    catchBar.setFill(catchProgress / timeUntilCatch);

    //Catch fish
    if (catchProgress >= timeUntilCatch) {
      //Instantiate(goalAreaController)
      Debug.Log("Fish caught!");
      catchProgress = 0;
    }
  }
}
