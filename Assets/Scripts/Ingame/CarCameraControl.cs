using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cartransform;
    public float cameraposy;
    public float cameraposz;
    


    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(cartransform.position.x + 9, cameraposy, cameraposz);
    }
}
