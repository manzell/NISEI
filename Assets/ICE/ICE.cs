using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ICE : MonoBehaviour
{
    public ICEdata data;

    public bool broken => data.bits.All(bit => bit.decrypted == true);

    public void Awake()
    {
        if (data != null)
            Setup(data); 
    }

    void Setup(ICEdata data)
    {
        CombatManager.turnEndEvent.AddListener(Execute);
        this.data = GameObject.Instantiate(data);        

        foreach ((Subroutine s, int num) sub in this.data.subroutines) 
        {
            for(int n = 0; n < sub.num; n++)
            {
                int totalBitWeight = this.data.bitTypeWeights.Sum(kvp => kvp.Value);
                int bitTypeSeed = Random.Range(0, totalBitWeight);

                int weight = 0; 
                
                foreach (KeyValuePair<Bit, int> kvp in this.data.bitTypeWeights)
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

    public void Execute()
    {
        if(data.subroutines.Count > 0)
        {
            Debug.Log($"{data.name} Executing");
            (Subroutine routine, int bits) sub = data.subroutines.First(); 

            if(data.bits.Take(sub.bits).Any(bit => bit.decrypted == false)) 
                sub.routine.Execute();

            data.bits.RemoveRange(0, sub.bits);

            data.subroutines.Remove(sub); 
        }

        if (data.subroutines.Count > 0)
        {
            Debug.Log($"No Subroutines left on {data.name}; Destroying");
        }
    }
}