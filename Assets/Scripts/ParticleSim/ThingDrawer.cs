using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingDrawer : MonoBehaviour
{
  [Header("Controls")]
  [SerializeField] private float xRange;
  [SerializeField] private float yRange;
  [SerializeField] private GameObject starParticle;
  private GameObject currentStar;
  [SerializeField] private float maxStarSize;
  [SerializeField] private float maxScaleSpeed;

  [Header("Sound")]
  [SerializeField] private AudioSource loop;
  [SerializeField] private AudioClip loopEnd;
  [SerializeField] private AudioSource loopFalloff;
  private float loopVolume;
  private float falloffVolume;

  void FixedUpdate() {
    if (AnalogueInput.getValue() > 0.05) {
      if (currentStar == null) {
        currentStar = Instantiate(starParticle, new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange)), Quaternion.identity);
      }
      if (currentStar.transform.localScale.x < maxStarSize) {
        currentStar.transform.localScale = new Vector2(currentStar.transform.localScale.x + maxScaleSpeed * AnalogueInput.getValue(),
                                                       currentStar.transform.localScale.y + maxScaleSpeed * AnalogueInput.getValue());
      } else {
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(2.4f, 2.4f), maxScaleSpeed);
      }

      if (loopVolume < 0.5) {
        loopVolume += 0.10f * AnalogueInput.getValue();
        loop.volume = loopVolume;
      }
      
    } else {
      if (currentStar != null) {
        if (loopVolume > 0.1f) {
          loopFalloff.Play();
          falloffVolume = loopVolume;
        }
        loop.volume = 0;
        loopVolume = 0;
        //AudioSource.PlayClipAtPoint(loopEnd, new Vector3(0, 0, 0), loopVolume);
        ParticleSystem ps = currentStar.GetComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        StartCoroutine(destroyStar(currentStar));
        currentStar = null;
      }
    }

    if(falloffVolume > 0) {
      falloffVolume -= 0.01f;
      loopFalloff.volume = falloffVolume;
    } else {
      loopFalloff.Stop();
    }
  }

  IEnumerator destroyStar(GameObject star) {
    yield return new WaitForSeconds(3.0f);
    Destroy(star);
  }
}
