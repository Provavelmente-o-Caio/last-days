using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNovel : MonoBehaviour
{
    List<string> data = new List<string>();
    int progresso = 0;
    // Start is called before the first frame update
    void Start()
    {
        CarregaArquivoCapítulo("PrimeiroDia");
    }

    // Update is called once per frame
    void Update()
    {
        //Teste
        if (Input.touchCount>0)
        {
            Touch Toque = Input.GetTouch(0);
            if(Toque.phase == TouchPhase.Ended)
            {
                LidaLinha(data[progresso]);
                progresso++;
                CacheFalante = "";
            }
        }
    }

    public void CarregaArquivoCapítulo(string NomeArquivo)
    {
        data = ManagerArquivos.LoadFile(ManagerArquivos.savPath + "Resources/Story/" + NomeArquivo);
        progresso = 0;
    }

    void LidaLinha(string Linha)
    {
        string [] DialogoEAcoes = Linha.Split('"');

        if (DialogoEAcoes.Length == 3)
        {
            LidaDialogo(DialogoEAcoes[0], DialogoEAcoes[1]);
            LidaEventosLinha(DialogoEAcoes[2]);
        }
        else
        {
            LidaEventosLinha(DialogoEAcoes[0]);
        }
    }

    string CacheFalante = "";
    void LidaDialogo(string DetalheDialogo, string Dialogo)
    {
        string Falante = CacheFalante;
        bool Aditivo = false;

        if (DetalheDialogo.Length > 0)
        {
            if (DetalheDialogo[DetalheDialogo.Length-1] == ' ')
            {
                DetalheDialogo = DetalheDialogo.Remove(DetalheDialogo.Length-1);
            }

            Falante = DetalheDialogo;
            CacheFalante = Falante;
        }
        Personagem Personagens = ManagerPersonagem.instance.PegaPersonagem(Falante);
        Personagens.Falar(Dialogo, Aditivo);
    }

    void LidaEventosLinha(string eventos)
    {
        string[] acoes = eventos.Split(';');

        foreach(string acao in acoes)
        {
            LidaAcao(acao);
        }
    }

    void LidaAcao(string acao)
    {
        print("Ação [" + acao + "]");
        string[] data = acao.Split('(',')');

        if(data[0] == "DefineBackground")
        {
            comandoDefineLayerImagem(data[1], ControleBackground.instance.Background);
            return;
        }

        if(data[0] == "DefineForeground")
        {
            comandoDefineLayerImagem(data[1], ControleBackground.instance.Foreground);
            return;
        }
    }

    void comandoDefineLayerImagem(string data, ControleBackground.Layer layer)
    {
        string NomeTextura = data.Contains(",") ? data.Split(',')[0] : data;
        Debug.Log(NomeTextura);
        Texture2D tex = Resources.Load("Images/backgrounds/" + NomeTextura) as Texture2D;
        float velocidade = 2f;
        bool suave = false;

        if (data.Contains(","))
        {
            string[] parametros = data.Split(',');
            foreach(string p in parametros)
            {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                {
                    velocidade = fVal;
                }
                if(bool.TryParse(p, out bVal))
                {
                    suave = bVal; continue;
                }
            }
        }
        layer.TransicionaParaTextura(tex, velocidade, suave);
    }
}
