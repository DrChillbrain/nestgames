using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaController : MonoBehaviour
{
    [Header("GoalEnds")]
    [SerializeField] private GameObject goalTop;
    [SerializeField] private GameObject goalBottom;

    //Range of area
      //Buffer of 0.05 at scale 1 for scale differences
      //Buffer of 0.05 for goal top or bottom

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        normalizeGoal();
        DebugScaleTest();
        DebugPosTest();
    }

    private void DebugPosTest()
    {

    }

    private void DebugScaleTest()
    {

    }

    //Makes sure that the top and bottom of the goal are properly set
    [ContextMenu("Normalize Goal")]
    private void normalizeGoal()
    {
        //Range is roughly between 1 and 3.5 for the yScale
        goalTop.transform.position = new Vector2(transform.position.x, transform.position.y + transform.localScale.y - (1 - (transform.localScale.y - 1) / 50));
        goalBottom.transform.position = new Vector2(transform.position.x, transform.position.y - transform.localScale.y + (1 - (transform.localScale.y - 1) / 50));
    }
}
