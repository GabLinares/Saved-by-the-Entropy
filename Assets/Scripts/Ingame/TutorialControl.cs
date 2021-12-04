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
    string text2cat = "Per a aconseguir-ho, hauràs de separar les partícules entre els dos compartiments segons les seves velocitats, representades pel seu color: blau per les més lentes i vermell per les ràpides.";
    string text2cast = "Para ello, deberás separar las partículas entre los dos compartimentos según sus velocidades, representadas por su color: azul para las más lentas y rojo para las rápidas.";
    string text2ita = "Per fare ciò, devi separare le particelle tra i due scompartimenti a seconda delle velocità, rappresentate dal proprio colore: blu per quelle più lente e rosso per quelle più veloci.";
    public GameObject text2prefab;

    string[] text3;
    string text3eng = "In the upper signs is shown the temperature on each compartment. The greater the difference between them, the more energy you will be able to pass to the car.";
    string text3cat = "En els marcadors superiors es mostra la temperatura en cada compartiment. Podràs transmetre més energia al cotxe aconseguint que la diferència de temperatura entre ells sigui prou gran.";
    string text3cast= "En los marcadores superiores se muestra la temperatura en cada compartimento. Contra más grande sea la diferencia entre ellas, más energía podrás transmitirle al coche.";
    string text3ita = "Negli indicatori superiori si mostra la temperatura in ogni scompartimento. Più grande è la differenza tra di loro, più energia potrai trasmettere alla macchina.";
    public GameObject text3prefab;

    string[] text4;
    string text4eng = "Pressing the speedometer button when is shown in green will turn on the car engine and the particles will be reset.";
    string text4cat = "Prement el botó del velocímetre quan estigui de color verd encendràs el motor del cotxe i les partícules es reiniciaran.";
    string text4cast = "Pulsando el botón del velocímetro cuando se muestre de color verde encenderás el motor del coche y las partículas se reiniciarán.";
    string text4ita = "Pulsando il bottone del misuratore di velocità quando è verde accendi il motore della macchina e le particelle rinizieranno.";
    public GameObject text4prefab;

    string[] text5;
    string text5eng = "Once done, you will have to control the vehicle, trying to prevent it from colliding to the different obstacles";
    string text5cat = "Una vegada fet això, hauràs de controlar el vehicle per tal d'evitar que impacti amb els diferents obstacles";
    string text5cast = "Una vez hecho esto, tendrás que controlar el vehículo para evitar que colisione con los diferentes obstáculos.";
    string text5ita = "Una volta fatto tutto ciò, dovrai controllare il veicolo per evitare che si scontri con i differenti ostacoli.";
    public GameObject text5prefab;

    string[] text6;
    string text6eng = "Reaching the goal fast will grant you more points. In addition, you can obtain further points by collecting the rubies in the road!";
    string text6cat = "Arribar ràpid a la meta et proporcionarà més punts. A més a més, pots aconseguir punts extra recollint els robins que trobaràs pel camí.";
    string text6cast = "Llegar rápido a la meta te proporcionará más puntos. Además, puedes conseguir puntos extra recogiendo algún rubí por el camino.";
    string text6ita = "Arrivare velocemente alla meta ti darà più punti. Inoltre, puoi ottenere punti extra raccogliendo qualche rubino per il cammino. ";
    public GameObject text6prefab;

    string[] text7;
    string text7eng = "Earning certain achivements will allow you to unlock additional skins for your car.\nThere are a total of 6 skins available!";
    string text7cat = "Completant certes fites podràs desbloquejar aspectes addicionals per al teu vehicle.\nHi ha 6 aspectes en total disponibles! ";
    string text7cast = "Completando ciertos logros podrás desbloquear aspectos adicionales para tu vehículo.\n¡Hay 6 aspectos en total disponibles!";
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
