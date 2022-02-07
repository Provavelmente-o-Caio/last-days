using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaTexto
{
    public string TextoAtual {get {return _TextoAtual;}}
    private string _TextoAtual = "";

    private string PreTexto;
    private string TextoAlvo;

    //DPF = Digitos por Frame
    private int DPF = 1;
    [Range(1f, 60f)]
    private float Velocidade = 1f;
    private bool UsarEncapsulamento = true;

    public bool Pular = false;

    public bool EstaConstruindo {get{return Processo != null;}}
    public Coroutine Processo = null;

    public SistemaTexto(string TextoAlvo, string PreTexto = "", int DPF = 1, float Velocidade = 1f, bool UsarEncapsulamento = true)
    {
        this.TextoAlvo = TextoAlvo;
        this.PreTexto = PreTexto;
        this.DPF = DPF;
        this.Velocidade = Velocidade;
        this.UsarEncapsulamento = UsarEncapsulamento;

        Processo = DialogueSystem.instance.StartCoroutine(Construcao());
    }

    public void Parar()
    {
        if (EstaConstruindo)
        {
            DialogueSystem.instance.StopCoroutine(Processo);
        }
        Processo = null;
    }

    IEnumerator Construcao()
    {
        //DNF = Digitos no frame
        int DNF = 0;
        string[] FalaETags = UsarEncapsulamento ? ManagerTag.SplitByTags(TextoAlvo) : new string[1]{TextoAlvo};

        _TextoAtual = PreTexto;
        string TextoAtual_save = "";
        
        for (int a = 0; a < FalaETags.Length; a++)
        {
            string sessao = FalaETags[a];

            bool TesteTag = (a & 1) != 0;

            if (TesteTag && UsarEncapsulamento)
            {
                TextoAtual_save = _TextoAtual;
                TextoEncapsulado Encapsulacao = new TextoEncapsulado(string.Format("<{0}>", sessao), FalaETags, a);
                while (!Encapsulacao.Pronto)
                {
                    bool passou = Encapsulacao.Passo();

                    _TextoAtual = TextoAtual_save + Encapsulacao.DisplayTexto;

                    if(passou)
                    {
                        DNF++;
                        int DNF_Maximo = Pular ? 5 : DPF;
                        if (DNF == DNF_Maximo)
                        {
                            DNF = 0;
                            yield return new WaitForSeconds (Pular ? 0.01f : 0.01f * Velocidade);
                        }
                    }
                }
                a = Encapsulacao.ProgressoTodasFalaETagsArray + 1;

            }
            //Não contem nenhuma tag
            else
            {
                for(int i = 0; i < sessao.Length; i++)
                {
                    _TextoAtual += sessao[i];

                    DNF++;
                    int DNF_Maximo = Pular ? 5 : DPF;
                    if (DNF == DNF_Maximo)
                    {
                        DNF = 0;
                        yield return new WaitForSeconds (Pular ? 0.01f : 0.01f * Velocidade);
                    }
                }
            }
        }

        Processo = null;
    }

    private class TextoEncapsulado
    {
        private string tag = "";
        private string TagFim = "";

        private string TextoAtual = "";
        private string TextoAlvo = "";

        public string DisplayTexto {get {return _DisplayTexto;}}
        private string _DisplayTexto = "";

        private string[] TodasFalaETagsArray;
        public int ProgressoTodasFalaETagsArray {get{return ProgressoArray;}}
        private int ProgressoArray = 0;

        public bool Pronto {get{return _Pronto;}}
        private bool _Pronto = false;

        public TextoEncapsulado Encapsulador = null;
        public TextoEncapsulado SubEncapsulador = null;

        public TextoEncapsulado(string tag, string[] TodasFalaETagsArray, int ProgressoArray)
        {
            this.tag = tag;
            GerarTagFim();

            this.TodasFalaETagsArray = TodasFalaETagsArray;
            this.ProgressoArray = ProgressoArray;

            if (TodasFalaETagsArray.Length - 1 > ProgressoArray)
            {
                string ProximaParte = TodasFalaETagsArray[ProgressoArray + 1];
                TextoAlvo = ProximaParte;

                this.ProgressoArray++;
            }
        }
        void GerarTagFim()
        {
            TagFim = tag.Replace("<","").Replace(">","");

            if (TagFim.Contains("="))
            {
                TagFim = string.Format("</{0}>", TagFim.Split('=')[0]);
            }
            else
            {
                TagFim = string.Format("</{0}>", TagFim);
            }
        }

        public bool Passo()
        {
            if(Pronto)
            {
                return true;
            }

            if (SubEncapsulador != null && !SubEncapsulador.Pronto)
            {
                return SubEncapsulador.Passo();
            }
            else
            {
                if(TextoAtual == TextoAlvo)
                {
                    if (TodasFalaETagsArray.Length > ProgressoArray + 1)
                    {
                        string ProximaParte = TodasFalaETagsArray[ProgressoArray + 1];
                        bool TesteTag = ((ProgressoArray + 1) & 1) != 0;

                        if (TesteTag)
                        {
                            if (string.Format("<{0}>", ProximaParte) == TagFim)
                            {
                                _Pronto = true;

                                if(Encapsulador != null)
                                {
                                    string TextoComTag = (tag + TextoAtual + TagFim);
                                    Encapsulador.TextoAtual += TextoComTag;
                                    Encapsulador.TextoAlvo += TextoComTag;

                                    UpdateProgressoArray(2);
                                }
                            }
                            else
                            {
                                SubEncapsulador = new TextoEncapsulado(string.Format("<{0}>", ProximaParte), TodasFalaETagsArray, ProgressoArray + 1);
                                SubEncapsulador.Encapsulador = this;

                                UpdateProgressoArray();
                            }
                        }
                        else
                        {
                            TextoAlvo += ProximaParte;
                            UpdateProgressoArray();
                        }
                    }
                    else
                    {
                        _Pronto = true;
                    }
                }
                else
                {
                    TextoAtual += TextoAlvo[TextoAtual.Length];
                    UpdateDisplay("");

                    return true;
                }
            }
            return false;
        }

        void UpdateProgressoArray(int val = 1)
        {
            ProgressoArray += val;

            if (Encapsulador != null)
            {
                Encapsulador.UpdateProgressoArray(val);
            }
        }

        void UpdateDisplay(string SubTexto)
        {
           _DisplayTexto = string.Format("{0},{1},{2},{3}", tag, TextoAtual, SubTexto, TagFim); 

           if (Encapsulador != null)
           {
               Encapsulador.UpdateDisplay(DisplayTexto);
           }
        }
    }
}
