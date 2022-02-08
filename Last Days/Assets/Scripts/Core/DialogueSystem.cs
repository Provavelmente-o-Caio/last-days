using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public ELEMENTS elements;    

    void Awake()
    {
        instance = this;
    }

    public void Falar(string Fala, string Falante = "")
    {
        ParaDeFalar();

        Falando = StartCoroutine(FalandoNumerador(Fala, false, Falante));
    }

    public void AdicionaFalar(string Fala, string Falante = "")
    {
        ParaDeFalar();

        TextoFalas.text = FalaAlvo;

        Falando = StartCoroutine(FalandoNumerador(Fala, true, Falante));
    }

    public void ParaDeFalar()
    {
        if (EstaFalando)
        {
            StopCoroutine(Falando);
        }
        if (SisTexto != null && SisTexto.EstaConstruindo)
        {
            SisTexto.Parar();
        }
        Falando = null;
    }

    public bool EstaFalando {get{return Falando != null;}}
    [HideInInspector] public bool EstaEsperandoUsuarioClicar = false;

    public string FalaAlvo = "";
    Coroutine Falando = null;
    SistemaTexto SisTexto = null;

    IEnumerator FalandoNumerador(string Fala, bool Additive, string Falante = "")
    {
        PainelFalas.SetActive(true);

        string FalaAditiva = Additive ? TextoFalas.text : "";
        FalaAlvo = FalaAditiva + Fala;

        SisTexto = new SistemaTexto(TextoFalas, Fala, FalaAditiva);

        TextoNome.text = DeterminaFalante(Falante); //gambiarra

        EstaEsperandoUsuarioClicar = false;

        //while (SisTexto.EstaConstruindo)
        //{
        //    //Tenho de depois se possível adicionar função de pular fala
        //     yield return new WaitForEndOfFrame();
        //}

        EstaEsperandoUsuarioClicar = true;
        while(EstaEsperandoUsuarioClicar)
            yield return new WaitForEndOfFrame();

        ParaDeFalar();
    }

    string DeterminaFalante(string s)
    {
        string RetVal = TextoNome.text;
        if (s != TextoNome.text && s != "")
            RetVal = (s.ToLower().Contains("Narrador")) ? "" : s;

        return RetVal;
    }

    public void Fechar()
    {
        ParaDeFalar();
        PainelFalas.SetActive(false);
    }

    [System.Serializable]
    public class ELEMENTS
    {
        /// O painel que controlará todos os elementos de dialogo da UI
        public GameObject PainelFalas;
        public TextMeshProUGUI TextoNome;
        public TextMeshProUGUI TextoFalas;
    }
        public GameObject PainelFalas {get{return elements.PainelFalas;}}
        public TextMeshProUGUI TextoNome {get{return elements.TextoNome;}}
        public TextMeshProUGUI TextoFalas {get{return elements.TextoFalas;}}
}
