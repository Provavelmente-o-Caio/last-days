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

    public string NomeSpriteCorpo = "abgail_completa";
    public string NomeSpriteRosto = "abigailRaiva";

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
                }

                i++;
            }

            if (Input.touchCount>1)
            {
                Touch Toque2 = Input.GetTouch(1);
                if (Toque2.phase == TouchPhase.Ended)
                {
                    Abigail.DefineRosto (abigailFeliz);
                }
                
            }
        }
    }
}
