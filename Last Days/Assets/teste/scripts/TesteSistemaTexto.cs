using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TesteSistemaTexto : MonoBehaviour
{
    public TextMeshProUGUI tmprotext;
    SistemaTexto ST;

    [TextArea(5, 10)]
    public string say;
    public int DPF = 1;
    public float Velocidade = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ST = new SistemaTexto(tmprotext, say, "", DPF, Velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            Touch Toque = Input.GetTouch(0);
            if (Toque.phase == TouchPhase.Ended)
            {
                ST = new SistemaTexto(tmprotext, say, "", DPF, Velocidade);
            }
        }
    }
}
