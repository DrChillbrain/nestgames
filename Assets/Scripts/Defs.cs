using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Defs
{
    //Delay coroutine with an action, can be customized with a lambda
    public static IEnumerator delay(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
