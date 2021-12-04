using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    GameObject playerCar;
    CarControl script;
    Rigidbody2D CarRb;

    void Start()
    {
        playerCar = GameObject.FindWithTag("PlayerCar");
        script = playerCar.GetComponent<CarControl>();
        CarRb = playerCar.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
