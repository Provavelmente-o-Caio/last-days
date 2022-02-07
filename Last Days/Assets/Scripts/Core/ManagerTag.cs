using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTag : MonoBehaviour
{
    public static string[] SplitByTags(string TextoAlvo)
    {
        return TextoAlvo.Split(new char[2]{'<','>'});
    }
}
