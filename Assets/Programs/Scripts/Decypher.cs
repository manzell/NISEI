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
        foreach (ProgramType _programType in programTypes)
            targetIceTypes.Union(_programType.targetBitTypes); 

        List<Bit> addressableBits = ice.bits.Where(bit => bit.decrypted == false && targetIceTypes.Contains(bit.bitType)).ToList();

        int OverallPower = (int)(powerLevel * ServerManager.currentRig.clockSpeed);

        if (addressableBits.Count > 0)
        {
            float factor = Mathf.Min(Mathf.Sqrt(OverallPower) * widthFactor, 1f); // The closer to 1 the width factor is, the more square it is

            int numBitsAddressed = Mathf.Max((int)(OverallPower / factor), addressableBits.Count);
            int attemptsPerBit = OverallPower / numBitsAddressed;

            if (approach == Style.Depth)
            {
                numBitsAddressed = numBitsAddressed + attemptsPerBit;
                attemptsPerBit = numBitsAddressed - attemptsPerBit;
                numBitsAddressed = numBitsAddressed - attemptsPerBit;
            }

            Debug.Log($"|{exe.name}>{exe.cycles} :: {numBitsAddressed}x{attemptsPerBit} bitfield address");

            /*
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
            */
        }
        else
        {
            Debug.Log($"|{exe.name}>No uncracked bits on {ice.name}; Decryption ending"); 
        }
    }

    public override Executable GetExecutable(Program program)
    {
        ICE ice = ServerManager.currentIce; 

        Executable exe = new Executable(executable => DecypherICE(ice, executable));
        exe.cycles = program.executionCost;
        exe.name = $"{program.name}({ice.name})";
        exe.description = $"Cycles: {exe.cycles}"; 

        return exe; 
    }
}