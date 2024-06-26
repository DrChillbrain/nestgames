using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
  private Vector2 startPos;
  private Vector2 arrayPos;
  bool movingToPos;
  [SerializeField] private Sprite[] fishSprites;
  private float moveSpeed = 5;

  // Start is called before the first frame update
  void Start()
  {
    GetComponent<SpriteRenderer>().sprite = fishSprites[Random.Range(0, fishSprites.Length)];
    movingToPos = true;
    startPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (movingToPos) {
      transform.position = Vector2.Lerp(transform.position, arrayPos, moveSpeed * Time.deltaTime);
      transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.75f, 0.75f), moveSpeed * Time.deltaTime);
      if (Mathf.Approximately(transform.position.x, arrayPos.x) && Mathf.Approximately(transform.position.y, arrayPos.y)) {
        movingToPos = false;
      }
    }
  }

  public void setInfo(int pos) {
    //x is -3 + 1.5n if over 7
    //y is -4 + 1.333n
    arrayPos = new Vector2(-3 + (1.5f * (pos / 7)), -4 + (1.333f * (pos % 7)));
  }
}
