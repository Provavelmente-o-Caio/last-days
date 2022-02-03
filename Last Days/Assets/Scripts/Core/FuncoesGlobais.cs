using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuncoesGlobais : MonoBehaviour
{
    public static bool TransicaoImagens(ref Image activeImage, ref List<image> allImages, float velocidade, bool suave)
    {
        bool ValorAlterado = false;

        velocida *= Time.deltaTime;
        for (int i = TodasAsImagens.count - 1; i >= 0; i--)
        {
            Image imagem = TodasAsImagens[i];

            if (imagem == ImagemAtiva)
            {
                imagem.cor = DefineAlpha(imagem.cor, suave ? Mathf.Lerp(imagem.cor.a, 1f, velocidade) : Mathf.MoveTowards (imagem.color.a, 1f, velocidade));
                ValorAlterado = true;
            }
            else
            {
                imagem.cor = DefineAlpha(imagem.cor, suave ? Mathf.Lerp(imagem.cor.a, 0f, velocidade) : Mathf.MoveTowards (imagem.color.a, 0f, velocidade));
                ValorAlterado = true;
            }   

        }

        return ValorAlterado;
    }

    public static Color DefineAlpha(Color cor, float alpha)
    {
        return new Color(cor.r, cor.g, cor.b, alpha);
    }
}
