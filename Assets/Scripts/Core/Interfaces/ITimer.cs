using System;

public interface ITimer
{
    void WaitUntil(Func<bool> condition, Action onComplete);
}
