using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuLanguageControl : MonoBehaviour
{
    public TMP_Text engtext;
    public TMP_Text cattext;
    public TMP_Text casttext;
    public TMP_Text itatext;

    public TMP_Text opttext;
    public TMP_Text playtext;
    
    public TMP_Text langtext;
    public TMP_Text tuttext;
    public TMP_Text deltext;


    public TMP_Text headeropttext1;
    public TMP_Text headeropttext2;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Main Menu Camera")
        {
            ApplyMainMenuLanguage();
        }
        else if (this.tag == "LanguagesWindow")
        {
            ApplyLangOptionsLanguage();
        }
        else if (this.tag == "Options")
        {
            ApplyOptionsLanguage();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyLangOptionsLanguage()
    {
        int lang = PlayerPrefs.GetInt("Language");

        string[] englist = new string[] { "English", "Anglès", "Inglés", "Inglese" };
        string[] catlist = new string[] { "Catalan", "Català", "Catalán", "Catalano" };
        string[] castlist = new string[] { "Spanish", "Castellà", "Castellano", "Castigliano" };
        string[] italist = new string[] { "Italian", "Italià", "Italiano", "Italiano" };
        string[] headeroptlist1 = new string[] { "Options", "Opcions", "Opciones", "Opzioni" };


        engtext.text = englist[lang];
        cattext.text = catlist[lang];
        casttext.text = castlist[lang];
        itatext.text = italist[lang];
        headeropttext1.text = headeroptlist1[lang];

    }

    public void ApplyMainMenuLanguage()
    {
        int lang = PlayerPrefs.GetInt("Language");

        string[] optlist = new string[] { "Options", "Opcions", "Opciones", "Opzioni" };
        string[] playlist = new string[] { "Play", "Jugar", "Jugar", "Giocare" };
        


        opttext.text = optlist[lang];
        playtext.text = playlist[lang];



    }

    public void ApplyOptionsLanguage()
    {
        int lang = PlayerPrefs.GetInt("Language");

        string[] langlist = new string[] { "Language", "Idioma", "Idioma", "Idioma" };
        string[] dellist = new string[] { "Delete Save", "Eliminar dades", "Eliminar datos", "Elimina dati" };
        string[] headeroptlist2 = new string[] { "Options", "Opcions", "Opciones", "Opzioni" };
        string[] tutlist = new string[] { "Tutorial", "Tutorial", "Tutorial", "Tutorial" };



        langtext.text = langlist[lang];
        deltext.text = dellist[lang];
        headeropttext2.text = headeroptlist2[lang];
        tuttext.text = tutlist[lang];


    }

    public void ReplayTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 0);

        SceneManager.LoadScene("1Player");
    }
}
