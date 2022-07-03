using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Program/ICEBreaker")]
public class Decypher : Program
{
    public enum Style { Breadth, Depth}    
    public Style approach;

    [Range(0f,1f)] public float widthFactor;

    public void DecypherICE(ICE ice, Executable exe)
    {
        List<ICEType> targetIceTypes = new List<ICEType>();

        foreach (ProgramType type in programTypes)
            targetIceTypes.AddRange(type.targetBitTypes);

        List<Bit> addressableBits = new List<Bit>();

        foreach (Subroutine sub in ice.Subroutines)
            addressableBits.AddRange(sub.bits.Where(bit => bit.decrypted == false && targetIceTypes.Contains(bit.bitType)));

        int power = (int)(powerLevel * ServerManager.currentRig.clockSpeed);

        if (addressableBits.Count > 0)
        {
            float factor = Mathf.Max(Mathf.Sqrt(power) * widthFactor, 1f); // The closer to 1 the width factor is, the more square it is

            int numBitsAddressed = Mathf.Min((int)(power / factor), addressableBits.Count);
            int attemptsPerBit = power / numBitsAddressed;

            if (approach == Style.Depth)
            {
                numBitsAddressed += attemptsPerBit;
                attemptsPerBit = numBitsAddressed - attemptsPerBit;
                numBitsAddressed -= attemptsPerBit;
            }
            
            Debug.Log($"|{exe.Name}>{exe.cycles} :: {numBitsAddressed}x{attemptsPerBit} bitfield address");

            for (int i = 0; i < numBitsAddressed; i++)
            {
                Bit bit = addressableBits[i];

                for (int n = 0; n < attemptsPerBit; n++)
                {
                    int max = (int)Mathf.Min(Mathf.Pow(powerLevel + 1, 2), bit.depth);
                    int guess = Random.Range(0, max);

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
    }

    public override Executable GetExecutable(Program program)
    {
        ICE ice = ServerManager.currentIce; 

        Executable exe = new Executable(executable => DecypherICE(ice, executable));
        exe.cycles = program.ExecutionCost;
        exe.Name = $"{program.name}({ice.name})";
        exe.Desc = $"Cycles: {(string)exe.cycles}";

        return exe; 
    }
}