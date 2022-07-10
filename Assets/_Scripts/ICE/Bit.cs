using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[System.Serializable]
public class Bit
{
    public BitType bitType; 
    public int depth;
    public int value { get; private set; }
    public bool decrypted = false;

    public static UnityEvent TestEvent = new UnityEvent(); 
    [HideInInspector] public UnityEvent testEvent = new UnityEvent();

    public static UnityEvent DecryptEvent = new UnityEvent();
    [HideInInspector] public UnityEvent decryptEvent = new UnityEvent();

    public Bit(BitType type, int depth)
    {
        this.bitType = type; 
        this.depth = depth;
        value = Random.Range(0, depth);
    }

    public Bit(int depth)
    {
        this.depth = depth;
        value = Random.Range(0, depth);
    }

    public Bit(Bit bit)
    {
        depth = bit.depth;
        bitType = bit.bitType;
        decrypted = bit.decrypted; 
        value = Random.Range(0, depth);
    }

    public virtual bool TryDecrypt(int guessVal)
    {
        testEvent.Invoke();
        TestEvent.Invoke(); 
        decrypted = guessVal == value;

        if (decrypted)
        {
            decryptEvent.Invoke();
            DecryptEvent.Invoke();
        }

        return decrypted; 
    }
}