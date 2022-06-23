using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public string name => cardData.name; 
    public CardData cardData;
    public Card(CardData data) => cardData = GameObject.Instantiate(data);
    public virtual void Play(Rig rig) => cardData.playBehavior?.Do(rig, this); 
}