using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public abstract class GameEvent<T> : ScriptableObject
{
    private UnityEvent<T> Event = new UnityEvent<T>(); 

    public void AddListener(UnityAction<T> call) => Event.AddListener(call);
    public void RemoveListener(UnityAction<T> call) => Event.RemoveListener(call);
    public void Invoke(T arg) => Event.Invoke(arg);
}

public abstract class GameEvent<T, U> : ScriptableObject
{
    private UnityEvent<T, U> Event = new UnityEvent<T, U>();

    public void AddListener(UnityAction<T, U> call) => Event.AddListener(call);
    public void RemoveListener(UnityAction<T, U> call) => Event.RemoveListener(call);
    public void Invoke(T arg, U arg2) => Event.Invoke(arg, arg2);
}

public abstract class GameEvent<T, U, V> : ScriptableObject
{
    private UnityEvent<T, U, V> Event = new UnityEvent<T, U, V>();

    public void AddListener(UnityAction<T, U, V> call) => Event.AddListener(call);
    public void RemoveListener(UnityAction<T, U, V> call) => Event.RemoveListener(call);
    public void Invoke(T arg, U arg2, V arg3) => Event.Invoke(arg, arg2, arg3);
}
