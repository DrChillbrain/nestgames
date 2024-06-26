using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoalAreaController : MonoBehaviour {
  [Header("Goal Ends")]
  [SerializeField] private GameObject goalTop;
  [SerializeField] private GameObject goalBottom;

  [Header("Goal Controller Variables")]
  [Tooltip("In Seconds")]
  [SerializeField] private float timeBetweenChance;
  [Tooltip("0.34 = 34%")]
  [SerializeField] private float chanceToChangeDirection;
  [SerializeField] private float moveSpeed;
  [SerializeField] private float size;

  [Header("Difficulty Changes")]
  [SerializeField] private float difficultySpeedIncrease;
  [SerializeField] private float difficultyScaleDecrease;
  [SerializeField] private float difficultyTimeChanceDecrease;

  [Header("MaxDifficulty")]
  [SerializeField] private float maxSpeed;
  [SerializeField] private float minSize;
  [SerializeField] private float minTimeChance;

  private float moveAccel;
  private bool moveUp;

  // Start is called before the first frame update
  void Start()
  {
    moveSpeed /= 100;
    difficultySpeedIncrease /= 100;
    StartCoroutine(changeDirectionCheck());
  }

  void OnEnable() {
    goalTop.SetActive(true);
    goalBottom.SetActive(true);
    StartCoroutine(changeDirectionCheck());
  }

  void OnDisable() {
    goalTop.SetActive(false);
    goalBottom.SetActive(false);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    move();
    normalizeGoal();
  }

  //Increases difficulty after fish is caught
  public void increaseDifficulty() {
    moveSpeed += difficultySpeedIncrease;
    moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
    transform.localScale = new Vector2(transform.localScale.x, Mathf.Max(transform.localScale.y - difficultyScaleDecrease, minSize));
    timeBetweenChance -= difficultyTimeChanceDecrease;
    timeBetweenChance = Mathf.Max(timeBetweenChance, minTimeChance);
  }

  //Randomly changes direction of movement
  IEnumerator changeDirectionCheck() {
    if (Random.Range(0.0f, 1.0f) >= chanceToChangeDirection) {
      moveUp = !moveUp;
    }

    yield return new WaitForSeconds(timeBetweenChance);
    StartCoroutine(changeDirectionCheck());
  }

  //Move bar
  private void move() {
    //Adjust acceleration
    float targetAccel = (moveUp) ? 1.0f : -1.0f;

    if (targetAccel * moveAccel < 1.0) {
      moveAccel += 0.05f * targetAccel;
    }

    //Update position
    transform.position = new Vector2(transform.position.x, transform.position.y + (moveSpeed * moveAccel));

    //Clamp to edges
    float edge = (4 - transform.localScale.y) - (0.05f * (transform.localScale.y + 1));
    transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -edge, edge));
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
