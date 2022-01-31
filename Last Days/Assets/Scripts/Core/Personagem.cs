using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[System.Serializable]
public class Personagem
{
    public string NomePersonagem;
    [HideInInspector]public RectTransform root;

    //Cria sistema de mostrar ou esconder personagem na tela, padrão é com ele como verdadeiro
    public bool Ativado{get {return root.gameObject.activeInHierarchy;} set {root.gameObject.SetActive(value);}}

    DialogueSystem Dialogo;

    public void Falar(string Fala, bool add=false)
    {
        //Pequeno teste, faz o personagem aparecer na tela para falar
        if(!Ativado)
        {
            Ativado = true;
        }

        if(!add)
        {
            Dialogo.Falar(Fala, NomePersonagem);
        }
        else
        {
            Dialogo.AdicionaFalar(Fala, NomePersonagem);
        }
    }

    //Carrega/Cria Personagem
    public Personagem (string _nome, bool AtivaAoIniciar = true)
    {
        ManagerPersonagem mp = ManagerPersonagem.instance;
        //Pega a prefab do personagem
        GameObject prefab = Resources.Load("Personagens/Personagem[" + _nome + "]") as GameObject;
        GameObject ob = GameObject.Instantiate(prefab, mp.PainelPersonagens);

        root = ob.GetComponent<RectTransform> ();
        NomePersonagem = _nome;

        renderizadores.RenderizaCorpo = ob.transform.Find("LayerCorpo").GetComponent<Image> ();
        renderizadores.RenderizaRosto = ob.transform.Find("LayerRosto").GetComponent<Image> ();

        Dialogo = DialogueSystem.instance;

        Ativado = AtivaAoIniciar;
    }

    [System.Serializable]
    public class Renderers
    {
        public Image RenderizaCorpo;
        public Image RenderizaRosto;
    }

    public Renderers renderizadores  = new Renderers();
}