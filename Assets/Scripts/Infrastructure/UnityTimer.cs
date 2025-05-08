using System;
using System.Collections;
using UnityEngine;

public class UnityTimer : MonoBehaviour, ITimer
{
    public void WaitUntil(Func<bool> condition, Action onComplete)
    {
        StartCoroutine(WaitRoutine(condition, onComplete));
    }

    private IEnumerator WaitRoutine(Func<bool> condition, Action callback)
    {
        yield return new WaitUntil(condition);
        callback?.Invoke();
    }
}
