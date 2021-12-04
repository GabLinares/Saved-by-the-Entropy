using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
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
        int npart;
        npart = System.Convert.ToInt32(other.name);

        if(this.tag == "side")
        {
            script.v0[npart, 0] = -1 * script.v0[npart, 0];
        }
        if (this.tag == "topdown")
        {
            script.v0[npart, 1] = -1 * script.v0[npart, 1];
        }



    }
}
