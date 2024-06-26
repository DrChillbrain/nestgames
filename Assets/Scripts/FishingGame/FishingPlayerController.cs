using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingPlayerController : MonoBehaviour
{
  [SerializeField] private Transform goalArea;
  private GoalAreaController goalAreaController;
  [SerializeField] private ProgressBar catchBar;
  [SerializeField] private GameObject fishPrefab;

  [Header("Game Values")]
  [SerializeField] private float timeUntilCatch;
  [SerializeField] private float timeUntilCatchDifficultyAdjust;
  private float catchProgress;

  private int fishCaught;


  // Start is called before the first frame update
  void Start()
  {
    goalAreaController = goalArea.gameObject.GetComponent<GoalAreaController>();
  }

  // Update is called once per frame
  void Update()
  {
    //Update cursor position
    transform.position = new Vector2(transform.position.x,
      AnalogueInput.Map(AnalogueInput.getValue(), 0, 1, -3.35f, 3.35f));

    if (goalArea.gameObject.activeSelf) {
      //Check if in range of goal area
      if(transform.position.y > goalArea.position.y - (goalArea.localScale.y) &&
          transform.position.y < goalArea.position.y + (goalArea.localScale.y)) {
        catchProgress += 1.0f * Time.deltaTime;
      } else {
        catchProgress -= 1.0f * Time.deltaTime;
        catchProgress = Mathf.Max(catchProgress, 0.0f);
      }
    }

    //Update catch progress UI
    catchBar.setFill(catchProgress / timeUntilCatch);

    //Catch fish
    if (catchProgress >= timeUntilCatch) {
      GameObject fish = Instantiate(fishPrefab, new Vector3(0, -7, 0), Quaternion.identity);
      fish.GetComponent<FishController>().setInfo(fishCaught);
      fishCaught++;
      goalAreaController.increaseDifficulty();
      goalArea.gameObject.SetActive(false);
      timeUntilCatch += timeUntilCatchDifficultyAdjust;
      catchProgress = 0;
      StartCoroutine(enableGoalArea());
    }
  }

  //Enable goal area after a delay (after catching)
  IEnumerator enableGoalArea() {
    yield return new WaitForSeconds(0.5f);
    goalArea.gameObject.SetActive(true);
  }
}
