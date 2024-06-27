using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

  [Header("Sound Effects")]
  [SerializeField] private AudioClip smallSplash;
  [SerializeField] private AudioClip largeSplash;
  [SerializeField] private AudioSource fishingReel;


  // Start is called before the first frame update
  void Start()
  {
    goalAreaController = goalArea.gameObject.GetComponent<GoalAreaController>();
    fishingReel.Stop();
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
        if (!fishingReel.isPlaying)
          fishingReel.Play();
      } else {
        catchProgress -= 1.0f * Time.deltaTime;
        catchProgress = Mathf.Max(catchProgress, 0.0f);
        if (fishingReel.isPlaying)
          fishingReel.Stop();
      }
    }

    //Update catch progress UI
    catchBar.setFill(catchProgress / timeUntilCatch);

    //Catch fish
    if (catchProgress >= timeUntilCatch) {
      GameObject fish = Instantiate(fishPrefab, new Vector3(0, -7, 0), Quaternion.identity);
      fish.GetComponent<FishController>().setInfo(fishCaught);
      fishCaught++;
      AudioSource.PlayClipAtPoint(largeSplash, new Vector3(0, 0, 0));
      if(fishingReel.isPlaying)
        fishingReel.Stop();
      goalAreaController.increaseDifficulty();
      goalArea.gameObject.SetActive(false);
      timeUntilCatch += timeUntilCatchDifficultyAdjust;
      catchProgress = 0;
      StartCoroutine(enableGoalArea());
    }
  }

  //Enable goal area after a delay (after catching)
  IEnumerator enableGoalArea() {
    yield return new WaitForSeconds(1f);
    goalArea.gameObject.SetActive(true);
    AudioSource.PlayClipAtPoint(smallSplash, new Vector3(0, 0, 0));
  }
}
