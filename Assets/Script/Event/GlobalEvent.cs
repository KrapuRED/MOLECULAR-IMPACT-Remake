using System;
using UnityEngine;

public class CustomEvent
{
    private event Action _action = delegate { };

    public void Invoke()
    {
        _action?.Invoke();
    }

    public void Addistener(Action listener)
    {
        _action += listener;
    }
    public void Removeistener(Action listener)
    {
        _action += listener;
    }
}

public class CustomEvent<T>
{
    private event Action<T> _action = delegate { };

    public void Invoke(T args)
    {
        _action?.Invoke(args);
    }

    public void Addistener(Action<T> listener)
    {
        _action += listener;
    }
    public void Removeistener(Action<T> listener)
    {
        _action += listener;
    }
}

public class CustomEvent<T1,T2>
{
    private event Action<T1, T2> _action = delegate { };

    public void Invoke(T1 arg1, T2 arg2)
    {
        _action?.Invoke(arg1, arg2);
    }

    public void Addistener(Action<T1, T2> listener)
    {
        _action += listener;
    }
    public void Removeistener(Action<T1, T2> listener)
    {
        _action += listener;
    }
}

public class CustomEvent<T1,T2,T3>
{
    private event Action<T1, T2, T3> _action = delegate { };

    public void Invoke(T1 arg1, T2 arg2, T3 arg3)
    {
        _action?.Invoke(arg1,arg2,arg3);
    }

    public void Addistener(Action<T1, T2, T3> listener)
    {
        _action += listener;
    }
    public void Removeistener(Action<T1, T2, T3> listener)
    {
        _action += listener;
    }
}

public class GlobalEvent
{
    public static readonly CustomEvent OnResetManager = new();

    // ======================= EVENTS BED ROOM =======================
    public static readonly CustomEvent OnRefreshUI = new();

    // ======================= EVENTS WORK DAY ======================= 
    public static readonly CustomEvent<string, float> OnApplyRandomDayEvent = new ();
    
    public static readonly CustomEvent<Sprite, string> OnShowIllustrastionWorkDay = new();
    public static readonly CustomEvent<Days> OnUpdateVisualizeDay = new();

    public static readonly CustomEvent OnNextActivity = new();
    public static readonly CustomEvent OnNextDay = new();
}
