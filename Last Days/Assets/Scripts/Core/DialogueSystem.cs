using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem instance;

    public ELEMENTS elements;
    

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Falar(string Fala, string Falante) {
        ParaDeFalar();
        Falando = ComecaCoroutine(FalandoNumerador(Fala, Falante));
    }

    public void ParaDeFalar() {
        if (EstaFalando) {
            ParaCoroutine(Falando);
        }
        Falando = null;
    }

    public bool EstaFalando {get{return Falando != null;}}
    [HideInInspector] public bool EstaEsperandoUsuarioClicar = false;

    Coroutine Falando = null;
    IEnumerator FalandoNumerador(string FalaAlvo, string Falante) {
        PainelFalas.SetActive(true);
        TextoFalas.text = "";
        TextoNome.text = Falante; //gambiarra
        EstaEsperandoUsuarioClicar = false;

        while (TextoFalas.text != FalaAlvo) {
            TextoFalas.text += FalaAlvo[TextoFalas.text.Lengh-1];
             yield return new EspereFimFrame();
        }

        EstaEsperandoUsuarioClicar = true;
        while(EstaEsperandoUsuarioClicar)
            yield return new EspereFimFrame();

        ParaDeFalar();
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
