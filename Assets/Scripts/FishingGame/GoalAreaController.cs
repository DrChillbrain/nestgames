using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoalAreaController : MonoBehaviour {
  [Header("Goal Ends")]
  [SerializeField] private GameObject goalTop;
  [SerializeField] private GameObject goalBottom;

  [Header("Goal Controller Variables")]
  [SerializeField] private float timeBetweenChance;
  [SerializeField] private float chanceToChangeDirection;
  [SerializeField] private float moveSpeed;

  private float debugScale = 3;
  private int debugScaleTick;

  private float moveDirection = 1;

  //Range of area
    //Buffer of 0.05 at scale 1 for scale differences
    //Buffer of 0.05 for goal top or bottom

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    normalizeGoal();
    //DebugScaleTest();
    DebugPosTest();
  }

  private void DebugPosTest()
  {
    transform.position = new Vector2(transform.position.x, transform.position.y + (0.025f * moveDirection));

    if(Mathf.Abs(transform.position.y) > (4 - transform.localScale.y) - (0.05 * (transform.localScale.y + 1))) {
      moveDirection *= -1;
    }
  }

  private void DebugScaleTest()
  {
    transform.localScale = new Vector2(transform.localScale.x, debugScale);
    debugScaleTick++;
    if (debugScaleTick > 60) {
      debugScaleTick = 0;
      debugScale -= 0.1f;
    }
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
