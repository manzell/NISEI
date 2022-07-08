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
    //[SerializeField] OrderedDictionary<Subroutine, (int bits, int depth)> subroutines = new OrderedDictionary<Subroutine, (int, int)>();
    [SerializeField] Dictionary<Subroutine, (BitType bitType, int bits, int depth)> subroutines = new Dictionary<Subroutine, (BitType bitType, int bits, int depth)>();
    public Sprite sprite;

    public List<BitType> types { get; private set; } = new List<BitType>();
    public List<Subroutine> Subroutines { get; private set; } = new List<Subroutine>(); 
    public bool broken => Subroutines.All(sub => sub.bits.All(bit => bit.decrypted == true));

    [HideInInspector] public UnityEvent
        iceBreakEvent = new UnityEvent(),
        derezIceEvent = new UnityEvent();

    public void Rez()
    {
        // TODO Verify this works I'm not sure with the private set that we're dealing with the same copy of sub.bits
        foreach (Subroutine subroutine in subroutines.Keys)
        {
            Subroutine sub = Instantiate(subroutine);
            sub.bits.Clear(); 

            for (int i = 0; i < subroutines[subroutine].bits; i++)
                sub.bits.Add(new Bit(subroutines[subroutine].bitType, this, subroutines[subroutine].depth));

            Subroutines.Add(sub); 
        }

        ServerManager.iceRezEvent.Invoke(this);
        ServerManager.turnEndEvent.AddListener(ExecuteNextSubroutine);
    }

    public void ExecuteNextSubroutine()
    {
        Debug.Log($"Subroutines: ({Subroutines.Count})");

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
