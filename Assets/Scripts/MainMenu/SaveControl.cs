using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveControl : MonoBehaviour
{

    public CanvasGroup skin1lock;
    public CanvasGroup skin2lock;
    public CanvasGroup skin3lock;
    public CanvasGroup skin4lock;
    public CanvasGroup skin5lock;

    public GameObject window1prefab;
    public GameObject optionsprefab;
    public GameObject languagesprefab;
    public GameObject chooselanguageprefab;

    public GameObject deletebutton2prefab;

    int delcount;
    GameObject delbutton;
    Image delImage;

    public GameObject achievementtextprefab;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            Debug.Log("First time opening the game.");
            CreateSavePlaceHolders();
            PlayerPrefs.Save();

            ShowFirstLanguage();
        }

        if (GameObject.FindGameObjectWithTag("Title") != null && GameObject.FindGameObjectWithTag("Window1") != null)
        {
            //code
            SkinCheck();
        }

        if (GameObject.FindGameObjectWithTag("Title") != null && GameObject.FindGameObjectWithTag("Window1") == null)
        {
            //code

            SkinControl();
        }

        delcount = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteSave()
    {
        if (delcount == 0)
        {
            delbutton = GameObject.FindWithTag("DeleteButton");

            delImage = delbutton.GetComponent<Image>();

            delImage.color = Color.red;

            delcount++;
        }
        else if (delcount == 1)
        {
            PlayerPrefs.DeleteAll();

            SceneManager.LoadScene("MainMenu");

            delcount = 0;
        }

    }

    public void CreateSavePlaceHolders()
    {
        PlayerPrefs.SetInt("TotalRaces", 0);
        PlayerPrefs.SetInt("MaxScore", 0);
        PlayerPrefs.SetInt("TotalWins", 0);
        PlayerPrefs.SetInt("TotalGameOvers", 0);
        PlayerPrefs.SetInt("MaxSpeed", 0);

        PlayerPrefs.SetInt("CurrentSkin", 0);

    }

    public void SkinControl()
    {
        if (PlayerPrefs.GetInt("TotalWins") > 1)
        {
            PlayerPrefs.SetInt("Skin1", 1);
            //code
        }

        if (PlayerPrefs.GetInt("TotalWins") >= 2)
        {
            PlayerPrefs.SetInt("Skin2", 1);
            //code
        }

        if (PlayerPrefs.GetInt("TotalWins") >= 3)
        {
            PlayerPrefs.SetInt("Skin3", 1);
            //code
        }

        if (PlayerPrefs.GetInt("MaxSpeed") > 0)
        {
            PlayerPrefs.SetInt("Skin4", 1);
            //code
        }

        if (PlayerPrefs.GetInt("MaxSpeed") > 0)
        {
            PlayerPrefs.SetInt("Skin5", 1);
            //code
        }

        PlayerPrefs.Save();
    }

    public void ApplySkin(int nskin)
    {
        string[] skins = new string[] { "Skin0", "Skin1", "Skin2", "Skin3", "Skin4", "Skin5" };

        if (PlayerPrefs.GetInt(skins[nskin]) == 1 || nskin == 0)
        {
            PlayerPrefs.SetInt("CurrentSkin", nskin);
            Debug.Log(skins[nskin] + " applied");
            Destroy(this.gameObject);
        }

        PlayerPrefs.Save();
    }

    public void InstantiateSkinsWindow()
    {
        Instantiate(window1prefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject window1 = GameObject.FindWithTag("Window1");
        GameObject canvasobject = GameObject.FindWithTag("Canvas");
        window1.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);
        window1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void CloseWindow()
    {
        Destroy(this.gameObject);
    }

    public void SkinCheck()
    {
        string[] skins = new string[] { "Skin1", "Skin2", "Skin3", "Skin4", "Skin5" };

        CanvasGroup[] skinlocks = new CanvasGroup[] { skin1lock, skin2lock, skin3lock, skin4lock, skin5lock };

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt(skins[i]) == 1)
            {
                skinlocks[i].alpha = 0f;
                skinlocks[i].blocksRaycasts = false;
            }
        }
    }

    public void InstantiateOptionsWindow()
    {
        Instantiate(optionsprefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject optionsobject = GameObject.FindWithTag("Options");
        GameObject canvasobject = GameObject.FindWithTag("Canvas");
        optionsobject.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);
        optionsobject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void ShowLanguages()
    {
        Instantiate(languagesprefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject languagesobject = GameObject.FindWithTag("LanguagesWindow");
        GameObject canvasobject = GameObject.FindWithTag("Canvas");
        languagesobject.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);
        languagesobject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void ChangeLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }

    public void ChooseFirstLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }

    public void ShowFirstLanguage()
    {
        Instantiate(chooselanguageprefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject languagesobject = GameObject.FindWithTag("FirstLanguageWindow");
        GameObject canvasobject = GameObject.FindWithTag("Canvas");
        languagesobject.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);
        languagesobject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void SkinLockWarning(int skinN)
    {
        
        string[] skin2list = new string[] { "Win 10 matches", "Guanya 10 partides", "Gana 10 partidas", "Ita win 10" };
        string[] skin3list = new string[] { "Win 25 matches", "Guanya 25 partides", "Gana 25 partidas", "Ita win 25" };
        string[] skin4list = new string[] { "Win 100 matches", "Guanya 100 partides", "Gana 100 partidas", "Ita win 100" };
        string[] skin5list = new string[] { "Propel your car with a very high speed", "Propulsa el cotxe amb una gran velocitat", "Propulsa el coche con una gran velocidad", "Ita high speed" };
        string[] skin6list = new string[] { "Run from the monster just before it reaches you", "Escapa del monstre just abans de que t'agafi", "Escapa del monstruo justo antes de que te alcance", "Ita run before it reaches you" };

        int lang = PlayerPrefs.GetInt("Language");

        Instantiate(achievementtextprefab, new Vector3(0, 0, 0), Quaternion.identity);

        GameObject canvasobject = GameObject.FindWithTag("Canvas");
        GameObject achievementobject = GameObject.FindWithTag("AchievementText");

        achievementobject.GetComponent<RectTransform>().SetParent(canvasobject.GetComponent<RectTransform>(), false);

        RectTransform achievementtransform = achievementobject.GetComponent<RectTransform>();
        Transform achievementtexttransform = achievementtransform.GetChild(0);
        TextMeshProUGUI achievementtext = achievementtexttransform.GetComponent<TextMeshProUGUI>();

        achievementtransform.SetParent(GameObject.FindWithTag("Window1").GetComponent<RectTransform>());

        achievementtransform.anchoredPosition = new Vector3(0, -115, 0);

        switch (skinN)
        {
            case 2:
                achievementtext.text = skin2list[lang];
                break;
            case 3:
                achievementtext.text = skin3list[lang];
                break;
            case 4:
                achievementtext.text = skin4list[lang];
                break;
            case 5:
                achievementtext.text = skin5list[lang];
                break;
            case 6:
                achievementtext.text = skin6list[lang];
                break;

        }

        //GameObject.Destroy(achievementobject);
    }
}
