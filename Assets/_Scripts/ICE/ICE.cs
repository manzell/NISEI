using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu(menuName ="ICE/Ice")]
public class ICE  : SerializedScriptableObject
{
    public new string name;
    public Sprite sprite;

    public List<BitType> types { get; private set; } = new List<BitType>();
    public List<Subroutine> Subroutines => subroutines;
    public bool broken => subroutines.All(sub => sub.bits.All(bit => bit.decrypted == true));

    [SerializeField] List<Subroutine> subroutines = new List<Subroutine>();

    [HideInInspector] public UnityEvent
        iceBreakEvent = new UnityEvent(),
        derezIceEvent = new UnityEvent();

    public void Rez()
    {
        for(int i = 0; i < subroutines.Count; i++)
            subroutines[i] = Instantiate(subroutines[i]); 

        ServerManager.iceRezEvent.Invoke(this);
        ServerManager.turnEndEvent.AddListener(ExecuteNextSubroutine);
    }

    public void ExecuteNextSubroutine()
    {
        if (Subroutines.Count > 0)
        {
            Subroutine sub = Subroutines.First(); 

            if (sub.bits.Any(bit => bit.decrypted == false))
            {
                Debug.Log($"{sub.name} on {name} Executing");
                sub.Execute();
            }
            else
            {
                Debug.Log($"{sub.name} is fully broken and cannot Execute");
            }

            Subroutines.Remove(sub);
        }

        if (Subroutines.Count == 0)
        {
            Debug.Log($"No Subroutines left on {name}; Derezzing");
            derezIceEvent.Invoke();
        }
    }
}
