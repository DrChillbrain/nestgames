using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = 32)]
struct NestControllerInputReport : IInputStateTypeInfo {
  // Because all HID input reports are tagged with the 'HID ' FourCC,
  // this is the format we need to use for this state struct.
  public FourCC format => new FourCC('H', 'I', 'D');

  // X & Y
  [InputControl(name = "leftStick", layout = "Stick", format = "VEC2", sizeInBits = 32)]
  
  //[InputControl(name = "leftStick/x", offset = 0, format = "SHRT", sizeInBits = 16,
  //     parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=-1,clampMax=1")]
  //[InputControl(name = "leftStick/left", offset = 0, format = "BYTE",
  //    parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=-1,clampMax=0.0,invert")]
  //[InputControl(name = "leftStick/right", offset = 0, format = "BYTE",
  //    parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=0.0,clampMax=1")]
  
  [InputControl(name = "leftStick/y", offset = 3, format = "SHRT", sizeInBits = 16)]//,
      // parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=-1,clampMax=1")]
  //[InputControl(name = "leftStick/down", offset = 3, format = "BYTE",
  //    parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=0,clampMax=1")]
  //[InputControl(name = "leftStick/up", offset = 3, format = "BYTE",
  //    parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.0,clamp,clampMin=-1,clampMax=0,invert")]
  
  [FieldOffset(2)] public byte leftStickX;
  [FieldOffset(4)] public byte leftStickY;
}

[InputControlLayout(stateType = typeof(NestControllerInputReport))]
#if UNITY_EDITOR
[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
public class ControllerConfig : Gamepad
{
  static ControllerConfig() {
    InputSystem.RegisterLayout<ControllerConfig>(
        matches: new InputDeviceMatcher()
            .WithInterface("HID")
            .WithCapability("vendorId", 0x2E5)
            .WithCapability("productId", 0xABBB));
  }

  // In the Player, to trigger the calling of the static constructor,
  // create an empty method annotated with RuntimeInitializeOnLoadMethod.
  [RuntimeInitializeOnLoadMethod]
  static void Init() { }
}