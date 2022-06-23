using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

[CreateAssetMenu]
public class ICEdata : SerializedScriptableObject
{
    public new string name;
    public List<Bit> bits = new List<Bit>();
    public List<(Subroutine subroutine, int numBits)> subroutines = new List<(Subroutine subroutine, int numBits)>();
    public Dictionary<Bit, int> bitTypeWeights = new Dictionary<Bit, int>(); 

    public GameObject icePrefab; 
}
