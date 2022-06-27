using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu(menuName ="ICE/ice")]
public class ICE  : SerializedScriptableObject
{
    public new string name;
    public List<ICEType> types = new List<ICEType>(); 
    public List<Bit> bits = new List<Bit>();
    public List<(Subroutine subroutine, int numBits)> subroutines = new List<(Subroutine subroutine, int numBits)>();
    public Dictionary<Bit, int> bitTypeWeights = new Dictionary<Bit, int>(); 

    public GameObject icePrefab;
    [HideInInspector] public UnityEvent
        iceBreakEvent = new UnityEvent(),
        iceRetireEvent = new UnityEvent();

    public bool broken => bits.All(bit => bit.decrypted == true);

    public void OnEncounter()
    {
        ServerManager.turnEndEvent.AddListener(ExecuteNextSubroutine);

        foreach ((Subroutine s, int num) sub in subroutines)
        {
            for (int n = 0; n < sub.num; n++)
            {
                int totalBitWeight = bitTypeWeights.Sum(kvp => kvp.Value);
                int bitTypeSeed = Random.Range(0, totalBitWeight);
                int weight = 0;

                foreach (KeyValuePair<Bit, int> kvp in bitTypeWeights)
                {
                    weight += kvp.Value;
                    if (weight > bitTypeSeed)
                    {
                        bits.Add(new Bit(kvp.Key));
                        break;
                    }
                }
            }
        }
    }

    public void ExecuteNextSubroutine()
    {
        if (subroutines.Count > 0)
        {
            (Subroutine routine, int bits) sub = subroutines.First();

            if (bits.Take(sub.bits).Any(bit => bit.decrypted == false))
            {
                Debug.Log($"{name} Executing");
                sub.routine.Execute();
            }
            else
            {
                // The subroutine was broken previously - this should not happen
                Debug.Log($"{sub.routine.name} is fully broken and cannot Execute");
            }

            bits.RemoveRange(0, sub.bits);
            subroutines.Remove(sub);

        }

        if (subroutines.Count > 0)
        {
            Debug.Log($"No Subroutines left on {name}; Destroying");
            iceRetireEvent.Invoke();
        }
    }
}
