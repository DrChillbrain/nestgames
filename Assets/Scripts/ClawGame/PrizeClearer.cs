using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeClearer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float moveAccel = 0;
    private bool leave;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed /= 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (leave) {
            if (moveAccel < 1)
            {
                moveAccel += 0.02f;
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - (moveSpeed * moveAccel));

            if (transform.position.y < -7)
            {
                Destroy(gameObject);
            }
        }
    }

    public void startLeave()
    {
        leave = true;
    }
}
