using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

[CreateAssetMenu(menuName ="Subroutine/Sub")]
public class Subroutine : ScriptableObject
{
    public new string name;
    public string description;
    public List<Bit> bits { get; private set; } = new List<Bit>();

    [SerializeField] Program program;
    [SerializeField] BitType bitType;
    [SerializeField] int numBits, bitDepth;

    public Program Program => program; 

    public void Execute() => program.GetExecutable().Execute();

    private void OnEnable()
    {
        for (int i = 0; i < numBits; i++)
            bits.Add(new Bit(bitType, bitDepth));
    }
}
