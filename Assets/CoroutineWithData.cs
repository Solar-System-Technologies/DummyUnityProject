/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// taken from https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    private object result;
    private IEnumerator target;

    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }

    public T GetResult<T>() where T : class
        => result as T;
} */