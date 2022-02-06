using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuncoesGlobais : MonoBehaviour
{
    public static bool TransicaoImagens(ref Image ImagemAtiva, ref List<Image> TodasAsImagens, float velocidade, bool suave)
    {
        bool ValorAlterado = false;

        velocidade *= Time.deltaTime;
        for (int i = TodasAsImagens.Count - 1; i >= 0; i--)
        {
            Image imagem = TodasAsImagens[i];

            if (imagem == ImagemAtiva)
            {
                if (imagem.color.a < 1f)
                {
                    imagem.color = DefineAlpha(imagem.color, suave ? Mathf.Lerp(imagem.color.a, 1f, velocidade) : Mathf.MoveTowards (imagem.color.a, 1f, velocidade));
                    ValorAlterado = true;
                }
            }
            else
            {
                if(imagem.color.a > 0)
                {
                    imagem.color = DefineAlpha(imagem.color, suave ? Mathf.Lerp(imagem.color.a, 0f, velocidade) : Mathf.MoveTowards (imagem.color.a, 0f, velocidade));
                    ValorAlterado = true;
                }
                else
                {
                    TodasAsImagens.RemoveAt(i);
                    DestroyImmediate (imagem.gameObject);
                    continue;
                }
            } 

        }

        return ValorAlterado;
    }

    public static Color DefineAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static bool TransicaoImagensRaw(ref RawImage ImagemAtiva, ref List<RawImage> TodasAsImagens, float velocidade, bool suave)
    {
        bool ValorAlterado = false;

        velocidade *= Time.deltaTime;
        for (int i = TodasAsImagens.Count - 1; i >= 0; i--)
        {
            RawImage imagem = TodasAsImagens[i];

            if (imagem == ImagemAtiva)
            {
                if (imagem.color.a < 1f)
                {
                    imagem.color = DefineAlpha(imagem.color, suave ? Mathf.Lerp(imagem.color.a, 1f, velocidade) : Mathf.MoveTowards (imagem.color.a, 1f, velocidade));
                    ValorAlterado = true;
                }
            }
            else
            {
                if(imagem.color.a > 0)
                {
                    imagem.color = DefineAlpha(imagem.color, suave ? Mathf.Lerp(imagem.color.a, 0f, velocidade) : Mathf.MoveTowards (imagem.color.a, 0f, velocidade));
                    ValorAlterado = true;
                }
                else
                {
                    TodasAsImagens.RemoveAt(i);
                    DestroyImmediate (imagem.gameObject);
                    continue;
                }
            } 

        }

        return ValorAlterado;
    }
}