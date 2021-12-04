using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    public GameObject cam;
    public Computation script;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        script = cam.GetComponent<Computation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.tag == "Right")
        {
            script.rightcount++;
            other.tag = "R";
        }
        else if (this.tag == "Left")
        {
            script.leftcount++;
            other.tag = "L";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (this.tag == "Right")
        {
            script.rightcount--;
        }
        else if (this.tag == "Left")
        {
            script.leftcount--;
        }
    }


}
