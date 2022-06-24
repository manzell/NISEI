using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public enum ModifierType { Flat, Multiplicitave }

    public ModifierType type;
    public float value; 

    public Modifier(ModifierType type, float value)
    {
        this.type = type;
        this.value = value; 
    }

    public float Value(floatRef r) => type == ModifierType.Flat ? value : value * r.baseValue;
    public int Value(intRef i) => type == ModifierType.Flat ? (int)value : (int)(value * i.baseValue);
    public float Value(IModifiable i) => type == ModifierType.Flat ? value : (value * i.baseValue);
}

public interface IModifiable
{
    public List<Modifier> modifiers { get; set; }
    public float Value { get; set; }
    public float baseValue { get; set; }
}
