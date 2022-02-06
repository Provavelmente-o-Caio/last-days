using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLayer : MonoBehaviour
{
    ControleBackground controller;

    public Texture tex;

    public float velocidade;
    public bool suave;

    // Start is called before the first frame update
    void Start()
    {
        controller = ControleBackground.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ControleBackground.Layer layer = null;
        if (Input.touchCount>0)
        {
            Touch Toque = Input.GetTouch(0);
            layer = controller.Background;
            if (Toque.phase == TouchPhase.Ended)
            {
                layer.TransicionaParaTextura(tex, velocidade, suave);
            }
        }
    }
}
