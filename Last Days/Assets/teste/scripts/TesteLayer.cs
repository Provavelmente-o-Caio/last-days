using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLayer : MonoBehaviour
{
    ControleBackground controller;

    public Texture tex;

    // Start is called before the first frame update
    void Start()
    {
        ControleBackground.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ControleBackground.Layer layer = null;
        Touch Toque = Input.GetTouch(0);
        layer = controller.Background;
        if (Toque.phase == TouchPhase.Ended)
        {
            layer.DefineTextura(tex);
        }
    }
}
