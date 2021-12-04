using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutScript : MonoBehaviour
{
    public GameObject aboutWindowPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AboutWindow()
    {
        Instantiate(aboutWindowPrefab);

        GameObject canvasObject = GameObject.FindWithTag("Canvas");
        GameObject aboutWindowObject = GameObject.FindWithTag("AboutWindow");
        aboutWindowObject.GetComponent<RectTransform>().SetParent(canvasObject.GetComponent<RectTransform>(), false);
        Debug.Log("clic");
    }

    public void CloseAboutWindow()
    {
        Destroy(this.gameObject);
    }
}
