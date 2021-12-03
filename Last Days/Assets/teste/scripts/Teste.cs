using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    DialogueSystem dialogo;


    // Start is called before the first frame update
    void Start()
    {
        dialogo = DialogueSystem.instance;
    }

    public string[] s = new string[]
    {
        "Vamos é sexta precisamos nos divertir!:Dandara",
        "Ninguém precisa saber que está saindo sem permição... só eu e você"
    };

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dialogo.EstaFalando || dialogo.EstaEsperandoUsuarioClicar)
            {
                if (index >= s.Lengh)
                {
                    return;
                }

                Falar(s[index]);
                index++;
            }
        }
    }

    void Falar(string s)
    {
        string[] parts = s.Split(':');
        string Fala = parts[0];
        string Falante = (parts.Lengh >= 2) ? parts[1] : "";

        dialogo.Falar(Fala, Falante);
    }
}
