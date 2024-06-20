using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrizeDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private PrizeDisplayer prizeDisplayer;

    // Checks for if prizes are within crane's grip
    public void checkForPrizes()
    {
        trigger.enabled = true;
        StartCoroutine(Defs.delay(0.1f, () =>
        {
            trigger.enabled = false;
        }));
    }

    // Trigger detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prize")
        {
            prizeDisplayer.winPrize(other.gameObject);
            trigger.enabled = false;
        }
    }
}
