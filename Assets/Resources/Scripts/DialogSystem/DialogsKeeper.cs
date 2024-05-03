using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsKeeper : MonoBehaviour
{
    private static DialogsKeeper _instance;

    public static DialogsKeeper GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
