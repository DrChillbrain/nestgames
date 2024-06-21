using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private Sprite[] numbers;
    [SerializeField] private Image[] numCoords;
    private int time = -1;

    public void setTime(int time)
    {
        if (this.time == -1)
        {
            this.time = time;
            updateSprites();
            StartCoroutine(tickTimer());
        }
    }

    private void updateSprites()
    {
        numCoords[0].sprite = numbers[(time / 60) / 10];
        numCoords[1].sprite = numbers[(time / 60) % 10];
        numCoords[2].sprite = numbers[(time % 60) / 10];
        numCoords[3].sprite = numbers[(time % 60) % 10];
    }

    IEnumerator tickTimer()
    {
        yield return new WaitForSeconds(1.0f);
        time--;

        if (time <= 0)
        {
            GameManager.ptr.endGame();
        }

        updateSprites();
        StartCoroutine(tickTimer());
    }
}
