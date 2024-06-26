using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public static class AnalogueInput {
  public static float minValue = 0.3687786f;
  public static float maxValue = 0.0f;
  private static AxisControl axis;

  public static float getValue() {
    return Mathf.Clamp(Map(axis.value, minValue, maxValue, 0, 1), 0, 1);
  }

  public static float Map(this float value, float from1, float to1, float from2, float to2) {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
  }

  public static void setAxis(AxisControl ac) {
    axis = ac;
  }

  public static AxisControl getAxis() {
    return axis;
  }
}
