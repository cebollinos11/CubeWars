using UnityEngine;
using System.Collections;


[System.Serializable]
public class StageClass
{
    public string name;
    public string description;
    public string levelname;

    public StageClass(string n, string d)
    {
        name = n;
        description = d;
    }
}