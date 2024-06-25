using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;

public class ControllerReader : MonoBehaviour {
  private void Start() {
    AnalogueInput.setAxis(Gamepad.current.leftStick.up);
    //var myAction = new InputAction(binding: "/*/<button>");
    //myAction.onPerformed += (action, control) => Debug.Log($"Button {control.name} pressed!");
    //myAction.Enable();
  }
}

/*
public class ControllerReader : MonoBehaviour
{
  public static ControllerReader ptr;
  private IDisposable _eventListener;

  private void Start() {
    ptr = this;
    this.gameObject.SetActive(false);
  }

  private void OnEnable() {
    _eventListener = InputSystem.onEvent.ForDevice(Gamepad.current).Call(sendAxis);
  }

  private void OnDisable() {
    _eventListener.Dispose();
  }

  private void sendAxis(InputEventPtr eventPtr) {
    //Check for axis

    AnalogueInput.setAxis(new AxisControl());
    this.gameObject.SetActive(false);
  }
}
*/
