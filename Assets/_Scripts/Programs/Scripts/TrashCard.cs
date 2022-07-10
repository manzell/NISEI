using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

[CreateAssetMenu(menuName = "Program/Trash")]
public class TrashCard : Program
{
    [SerializeField] Card card;

    public override Executable GetExecutable() => new Executable(callback => Execute(ServerManager.currentRig, callback)); 
    
    void Execute(Rig rig, UnityAction callback)
    {
        if(card == null)
            card = rig.GetAllCards().OrderBy(card => Random.value).First();
        if(card != null)
        {
            Debug.Log($"{name} trashes {card.name} from the {rig.SourceDeck(card)}");
            rig.RemoveCard(card);
        }

        callback?.Invoke(); 
    }
}
