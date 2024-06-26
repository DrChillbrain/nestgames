using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

public class ControllerReader : MonoBehaviour {
  private void Start() {
    AnalogueInput.setAxis(Gamepad.current.leftStick.y);
  }
}