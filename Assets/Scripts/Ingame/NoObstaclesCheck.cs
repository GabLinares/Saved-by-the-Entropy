using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoObstaclesCheck : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Obstacle" || other.tag == "Obstacle2")
        {
            Destroy(other.gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Obstacle2")
        {
            Destroy(other.gameObject);
        }
    }
}
