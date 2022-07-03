using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu(menuName ="ICE/Ice")]
public class ICE  : SerializedScriptableObject
{
    public new string name;
    [SerializeField] List<Subroutine> subroutines = new List<Subroutine>();
    public Sprite sprite;

    public List<ICEType> types { get; private set; } = new List<ICEType>();
    public List<Subroutine> Subroutines => subroutines; 
    public bool broken => Subroutines.All(sub => sub.bits.All(bit => bit.decrypted == true));

    [HideInInspector] public UnityEvent
        iceBreakEvent = new UnityEvent(),
        derezIceEvent = new UnityEvent();

    public void Rez()
    {
        for (int i = 0; i < subroutines.Count; i++)
            subroutines[i] = Instantiate(subroutines[i]); 

        // TODO Verify this works I'm not sure with the private set that we're dealing with the same copy of sub.bits
        foreach (Subroutine sub in Subroutines)
            for (int i = 0; i < sub.bitCount; i++)
                sub.bits.Add(new Bit(this, sub.bitDepth)); 

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
                Debug.Log($"{name} Executing");
                sub.Execute();
            }
            else
            {
                Debug.Log($"{sub.name} is fully broken and cannot Execute");
            }

            Subroutines.Remove(sub);
        }

        if (Subroutines.Count > 0)
        {
            Debug.Log($"No Subroutines left on {name}; Derezzing");
            derezIceEvent.Invoke();
        }
    }
}
