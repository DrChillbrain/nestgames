using TMPro;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

public class ControllerReader : MonoBehaviour {

  [SerializeField] private bool debugMode;
  [SerializeField] public int dataLength;
  [SerializeField] private TMP_Text text;
  [SerializeField] private GameObject calibrationScreen;
  private bool controllerConnected = false;
  public float[] minValues, maxValues;
  public float averageMin, averageMax;
  private void Start() {
    if (!debugMode) {
      //sets these arrays to the specified length
      minValues = new float[dataLength];
      maxValues = new float[dataLength];
      if(Gamepad.current == null) {
        text.SetText("No controller connected. Waiting...");
        //controllerConnected is false, it'll check every frame for it to
        //be true then start the calibration coroutine once it does
      } else {
        controllerConnected = true;
        StartCoroutine(startCalibration());
      }
    } else {
      AnalogueInput.setAxis(Gamepad.current.leftStick.up);
    }
    
  }

  void Update() {
    if (!debugMode) {
      //checks every frame if the controller is connected yet
      if(controllerConnected == false) {
        if(Gamepad.current != null) {
          controllerConnected = true;
          StartCoroutine(startCalibration());
        }
      }
    }
    
  }

  IEnumerator startCalibration() {

  //**SET UP FOR MIN VALUE**

    AnalogueInput.setAxis(Gamepad.current.leftStick.y);
    //give them 3 seconds to get ready to hold in a neutral position
    for (int i = 3; i > 0; i--) {
      text.SetText("Hold your hand at a neutral position: " + i);
      yield return new WaitForSeconds(1);
    }

    //**FIND MINIMUM VALUE**

    //every 0.05 seconds, save a new value
    for (int valuesSaved = 0; valuesSaved < dataLength; valuesSaved++) {
      minValues[valuesSaved] = (AnalogueInput.getAxis().value);
      text.SetText("Saved value " + (valuesSaved + 1) + "/" + dataLength);
      yield return new WaitForSeconds(.05f);
    }
    //fine the average
    float minSum = 0.0f;
    for (int i = 0; i < dataLength; i++) {
      minSum += minValues[i];
    }
    averageMin = (minSum / dataLength);
    //display the average
    text.SetText("Average resting value: " + averageMin);


    //**SET UP FOR MAX VALUE**

    yield return new WaitForSeconds(2);
    for (int i = 3; i > 0; i--) {
      text.SetText("Clench your hand as much as you comfortably can: " + i);
      yield return new WaitForSeconds(1);
    }
    
    //**FIND MAXIMUM VALUE**

    //every 0.05 seconds, save a new value
    for (int valuesSaved = 0; valuesSaved < dataLength; valuesSaved++) {
      maxValues[valuesSaved] = (AnalogueInput.getAxis().value);
      text.SetText("Saved value " + (valuesSaved + 1) + "/" + dataLength);
      yield return new WaitForSeconds(.05f);
    }
    //fine the average
    float maxSum = 0.0f;
    for (int i = 0; i < dataLength; i++) {
      maxSum += maxValues[i];
    }
    averageMax = (maxSum / dataLength);
    //display the average
    text.SetText("Average max pressure value: " + averageMax);


    yield return new WaitForSeconds(2);
    AnalogueInput.minValue = averageMin;
    AnalogueInput.maxValue = averageMax;
    calibrationScreen.SetActive(false);

    yield break;
  }


}