using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControl : MonoBehaviour
{
    GameObject car;
    CarControl script;

    public Transform cartransform;
    public Transform monstertransform;
    public float minimapy;
    public float minimapz;
    public GameObject carIcon;
    public GameObject monsterIcon;
    


    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindWithTag("PlayerCar");
        script = car.GetComponent<CarControl>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(cartransform.position.x + 8.26f, minimapy, minimapz);
        carIcon.transform.position = new Vector3(cartransform.position.x + 8.26f + cartransform.position.x/(script.tilewidth*script.maxtile)*30f-15f, minimapy, minimapz - 0.01f);
        monsterIcon.transform.position = new Vector3(cartransform.position.x + 8.26f + (monstertransform.position.x -15f) /(script.tilewidth * script.maxtile)*30f - 15f, minimapy, minimapz - 0.01f);
    }
}
