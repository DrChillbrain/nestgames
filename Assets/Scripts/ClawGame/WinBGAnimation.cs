using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBGAnimation : MonoBehaviour
{
    [SerializeField] private float rot = 15;
    [SerializeField] private float delay = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rot);
        StartCoroutine(leftRot());
    }

    IEnumerator leftRot()
    {
        yield return new WaitForSeconds(delay);
        transform.rotation = Quaternion.Euler(0, 0, -rot);
        StartCoroutine(rightRot());
    }

    IEnumerator rightRot()
    {
        yield return new WaitForSeconds(delay);
        transform.rotation = Quaternion.Euler(0, 0, rot);
        StartCoroutine(leftRot());
    }
}
