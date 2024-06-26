using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerDebug : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = new Vector2(transform.position.x, Mathf.Abs(Gamepad.current.leftStick.y.value));
  }
}
