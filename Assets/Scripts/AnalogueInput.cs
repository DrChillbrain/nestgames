using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public static class AnalogueInput {
  private static float minValue = 0.25f;
  private static float maxValue = 0.75f;
  private static AxisControl axis;

  public static void setMinValue(float newMinValue) {
    minValue = newMinValue;
  }

  public static void setMaxValue(float newMaxValue) {
    maxValue = newMaxValue;
  }

  public static float getValue() {
    return Mathf.Clamp(Map(axis.value, minValue, maxValue, 0, 1), 0, 1);
  }

  public static float Map(this float value, float from1, float to1, float from2, float to2) {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
  }

  public static void setAxis(AxisControl ac) {
    axis = ac;
  }
}
