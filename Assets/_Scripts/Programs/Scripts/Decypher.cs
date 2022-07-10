using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

[CreateAssetMenu(menuName ="Program/ICEBreaker")]
public class Decypher : Program
{
    enum Style { Breadth, Depth}    
    public List<ProgramType> Types => types;
    public floatRef PowerLevel => powerLevel;

    [SerializeField] Style approach;
    [SerializeField] List<ProgramType> types = new List<ProgramType>();
    [SerializeField] floatRef powerLevel;

    [SerializeField] [Range(0f, 1f)] float widthFactor;

    public void DecypherICE(ICE ice, Executable exe, UnityAction callback)
    {
        List<BitType> targetIceTypes = new List<BitType>();
        List<Bit> addressableBits = new List<Bit>();
        int power = (int)(PowerLevel * ServerManager.currentRig.clockSpeed);

        foreach (ProgramType type in types)
            targetIceTypes.AddRange(type.targetBitTypes);

        foreach (Subroutine sub in ice.Subroutines)
            addressableBits.AddRange(sub.bits.Where(bit => bit.decrypted == false && targetIceTypes.Contains(bit.bitType)));

        if (addressableBits.Count > 0)
        {
            float factor = Mathf.Max(Mathf.Sqrt(power) * widthFactor, 1f); // The closer to 1 the width factor is, the more square it is

            int numBitsAddressed = Mathf.Min((int)(power / factor), addressableBits.Count);
            int attemptsPerBit = power / numBitsAddressed;
            int maxDepthGuess = (int)Mathf.Pow(PowerLevel + 1, 2); 

            if (approach == Style.Depth)
            {
                numBitsAddressed += attemptsPerBit;
                attemptsPerBit = numBitsAddressed - attemptsPerBit;
                numBitsAddressed -= attemptsPerBit;
            }
            
            Debug.Log($"{exe.Name}>{exe.cycles} :: {numBitsAddressed}({addressableBits.Count})x{attemptsPerBit}::{maxDepthGuess} bitfield address");

            for (int i = 0; i < numBitsAddressed; i++)
            {
                Bit bit = addressableBits[i];

                for (int n = 0; n < attemptsPerBit; n++)
                {
                    int guess = Random.Range(0, Mathf.Min(maxDepthGuess, bit.depth));

                    if (bit.TryDecrypt(guess))
                    {
                        Debug.Log($"{bit}#{i} decyphered after {n + 1} {(n == 0 ? "attempt" : "attempts!")}");
                        break;
                    }
                }
                if(bit.decrypted == false)
                    Debug.Log($"{bit}#{i} failed ({attemptsPerBit} attempts)");
            }
        }
        else
        {
            Debug.Log($"|{exe.Name}>No crackable bits on {ice.name}> Decryption ending"); 
        }

        callback?.Invoke(); 
    }

    public override Executable GetExecutable()
    {
        ICE ice = ServerManager.currentIce; 
        Executable exe = new Executable(ExecutionCost);

        exe.Set(callback => DecypherICE(ice, exe, callback)); 
        exe.Name = $"{name}({ice.name})";
        exe.Desc = $"Cycles: {(string)exe.cycles}";

        return exe; 
    }
}