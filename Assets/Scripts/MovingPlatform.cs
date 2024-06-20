using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveTowards;
    Vector2 startPos;
    Vector2 goal;
    bool returnToStart;

    private void Start()
    {
        startPos = transform.position;
        moveTowards = (Vector2)transform.position + moveTowards;
    }

    public void Update()
    {
        if (returnToStart)
        {
            goal = startPos;
        }
        else 
        {
            goal = moveTowards;            
        }

        if ((Vector2)transform.position == goal)
        { 
            returnToStart = !returnToStart;
        }


        transform.position = Vector2.MoveTowards(transform.position, goal, 1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("MovingPlatform");
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
