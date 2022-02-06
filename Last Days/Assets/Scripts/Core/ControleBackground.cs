using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBackground : MonoBehaviour
{
    public static ControleBackground instance;

    public Layer Background = new Layer();
    public Layer Foreground = new Layer();

    void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Layer
    {
        public GameObject root;
        public GameObject NovaImagemRef;
        public RawImage ImagemAtiva;
        public List<RawImage> TodasAsImagens = new List<RawImage>();

        public void DefineTextura(Texture texture)
        {
            if (texture != null)
            {
                if (ImagemAtiva == null)
                {
                    AtualizarImagemAtiva();
                }

                ImagemAtiva.texture = texture;
                ImagemAtiva.color = FuncoesGlobais.DefineAlpha(ImagemAtiva.color, 1f);
            }
            else
            {
                if (ImagemAtiva != null)
                {
                    TodasAsImagens.Remove(ImagemAtiva);
                    GameObject.DestroyImmediate(ImagemAtiva.gameObject);
                    ImagemAtiva = null;
                }
            }
        }

        public void TransicionaParaTextura(Texture texture, float velocidade = 2f, bool suave = false)
        {
            if (ImagemAtiva != null && ImagemAtiva.texture == texture)
            {
                return;
            }

            ParaTransicao();
            Transicionando_C = ControleBackground.instance.StartCoroutine(Transicionando(texture, velocidade, suave));
        }

        void ParaTransicao()
        {
            if (EstaTransicionando)
            {
                ControleBackground.instance.StopCoroutine(Transicionando_C);
            }
            Transicionando_C = null;
        }

        public bool EstaTransicionando {get{return Transicionando_C != null;}}
        Coroutine Transicionando_C = null;

        IEnumerator Transicionando(Texture texture, float velocidade, bool suave)
        {
            if (texture != null)
            {
                for (int i = 0; i < TodasAsImagens.Count; i++)
                {
                    RawImage imagem = TodasAsImagens[i];
                    if (imagem.texture == texture)
                    {
                        ImagemAtiva = imagem;
                        break;
                    }
                }

                if (ImagemAtiva == null || ImagemAtiva.texture != texture)
                {
                    AtualizarImagemAtiva();
                    ImagemAtiva.texture = texture;
                    ImagemAtiva.color = FuncoesGlobais.DefineAlpha(ImagemAtiva.color, 0f);
                }
            }

            else
            {
                ImagemAtiva = null;
            }

            while (FuncoesGlobais.TransicaoImagensRaw(ref ImagemAtiva, ref TodasAsImagens, velocidade, suave))
            {
                yield return new WaitForEndOfFrame();
            }

            ParaTransicao();
        }

        void AtualizarImagemAtiva()
        {
            GameObject ob = Instantiate(NovaImagemRef, root.transform) as GameObject;
            ob.SetActive(true);
            RawImage raw = ob.GetComponent<RawImage>();
            ImagemAtiva = raw;
            TodasAsImagens.Add(raw);
        }
    }
}
