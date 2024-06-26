using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingPlayerController : MonoBehaviour
{
    

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    Debug.ClearDeveloperConsole();
    Debug.Log("Mapped value: " + AnalogueInput.getValue() + ", Raw value: " + Gamepad.current.leftStick.up.value);
    transform.position = new Vector2(transform.position.x,
      AnalogueInput.Map(AnalogueInput.getValue(), 0, 1, -3.35f, 3.35f));
  }
}
