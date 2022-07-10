using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Program/AddCard")]
public class AddCard : Program
{
    [SerializeField] Card card;

    public override Executable GetExecutable() => new Executable(callback => Execute(ServerManager.currentRig, card, callback)); 

    void Execute(Rig rig, Card card, UnityAction callback)
    {
        Debug.Log($"{name} adds {card.name} to {name}!");
        rig.AddCard(card);
        callback?.Invoke(); 
    }
}