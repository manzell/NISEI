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

    ICE ice;

    public Bit(BitType type, ICE ice, int depth)
    {
        this.bitType = type; 
        this.ice = ice;
        this.depth = depth;
        value = Random.Range(0, depth);
    }

    public Bit(ICE ice, int depth)
    {
        this.ice = ice; 
        this.depth = depth;
        value = Random.Range(0, depth);
    }

    public Bit(Bit bit)
    {
        ice = bit.ice;
        depth = bit.depth;
        bitType = bit.bitType;
        decrypted = bit.decrypted; 
        value = Random.Range(0, depth);
    }

    public virtual bool TryDecrypt(int guessVal)
    {
        decrypted = guessVal == value;

        if (decrypted)
        {
            decryptEvent.Invoke();
            DecryptEvent.Invoke();
        }

        return decrypted; 
    }
}