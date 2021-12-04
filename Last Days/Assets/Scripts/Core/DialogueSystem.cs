using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public ELEMENTS elements;    

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Falar(string Fala, string Falante = "")
    {
        ParaDeFalar();

        Falando = StartCoroutine(FalandoNumerador(Fala, false,Falante));
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
        Falando = null;
    }

    public bool EstaFalando {get{return Falando != null;}}
    [HideInInspector] public bool EstaEsperandoUsuarioClicar = false;

    public string FalaAlvo = "";
    Coroutine Falando = null;
    IEnumerator FalandoNumerador(string Fala, bool Additive, string Falante = "")
    {
        PainelFalas.SetActive(true);
        FalaAlvo = Fala;

        if (!Additive)
            TextoFalas.text = "";
        else
            FalaAlvo = TextoFalas.text + FalaAlvo;

        TextoNome.text = DeterminaFalante(Falante); //gambiarra

        EstaEsperandoUsuarioClicar = false;

        while (TextoFalas.text != FalaAlvo)
        {
            TextoFalas.text += FalaAlvo[TextoFalas.text.Length];
             yield return new WaitForEndOfFrame();
        }

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

    [System.Serializable]
    public class ELEMENTS
    {
        /// O painel que controlará todos os elementos de dialogo da UI
        public GameObject PainelFalas;
        public Text TextoNome;
        public Text TextoFalas;
    }
        public GameObject PainelFalas {get{return elements.PainelFalas;}}
        public Text TextoNome {get{return elements.TextoNome;}}
        public Text TextoFalas {get{return elements.TextoFalas;}}
}
