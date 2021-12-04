using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureTextControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text TempText;
    GameObject cam;
    Computation script;

    double timer;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        script = cam.GetComponent<Computation>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > 1 && script.collision > script.collisionlimit)
        {
            if(TempText.tag == "Left")
            {
                TempText.text = script.Tleft.ToString("F1");
            }

            if (TempText.tag == "Right")
            {
                TempText.text = script.Tright.ToString("F1");
            }

            timer = 0;
        }

        else if (script.collision < script.collisionlimit)
        {
            int lang = PlayerPrefs.GetInt("Language");

            string[] templist = new string[] { "Computing...", "Calculant...", "Calculando...", "Calcolando..." };

            TempText.text = templist[lang];

        }
    }
}
