using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverControl : MonoBehaviour
{

    public CanvasGroup gameoverOverlay;
    public CanvasGroup mainmenugameover;
    public CanvasGroup youwinOverlay;
    public CanvasGroup finalscorepanel;
    public CanvasGroup mainmenuwin;
    public CanvasGroup skullborder;

    GameObject cam;
    SaveControl savecontrol;


    public void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        savecontrol = cam.GetComponent<SaveControl>();
    }
    // Start is called before the first frame update
    public void GameOver()
    {

        gameoverOverlay.alpha = 1f;
        gameoverOverlay.blocksRaycasts = true;

        mainmenugameover.alpha = 1f;
        mainmenugameover.blocksRaycasts = true;

        //finalscorepanel.alpha = 1f;
        //finalscorepanel.blocksRaycasts = true;

        skullborder.alpha = 0f;
        skullborder.blocksRaycasts = false;

        int nraces = PlayerPrefs.GetInt("TotalRaces");

        nraces++;

        PlayerPrefs.SetInt("TotalRaces", nraces);

        int ngameovers = PlayerPrefs.GetInt("TotalGameOvers");

        ngameovers++;

        PlayerPrefs.SetInt("TotalGameOvers", ngameovers);


        PlayerPrefs.Save();


    }

    public void YouWin(int obstaclescore, float timestart, float timeend, out int finalscore)
    {

        youwinOverlay.alpha = 1f;
        youwinOverlay.blocksRaycasts = true;

        mainmenuwin.alpha = 1f;
        mainmenuwin.blocksRaycasts = true;

        finalscorepanel.alpha = 1f;
        finalscorepanel.blocksRaycasts = true;

        skullborder.alpha = 0f;
        skullborder.blocksRaycasts = false;

        float time = timeend - timestart;

        int timeint = Mathf.RoundToInt(time);

        if (time < 240)
        {
            finalscore = obstaclescore + (1000 - timeint * 4);


        }
        else
        {
            finalscore = obstaclescore;
        }

        if (finalscore > PlayerPrefs.GetInt("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", finalscore);
        }

        int nraces = PlayerPrefs.GetInt("TotalRaces");

        nraces++;

        PlayerPrefs.SetInt("TotalRaces", nraces);

        int nwins = PlayerPrefs.GetInt("TotalWins");

        nwins++;

        PlayerPrefs.SetInt("TotalWins", nwins);

        PlayerPrefs.Save();

        Debug.Log("total races: " + nraces);

        



    }




}
