using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextoAdaptativoPadrao : MonoBehaviour
{
    Text txt;
    public bool UpdateContinuo = true;

    public int fontSizeAtDefaultResolution = 48;
    public static float defaultResolution = 3000f;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();

        if (UpdateContinuo)
        {
            InvokeRepeating("Ajustar", 0f, Random.Range(0.3f, 1f));
        }
        else
        {
            Ajustar();
            enabled = false; 
        }
    }

    void Ajustar()
    {
        if (!enabled || !gameObject.activeInHierarchy)
        { 
            return;
        }

        float totalCurrentRes = Screen.height + Screen.width;
        float perc = totalCurrentRes / defaultResolution;
        int fontsize = Mathf.RoundToInt ((float)fontSizeAtDefaultResolution * perc);

        txt.fontSize = fontsize;
    }
}