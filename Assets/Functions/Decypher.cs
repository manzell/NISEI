using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu]
public class Decypher : ProgramBehavior
{
    void Execute(ICE ice, Program program, Executable exe)
    {
        ICEdata iceData = ice.data;
        ProgramData programData = program.data;
        List<Bit> addressableBits = iceData.bits.Where(bit => bit.decrypted == false && (int)bit.bitType == (int)programData.programType).ToList();

        if (addressableBits.Count > 0)
        {
            int attemptsPerBit = (int)Mathf.Pow(2, programData.decryptionLevel);
            int numBitsAddressed = Mathf.Min(iceData.bits.Sum(bit => bit.depth) / exe.cycles, addressableBits.Count);

            Debug.Log($"|{exe.name}>{exe.cycles} :: Will address {numBitsAddressed} bits up to {attemptsPerBit} times");

            for (int i = 0; i < numBitsAddressed; i++)
            {
                Bit bit = addressableBits[i];

                for (int n = 0; n < attemptsPerBit; n++)
                {
                    int max = (int)Mathf.Min(Mathf.Pow(programData.decryptionLevel, 2), bit.depth);
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
            Debug.Log($"|{exe.name}>No uncracked bits on {iceData.name}; Decryption ending"); 
        }
    }

    public override Executable GetExecutable(Program program)
    {
        ICE ice = CombatManager.targetIce; 

        Executable exe = new Executable(executable => Execute(ice, program, executable));
        exe.cycles = new intRef(program.data.cycleCost);
        exe.name = $"{program.data.name}({ice.name})";
        exe.description = $"Cycles: {exe.cycles}"; 

        return exe; 
    }
}