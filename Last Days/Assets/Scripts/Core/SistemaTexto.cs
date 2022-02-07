using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaTexto
{
	private static Dictionary<TextMeshProUGUI, SistemaTexto> SistemasAtivos = new Dictionary<TextMeshProUGUI, SistemaTexto>();

    private string PreTexto;
    private string TextoAlvo;

    //DPF = Digitos por Frame
    private int DPF = 1;
    private float Velocidade = 1f;

    public bool Pular = false;

    public bool EstaConstruindo {get{return Processo != null;}}
    public Coroutine Processo = null;

    TextMeshProUGUI tmpro;

    public SistemaTexto(TextMeshProUGUI tmpro, string TextoAlvo, string PreTexto = "", int DPF = 1, float Velocidade = 1f)
    {
        this.tmpro = tmpro;
        this.TextoAlvo = TextoAlvo;
        this.PreTexto = PreTexto;
        this.DPF = DPF;
        this.Velocidade = Mathf.Clamp(Velocidade, 1f, 300f);

        Iniciar();
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

        tmpro.text = "";
        tmpro.text  += PreTexto;

		tmpro.ForceMeshUpdate();
		TMP_TextInfo inf = tmpro.textInfo;
		int vis = inf.characterCount;

        tmpro.text += TextoAlvo;

		tmpro.ForceMeshUpdate();
		inf = tmpro.textInfo;
		int max = inf.characterCount;

        tmpro.maxVisibleCharacters = vis;

        while (vis < max)
        {
            if(Pular)
            {
                Velocidade = 1;
                DPF = DPF <5 ? 5 : DPF + 3;
            }

            while(DNF < DPF)
            {
                vis++;
                tmpro.maxVisibleCharacters = vis;
                DNF++;
            }

            DNF = 0;
            yield return new WaitForSeconds(0.01f * Velocidade);
            Terminar();
        }
    }

    void Iniciar()
    {
        SistemaTexto SistemaExistente = null;
        if(SistemasAtivos.TryGetValue(tmpro, out SistemaExistente))
        {
            SistemaExistente.Terminar();
        }

        Processo = DialogueSystem.instance.StartCoroutine(Construcao());
        SistemasAtivos.Add(tmpro, this);
    }

    public void Terminar()
    {
        SistemasAtivos.Remove(tmpro);
        if (EstaConstruindo)
        {
            DialogueSystem.instance.StopCoroutine(Processo);
        }
        Processo = null;
    }
}
