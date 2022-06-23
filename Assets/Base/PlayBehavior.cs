using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayBehavior : ScriptableObject
{
    public abstract void Do(Rig rig, Card card);
}
