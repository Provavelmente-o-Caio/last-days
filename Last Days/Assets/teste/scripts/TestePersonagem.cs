using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestePersonagem : MonoBehaviour
{
    public Personagem Abigail;

    // Start is called before the first frame update
    void Start()
    {
        Abigail = ManagerPersonagem.instance.PegaPersonagem("Abigail", AtivarPersonagemCriadoNoInicio: false);
    }

    public string[] Fala;
    int i = 0;

    public int IndexCorpo, IndexRosto = 0;
    public float velocidade = 5f;
    public bool TransicaoSuave = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            Touch Toque = Input.GetTouch(0);
            if (Toque.phase == TouchPhase.Ended)
            {
                if (i < Fala.Length)
                {
                    Abigail.Falar(Fala[i]);
                }
                else
                {
                    DialogueSystem.instance.Fechar();
                    Abigail.TransicionaCorpo(Abigail.PegaSprite(IndexCorpo), velocidade, TransicaoSuave);
                    Abigail.TransicionaRosto(Abigail.PegaSprite(IndexRosto), velocidade, TransicaoSuave);
                }
                i++;
            }
        }
    }
}
