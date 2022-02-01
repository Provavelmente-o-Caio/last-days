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

    //Transição de sprites
        //Já aviso que está meio gambiarra, não sei como fazer isso funcionar perfeitamente, estou seguindo um modelo diferente do tutorial
    public Sprite PegaSprite(int indexer = 0)
    {
<<<<<<< Updated upstream
        Sprite[] Sprite = Resources.LoadAll<Sprite> ("Images/personagens/" + NomePersonagem);
        return Sprite[];
    }

=======
        Sprite[] Sprites = Resources.LoadAll<Sprite> ("Images/personagens/" + NomePersonagem );
        return Sprites[index];
        
    }

    public void DefineRosto(int index)
    {
        renderizadores.RenderizaRosto.sprite = PegaSprite (index); // renderizadores.RenderizaRosto.SpriteRosto = PegaSprite (index);
    }

    public void DefineRosto(Sprite sprite)
    {
        renderizadores.RenderizaRosto.sprite = sprite; // renderizadores.RenderizaRosto.SpriteRosto = PegaSprite;
    }

>>>>>>> Stashed changes
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