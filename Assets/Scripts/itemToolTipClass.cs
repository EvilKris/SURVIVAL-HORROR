using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class itemToolTipClass
{
    //public string name;
    public GameObject gameobject;
    [TextArea(3, 10)]
    public string[] text;
}
