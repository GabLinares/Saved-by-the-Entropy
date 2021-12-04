using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextControl : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text TempText;
    GameObject cam;
    GameObject playerCar;
    
    CarControl carscript;


    void Start()
    {
        
        playerCar = GameObject.FindWithTag("PlayerCar");
        carscript = playerCar.GetComponent<CarControl>();

    }

    // Update is called once per frame
    void Update()
    {
        TempText.text = carscript.score.ToString("F0");
    }
}
