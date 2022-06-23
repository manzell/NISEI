using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public CardData data;
    public Card(CardData data) => this.data = GameObject.Instantiate(data);
    public virtual void Play(Rig rig) => data.playBehavior?.Do(rig, this); 
}