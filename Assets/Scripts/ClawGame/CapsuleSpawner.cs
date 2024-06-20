using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] private GameObject capsule;
    [SerializeField] private Sprite[] prizes;

    [Header("SpawningInfo")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private int spawnCenter;
    [SerializeField] private int spawnVariance;

    // Start is called before the first frame update
    void Start()
    {
        int spawnAmount = Random.Range(spawnCenter - spawnVariance, spawnCenter + spawnVariance + 1);

        //Make sure at least one of each animal spawns
        for (int i = 0; i < prizes.Length; i++)
        {
            spawnCapsule(i);
        }

        //Other random prizes
        for (int i = 0; i < spawnAmount; i++)
        {
            spawnCapsule(Random.Range(0, prizes.Length));
        }
    }

    private void spawnCapsule(int prizeIndex)
    {
        float xPos = Random.Range(xMin, xMax);
        float yPos = Random.Range(yMin, yMax);

        GameObject current = Instantiate(capsule, new Vector2(xPos, yPos), Quaternion.identity);
        current.GetComponent<SpriteRenderer>().sprite = prizes[prizeIndex];
    }
}
