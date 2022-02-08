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

        Dialogo.Falar(Fala, NomePersonagem, add);
    }

    //Transição de sprites
        //Já aviso que está meio gambiarra, não sei como fazer isso funcionar perfeitamente, estou seguindo um modelo diferente do tutorial
    public Sprite PegaSprite(int index = 0)
    {
        Sprite[] Sprites = Resources.LoadAll<Sprite> ("Images/personagens/" + NomePersonagem);
        return Sprites[index];
    }

    public void DefineCorpo(int index)
    {
        renderizadores.RenderizaCorpo.sprite = PegaSprite (index); 
    }

    public void DefineCorpo(Sprite sprite)
    {
        renderizadores.RenderizaCorpo.sprite = sprite;
    }

    public void DefineRosto(int index)
    {
        renderizadores.RenderizaRosto.sprite = PegaSprite (index);
    }

    public void DefineRosto(Sprite sprite)
    {
        renderizadores.RenderizaRosto.sprite = sprite;
    }

    //Transição de Sprites do corpo

    bool CorpoEstaTransicionando {get {return TransicaoCorpo != null;}}
    Coroutine TransicaoCorpo = null;

    public void TransicionaCorpo(Sprite sprite, float velocidade, bool suave)
    {
        if (renderizadores.RenderizaCorpo.sprite == sprite)
            return;
     
        ParaTransicaoCorpo();
        TransicaoCorpo = ManagerPersonagem.instance.StartCoroutine(CorpoTransicionando(sprite, velocidade, suave));
    }

    void ParaTransicaoCorpo()
    {
        if(CorpoEstaTransicionando)
            ManagerPersonagem.instance.StopCoroutine(TransicaoCorpo);
        TransicaoCorpo = null;
    }

    public IEnumerator CorpoTransicionando  (Sprite sprite, float velocidade, bool suave)
    {
        for (int i = 0; i < renderizadores.RenderizaCorpoTodos.Count; i++)
        {
            Image imagem = renderizadores.RenderizaCorpoTodos[i];
            if (imagem.sprite == sprite)
            {
                renderizadores.RenderizaCorpo = imagem;
                break;
            }
        }
        
        if (renderizadores.RenderizaCorpo.sprite != sprite)
        {
            Image imagem = GameObject.Instantiate(renderizadores.RenderizaCorpo.gameObject, renderizadores.RenderizaCorpo.transform.parent).GetComponent<Image>();
            renderizadores.RenderizaCorpoTodos.Add (imagem);
            renderizadores.RenderizaCorpo = imagem;
            imagem.color = FuncoesGlobais.DefineAlpha(imagem.color, 0f);
            imagem.sprite = sprite;
        }

        while (FuncoesGlobais.TransicaoImagens(ref renderizadores.RenderizaCorpo, ref renderizadores.RenderizaCorpoTodos, velocidade, suave))
            yield return new WaitForEndOfFrame();

        ParaTransicaoCorpo();
    }

    //Transição de sprites do Rosto
    bool RostoEstaTransicionando {get {return TransicaoRosto != null;}}
    Coroutine TransicaoRosto = null;

    public void TransicionaRosto(Sprite sprite, float velocidade, bool suave)
    {
        if (renderizadores.RenderizaRosto.sprite == sprite)
            return;
     
        ParaTransicaoRosto();
        TransicaoRosto = ManagerPersonagem.instance.StartCoroutine(RostoTransicionando(sprite, velocidade, suave));
    }

    void ParaTransicaoRosto()
    {
        if(RostoEstaTransicionando)
            ManagerPersonagem.instance.StopCoroutine(TransicaoRosto);
        TransicaoRosto = null;
    }

    public IEnumerator RostoTransicionando  (Sprite sprite, float velocidade, bool suave)
    {
        for (int i = 0; i < renderizadores.RenderizaRostoTodos.Count; i++)
        {
            Image imagem = renderizadores.RenderizaRostoTodos[i];
            if (imagem.sprite == sprite)
            {
                renderizadores.RenderizaRosto = imagem;
                break;
            }
        }
        
        if (renderizadores.RenderizaRosto.sprite != sprite)
        {
            Image imagem = GameObject.Instantiate(renderizadores.RenderizaRosto.gameObject, renderizadores.RenderizaRosto.transform.parent).GetComponent<Image>();
            renderizadores.RenderizaRostoTodos.Add (imagem);
            renderizadores.RenderizaRosto = imagem;
            imagem.color = FuncoesGlobais.DefineAlpha(imagem.color, 0f);
            imagem.sprite = sprite;
        }

        while (FuncoesGlobais.TransicaoImagens(ref renderizadores.RenderizaRosto, ref renderizadores.RenderizaRostoTodos, velocidade, suave))
            yield return new WaitForEndOfFrame();

        ParaTransicaoRosto();
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

        renderizadores.RenderizaCorpo = ob.transform.Find("LayerCorpo").GetComponentInChildren<Image>();
        renderizadores.RenderizaRosto = ob.transform.Find("LayerRosto").GetComponentInChildren<Image>();
        renderizadores.RenderizaCorpoTodos.Add(renderizadores.RenderizaCorpo);
        renderizadores.RenderizaRostoTodos.Add(renderizadores.RenderizaRosto);

        Dialogo = DialogueSystem.instance;

        Ativado = AtivaAoIniciar;
    }

    [System.Serializable]
    public class Renderers
    {
        public Image RenderizaCorpo;
        public Image RenderizaRosto;

        public List<Image> RenderizaCorpoTodos = new List<Image>();
        public List<Image> RenderizaRostoTodos = new List<Image>();
    }

    public Renderers renderizadores  = new Renderers();
}