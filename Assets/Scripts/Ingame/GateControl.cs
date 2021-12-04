using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam;
    public Computation script;
    public bool locked;
    [SerializeField] private Material gatemat;
    public Sprite spriteon;
    public Sprite spriteoff;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        script = cam.GetComponent<Computation>();
        locked = true;
        GameObject gatebutton;
        gatebutton = GameObject.FindGameObjectWithTag("GateButton");
        Image theimage = gatebutton.GetComponent<Image>();
        theimage.sprite = spriteoff;
    }

    // Update is called once per frame
    void Update()
    {
        ColorCheck(locked);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (locked == true)
        {
            int npart;
            npart = System.Convert.ToInt32(other.name);

            script.v0[npart, 0] = -1 * script.v0[npart, 0];
        }

    }

    void ColorCheck(bool locked)
    {
        if (locked == true)
        {
            gatemat.color = Color.black;
             
        }
        else
        {
            gatemat.color = Color.green;
        }
    }

    public void ChangeGate()
    {
        GameObject gatebutton;
        gatebutton = GameObject.FindGameObjectWithTag("GateButton");
        Image theimage = gatebutton.GetComponent<Image>();
        if (this.locked == true)
        {
            this.locked = false;
            theimage.sprite = spriteon;
        }
        else
        {
            this.locked = true;
            theimage.sprite = spriteoff;
        }
    }
}
