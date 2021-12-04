using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterControl : MonoBehaviour
{

    GameObject car;
    CarControl carscript;
    Transform carTransform;

    GameObject monsterAlert;
    Transform monsterAlertTransform;

    Rigidbody2D monsterRb;

    public float monsterSpeed;

    public bool monstercheck;

    double timer;

    bool goingup;
    bool staying;
    bool goingdown;
    bool posreached;
    bool firstcheck;

    public TMP_Text monstertext;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindWithTag("PlayerCar");
        carscript = car.GetComponent<CarControl>();
        carTransform = car.GetComponent<Transform>();

        monsterRb = GetComponent<Rigidbody2D>();

        monsterAlert = GameObject.FindWithTag("MonsterAlert");
        monsterAlertTransform = monsterAlert.GetComponent<Transform>();

        monstercheck = false;

        timer = 0;

        goingup = false;
        staying = false;
        goingdown = false;
        posreached = true;
        firstcheck = false;

        int lang = PlayerPrefs.GetInt("Language");

        string[] monsterlist = new string[] { "The monster is coming...", "El monstre està en camí...", "El monstruo está en camino...", "Il mostro sta arrivando..." };

        monstertext.text = monsterlist[lang];

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(carscript.MonsterStart == true)
        {

            monsterRb.velocity = new Vector2(monsterSpeed, 0);


        }
        else if (carscript.MonsterStart == false)
        {
            monsterRb.velocity = new Vector2(0, 0);
        }

        if (carscript.MonsterStart == true && monstercheck == false && firstcheck == false)
        {
            goingup = true;
            monstercheck = false;
            posreached = false;
            firstcheck = true;
        }



        if (goingup == true && posreached == false)
        {
            float xpos = carTransform.position.x;
            float zpos = monsterAlertTransform.position.z;
            float ypos = monsterAlertTransform.position.y;


            Vector3 newpos = new Vector3(xpos +9f, ypos + 0.15f, zpos);

            monsterAlertTransform.position = newpos;

            if(ypos> -96.9f)
            {
                goingup = false;
                staying = true;

            }
            //Debug.Log("goingup");
        }

        if (staying == true && posreached == false && timer < 3)
        {
            timer = timer + Time.deltaTime;

            float xpos = carTransform.position.x;
            float zpos = monsterAlertTransform.position.z;
            float ypos = monsterAlertTransform.position.y;


            Vector3 newpos = new Vector3(xpos +9f, ypos, zpos);

            monsterAlertTransform.position = newpos;

            if (timer > 3f)
            {
                staying = false;
                goingdown = true;
            }

        }

        if (goingdown == true && posreached == false)
        {
            float xpos = carTransform.position.x;
            float zpos = monsterAlertTransform.position.z;
            float ypos = monsterAlertTransform.position.y;


            Vector3 newpos = new Vector3(xpos + 9f, ypos - 0.15f, zpos);

            monsterAlertTransform.position = newpos;

            if (ypos < -105f)
            {
                goingdown = false;
                posreached = true;
            }
        }


    }

    void AlertAnimation()
    {
        while (monsterAlertTransform.position.y < -96.9f)
        {
            float xpos = carTransform.position.x;
            float zpos = monsterAlertTransform.position.z;
            float ypos = monsterAlertTransform.position.y;


            Vector3 newpos = new Vector3(xpos +20.45f, ypos + 0.001f, zpos);

            monsterAlertTransform.position = newpos;
            Debug.Log("check1");

        }
        while (monsterAlertTransform.position.y == -96.9f && timer < 3)
        {
            timer = timer + Time.deltaTime;
            Debug.Log("check2");
        }
        while (monsterAlertTransform.position.y > -105f)
        {
            float xpos = carTransform.position.x;
            float zpos = monsterAlertTransform.position.z;
            float ypos = monsterAlertTransform.position.y;


            Vector3 newpos = new Vector3(xpos, ypos - 0.001f, zpos);

            monsterAlertTransform.position = newpos;
            Debug.Log("check3");
        }


        monstercheck = true;
    }
}
