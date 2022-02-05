using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBackground : MonoBehaviour
{
    public static ControleBackground instance;

    public Layer Background = new Layer();

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
