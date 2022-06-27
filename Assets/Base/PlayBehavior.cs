using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayBehavior : ScriptableObject
{
    public abstract void Play(Rig rig, Card card);
}