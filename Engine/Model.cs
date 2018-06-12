using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model  {
    public string toString()
    {
        return JsonUtility.ToJson(this);
    }
}
