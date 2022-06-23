using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

[System.Serializable]
public class ICE
{
    public ICEdata data;
    [HideInInspector] public UnityEvent 
        iceBreakEvent = new UnityEvent(),
        iceRetireEvent = new UnityEvent(); 

    public bool broken => data.bits.All(bit => bit.decrypted == true);

    public ICE(ICEdata data)
    {
        ServerManager.turnEndEvent.AddListener(ExecuteNextSubroutine);
        this.data = GameObject.Instantiate(data);        

        foreach ((Subroutine s, int num) sub in data.subroutines) 
        {
            for(int n = 0; n < sub.num; n++)
            {
                int totalBitWeight = this.data.bitTypeWeights.Sum(kvp => kvp.Value);
                int bitTypeSeed = Random.Range(0, totalBitWeight);
                int weight = 0; 
                
                foreach (KeyValuePair<Bit, int> kvp in data.bitTypeWeights)
                {
                    weight += kvp.Value;
                    if (weight > bitTypeSeed)
                    {
                        this.data.bits.Add(new Bit(kvp.Key));
                        break;
                    }
                }
            }
        }
    }

    public void ExecuteNextSubroutine()
    {
        if(data.subroutines.Count > 0)
        {
            (Subroutine routine, int bits) sub = data.subroutines.First();

            if(data.bits.Take(sub.bits).Any(bit => bit.decrypted == false))
            {
                Debug.Log($"{data.name} Executing");
                sub.routine.Execute();
            }
            else
            {
                // The subroutine was broken previously - this should not happen
                Debug.Log($"{sub.routine.name} is fully broken and cannot Execute"); 
            }

            data.bits.RemoveRange(0, sub.bits);
            data.subroutines.Remove(sub);

        }

        if (data.subroutines.Count > 0)
        {
            Debug.Log($"No Subroutines left on {data.name}; Destroying");
            iceRetireEvent.Invoke(); 
        }
    }
}