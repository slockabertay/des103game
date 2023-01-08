using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    //code from lecture 5: Moving platforms 
    //I added a way to calculate the velocity so I can add it to the player to keep them on it when it moves

    public float movementSpeed = 0.1f;
    [SerializeField] public float maxRight = 7f;
    [SerializeField] public float maxLeft = 7f;
    float startX = 0;
    bool movingRight = true;
    bool isWaiting = false;
    public float waitingTime = 100;
    public float timeWaited = 0;
    public Vector3 position;
    public static Vector2 velocity; 

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        

        if (isWaiting == false)
        {
            MovePlatform();

        }
        else if (isWaiting == true)
        {
            UpdateWaitingStuff();
        }
        velocity = (transform.position - position) / Time.deltaTime;
        position = transform.position;
        CheckMovingDirection();
    }

    void UpdateWaitingStuff()
    {
        timeWaited = timeWaited + 1;
        if (timeWaited >= waitingTime)
        {
            timeWaited = 0;
            isWaiting = false;
        }
    }

    void MovePlatform()
    {
        //moves toward destination
        if (movingRight == true)
        {            
            transform.Translate(movementSpeed, 0, 0);
            transform.localPosition += new Vector3(0.01f, 0, 0);
        }
        else
        {
            transform.Translate(-movementSpeed, 0, 0);
            transform.localPosition += new Vector3(-0.01f, 0, 0);
        }
    }

    void CheckMovingDirection()
    {
        if (transform.position.x <= (startX - maxLeft))
        {
            if (movingRight == false)
            {
                isWaiting = true;
            }
            movingRight = true;

        }
        else if (transform.position.x > (startX + maxRight))
        {
            if (movingRight == true)
            {
                isWaiting = true;
            }

            movingRight = false;

        }
    }
}
