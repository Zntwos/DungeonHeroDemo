using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������������
/// </summary>
/// <typeparam name="X"></typeparam>
public class BaseManager<X> where X : new()
{
    private static X instance;

    public static X GetInstance()
    {
        if (instance == null)
        {
            instance = new X();
        }
        return instance;
    }
}