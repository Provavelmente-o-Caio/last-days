using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Script que irá adicionar os personagens na tela
public class ManagerPersonagem : MonoBehaviour
{
    public static ManagerPersonagem instance;

    public RectTransform PainelPersonagens;

    //Lista de todos os personagens presentemente na tela
    public List<Personagem> Personagens = new List<Personagem>();

    //Adiciona o nome dos personagens nesse dicionário, possibilitando chamada
    public Dictionary<string, int> DictionaryPersonagem = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;
    }

    //Tenta pegar um nome do dicionário
    public Personagem PegaPersonagem(string NomePersonagem, bool CriarPersonagemSeNaoExiste = true, bool AtivarPersonagemCriadoNoInicio = true)
    {
        int index = -1;
        if (DictionaryPersonagem.TryGetValue(NomePersonagem, out index))
        {
            return Personagens[index];
        }
        else if(CriarPersonagemSeNaoExiste)
        {
            return CriarPersonagem(NomePersonagem, AtivarPersonagemCriadoNoInicio);
        }
        return null;
    }

    public Personagem CriarPersonagem(string NomePersonagem, bool AtivaAoIniciar = true)
    {
        Personagem NovoPersonagem = new Personagem(NomePersonagem, AtivaAoIniciar);

        DictionaryPersonagem.Add(NomePersonagem, Personagens.Count);
        Personagens.Add(NovoPersonagem);

        return NovoPersonagem;
    }
}