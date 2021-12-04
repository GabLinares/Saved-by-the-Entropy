using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialControl : MonoBehaviour
{
    //TextMeshProUGUI textcontent;

    public int currentpage;

    string[] text1;
    string text1eng = "In this game you have to take the car to the goal before the monster reaches it.";
    string text1cat = "En aquest joc has de fer arribar el cotxe fins la meta abans que el monstre el pugui agafar.";
    string text1cast = "En este juego debes hacer llegar el coche hasta la meta antes de que el monstruo te alcance.";
    string text1ita = "In questo gioco devi far arrivare la macchina fino alla meta prima che il mostro ti raggiunga.";
    public GameObject text1prefab;

    string[] text2;
    string text2eng = "In order to make it, you will have to sort the particles taking into account their velocities, represented by their colors: blue for the slower ones and red for the fastests.";
    string text2cat = "Per a aconseguir-ho, haur�s de separar les part�cules entre els dos compartiments segons les seves velocitats, representades pel seu color: blau per les m�s lentes i vermell per les r�pides.";
    string text2cast = "Para ello, deber�s separar las part�culas entre los dos compartimentos seg�n sus velocidades, representadas por su color: azul para las m�s lentas y rojo para las r�pidas.";
    string text2ita = "Per fare ci�, devi separare le particelle tra i due scompartimenti a seconda delle velocit�, rappresentate dal proprio colore: blu per quelle pi� lente e rosso per quelle pi� veloci.";
    public GameObject text2prefab;

    string[] text3;
    string text3eng = "In the upper signs is shown the temperature on each compartment. The greater the difference between them, the more energy you will be able to pass to the car.";
    string text3cat = "En els marcadors superiors es mostra la temperatura en cada compartiment. Podr�s transmetre m�s energia al cotxe aconseguint que la difer�ncia de temperatura entre ells sigui prou gran.";
    string text3cast= "En los marcadores superiores se muestra la temperatura en cada compartimento. Contra m�s grande sea la diferencia entre ellas, m�s energ�a podr�s transmitirle al coche.";
    string text3ita = "Negli indicatori superiori si mostra la temperatura in ogni scompartimento. Pi� grande � la differenza tra di loro, pi� energia potrai trasmettere alla macchina.";
    public GameObject text3prefab;

    string[] text4;
    string text4eng = "Pressing the speedometer button when is shown in green will turn on the car engine and the particles will be reset.";
    string text4cat = "Prement el bot� del veloc�metre quan estigui de color verd encendr�s el motor del cotxe i les part�cules es reiniciaran.";
    string text4cast = "Pulsando el bot�n del veloc�metro cuando se muestre de color verde encender�s el motor del coche y las part�culas se reiniciar�n.";
    string text4ita = "Pulsando il bottone del misuratore di velocit� quando � verde accendi il motore della macchina e le particelle rinizieranno.";
    public GameObject text4prefab;

    string[] text5;
    string text5eng = "Once done, you will have to control the vehicle, trying to prevent it from colliding to the different obstacles";
    string text5cat = "Una vegada fet aix�, haur�s de controlar el vehicle per tal d'evitar que impacti amb els diferents obstacles";
    string text5cast = "Una vez hecho esto, tendr�s que controlar el veh�culo para evitar que colisione con los diferentes obst�culos.";
    string text5ita = "Una volta fatto tutto ci�, dovrai controllare il veicolo per evitare che si scontri con i differenti ostacoli.";
    public GameObject text5prefab;

    string[] text6;
    string text6eng = "Reaching the goal fast will grant you more points. In addition, you can obtain further points by collecting the rubies in the road!";
    string text6cat = "Arribar r�pid a la meta et proporcionar� m�s punts. A m�s a m�s, pots aconseguir punts extra recollint els robins que trobar�s pel cam�.";
    string text6cast = "Llegar r�pido a la meta te proporcionar� m�s puntos. Adem�s, puedes conseguir puntos extra recogiendo alg�n rub� por el camino.";
    string text6ita = "Arrivare velocemente alla meta ti dar� pi� punti. Inoltre, puoi ottenere punti extra raccogliendo qualche rubino per il cammino. ";
    public GameObject text6prefab;

    string[] text7;
    string text7eng = "Earning certain achivements will allow you to unlock additional skins for your car.\nThere are a total of 6 skins available!";
    string text7cat = "Completant certes fites podr�s desbloquejar aspectes addicionals per al teu vehicle.\nHi ha 6 aspectes en total disponibles! ";
    string text7cast = "Completando ciertos logros podr�s desbloquear aspectos adicionales para tu veh�culo.\n�Hay 6 aspectos en total disponibles!";
    string text7ita = "Completando determinati risultati potrai sbloccare ulteriori aspetti per il tuo veicolo.\nCi sono 6 aspetti disponibili in totale! ";
    public GameObject text7prefab;

    GameObject cam;
    TutorialControl script;

    GameObject carobject;
    SpriteRenderer carImage;
    bool carcolor;

    double timer;

    GameObject yellowarrowobject;
    SpriteRenderer yellowarrowImage;

    public GameObject tutorialparticlesprefab;



    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        currentpage = 0;
        text1 = new string[] { text1eng, text1cat, text1cast, text1ita };
        text2 = new string[] { text2eng, text2cat, text2cast, text2ita };
        text3 = new string[] { text3eng, text3cat, text3cast, text3ita };
        text4 = new string[] { text4eng, text4cat, text4cast, text4ita };
        text5 = new string[] { text5eng, text5cat, text5cast, text5ita };
        text6 = new string[] { text6eng, text6cat, text6cast, text6ita };
        text7 = new string[] { text7eng, text7cat, text7cast, text7ita };

        if(PlayerPrefs.GetInt("Tutorial") == 0 && this.tag == "MainCamera")
        {
            Debug.Log("camera tutorial");
            PlayTutorial();

            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();

            yellowarrowobject = GameObject.FindWithTag("YellowArrow");

            yellowarrowImage = yellowarrowobject.GetComponent<SpriteRenderer>();

            yellowarrowImage.enabled = false;


            carcolor = false;
            


        }

        if(this.tag != "MainCamera")
        {
            cam = GameObject.FindWithTag("MainCamera");
            script = cam.GetComponent<TutorialControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.tag != "MainCamera")
        {
            timer = timer + Time.deltaTime;

            if (script.currentpage == 1)
            {
                HideParticles();
            }


            if (script.currentpage == 1 && timer > 0.6f)
            {
                carobject = GameObject.FindWithTag("PlayerCar");

                carImage = carobject.GetComponent<SpriteRenderer>();

                yellowarrowobject = GameObject.FindWithTag("YellowArrow");

                yellowarrowImage = yellowarrowobject.GetComponent<SpriteRenderer>();

                switch (carcolor)
                {
                    case false:
                        carImage.color = Color.yellow;
                        carcolor = true;
                        yellowarrowImage.enabled = !yellowarrowImage.enabled;
                        break;
                    case true:
                        carImage.color = Color.white;
                        carcolor = false;
                        yellowarrowImage.enabled = !yellowarrowImage.enabled;
                        break;
                }

                

                //carImage.sprite.color.yellow;
                timer = 0;
            }
            else if(script.currentpage ==2 && timer > 0.5)
            {
                carobject = GameObject.FindWithTag("PlayerCar");

                carImage = carobject.GetComponent<SpriteRenderer>();

                yellowarrowobject = GameObject.FindWithTag("YellowArrow");

                yellowarrowImage = yellowarrowobject.GetComponent<SpriteRenderer>();

                carImage.color = Color.white;
                carcolor = false;
                yellowarrowImage.enabled = false;

                timer = 0;
            }
            else if (script.currentpage == 3 && timer > 0.5)
            {
                timer = 0;
            }
            else if (script.currentpage == 4 && timer > 0.5)
            {
                timer = 0;
            }
            else if (script.currentpage == 5 && timer > 0.5)
            {
                timer = 0;
            }
            else if (script.currentpage == 6 && timer > 0.5)
            {
                timer = 0;

            }
            else if (script.currentpage == 7 && timer > 0.5)
            {
                timer = 0;

            }
            else if(timer > 500)
            {
                timer = 0;
            }
        }
    }

    public void PlayTutorial()
    {
        int lang = PlayerPrefs.GetInt("Language");

        GameObject canvasobject = GameObject.FindWithTag("Canvas");

        int globalcurrentpage;

        if(this.tag == "MainCamera")
        {
            globalcurrentpage = currentpage;
        }
        else
        {
            globalcurrentpage = script.currentpage;
        }

        switch (globalcurrentpage)
        {
            case 0:
                Instantiate(text1prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial1 = GameObject.FindWithTag("Tutorial1");     

                tutorial1.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform1 = tutorial1.GetComponent<RectTransform>();
                Transform textbox1 = texttransform1.GetChild(0);
                TextMeshProUGUI textcontent1 = textbox1.GetComponent<TextMeshProUGUI>();

                textcontent1.text = text1[lang];

                currentpage++;
                break;
            case 1:
                tutorial1 = GameObject.FindWithTag("Tutorial1");
                Destroy(tutorial1);

                Instantiate(text2prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial2 = GameObject.FindWithTag("Tutorial2");

                tutorial2.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform2 = tutorial2.GetComponent<RectTransform>();
                Transform textbox2 = texttransform2.GetChild(0);
                TextMeshProUGUI textcontent2 = textbox2.GetComponent<TextMeshProUGUI>();

                texttransform2.anchoredPosition = new Vector3(0, 800, 0);

                textcontent2.text = text2[lang];

                GameObject.FindWithTag("TutorialParticles").transform.GetChild(0).gameObject.SetActive(true);


                script.currentpage++;
                break;
            case 2:
                tutorial2 = GameObject.FindWithTag("Tutorial2");
                Destroy(tutorial2);

                Instantiate(text3prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial3 = GameObject.FindWithTag("Tutorial3");

                tutorial3.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform3 = tutorial3.GetComponent<RectTransform>();
                Transform textbox3 = texttransform3.GetChild(0);
                TextMeshProUGUI textcontent3 = textbox3.GetComponent<TextMeshProUGUI>();

                textcontent3.text = text3[lang];

                script.currentpage++;
                break;
            case 3:
                tutorial3 = GameObject.FindWithTag("Tutorial3");
                Destroy(tutorial3);

                Instantiate(text4prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial4 = GameObject.FindWithTag("Tutorial4");

                tutorial4.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform4 = tutorial4.GetComponent<RectTransform>();
                Transform textbox4 = texttransform4.GetChild(0);
                TextMeshProUGUI textcontent4 = textbox4.GetComponent<TextMeshProUGUI>();

                textcontent4.text = text4[lang];

                script.currentpage++;
                break;
            case 4:
                tutorial4 = GameObject.FindWithTag("Tutorial4");
                Destroy(tutorial4);

                Instantiate(text5prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial5 = GameObject.FindWithTag("Tutorial5");

                tutorial5.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform5 = tutorial5.GetComponent<RectTransform>();
                Transform textbox5 = texttransform5.GetChild(0);
                TextMeshProUGUI textcontent5 = textbox5.GetComponent<TextMeshProUGUI>();

                textcontent5.text = text5[lang];

                script.currentpage++;
                break;
            case 5:
                tutorial5 = GameObject.FindWithTag("Tutorial5");
                Destroy(tutorial5);

                Instantiate(text6prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial6 = GameObject.FindWithTag("Tutorial6");

                tutorial6.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform6 = tutorial6.GetComponent<RectTransform>();
                Transform textbox6 = texttransform6.GetChild(0);
                TextMeshProUGUI textcontent6 = textbox6.GetComponent<TextMeshProUGUI>();

                textcontent6.text = text6[lang];

                script.currentpage++;
                break;
            case 6:
                tutorial6 = GameObject.FindWithTag("Tutorial6");
                Destroy(tutorial6);

                Instantiate(text7prefab, new Vector3(0, 0, 0), Quaternion.identity);

                GameObject tutorial7 = GameObject.FindWithTag("Tutorial7");

                tutorial7.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

                RectTransform texttransform7 = tutorial7.GetComponent<RectTransform>();
                Transform textbox7 = texttransform7.GetChild(0);
                TextMeshProUGUI textcontent7 = textbox7.GetComponent<TextMeshProUGUI>();

                textcontent7.text = text7[lang];

                script.currentpage++;
                break;
            case 7:
                tutorial7 = GameObject.FindWithTag("Tutorial7");
                Destroy(tutorial7);
                GameObject.FindWithTag("TutorialParticles").transform.GetChild(0).gameObject.SetActive(false);
                ShowParticles();
                break;
        }

        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.Save();
    }

    void HideParticles()
    {
        GameObject camx;
        Computation scriptx;
        GameObject particleobjectx;
        SpriteRenderer particlexImage;
        SpriteRenderer gateImage;
        GameObject gateobjectx;

        camx = GameObject.FindWithTag("MainCamera");
        scriptx = camx.GetComponent<Computation>();

        gateobjectx = GameObject.Find("Gate");
        gateImage = gateobjectx.GetComponent<SpriteRenderer>();
        gateImage.enabled = false;

        for (int i = 0; i < scriptx.N; i++)
        {
            particleobjectx = GameObject.Find(i.ToString());
            particlexImage = particleobjectx.GetComponent<SpriteRenderer>();
            particlexImage.enabled = false;

        }
    }


    void ShowParticles()
    {
        GameObject camx;
        Computation scriptx;
        GameObject particleobjectx;
        SpriteRenderer particlexImage;
        SpriteRenderer gateImage;
        GameObject gateobjectx;

        camx = GameObject.FindWithTag("MainCamera");
        scriptx = camx.GetComponent<Computation>();

        gateobjectx = GameObject.Find("Gate");
        gateImage = gateobjectx.GetComponent<SpriteRenderer>();
        gateImage.enabled = true;
        


        for (int i = 0; i < scriptx.N; i++)
        {
            particleobjectx = GameObject.Find(i.ToString());
            particlexImage = particleobjectx.GetComponent<SpriteRenderer>();
            particlexImage.enabled = true;

        }
    }
    /*public void NextPageText()
    {
        RectTransform texttransform = GetComponent<RectTransform>();
        Transform textbox = texttransform.GetChild(0);
        textcontent = textbox.GetComponent<TextMeshProUGUI>();
        int Npages = textcontent.textInfo.pageCount;



        if (currentpage < Npages)
        {
            currentpage++;
            textcontent.pageToDisplay++;

            Debug.Log(currentpage);
        }

    }*/
}
