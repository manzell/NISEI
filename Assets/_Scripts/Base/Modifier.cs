using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public enum ModifierType { Flat, Multiplicitave }
    public float BaseValue => baseValue;
    public ModifierType Type => type; 

    [SerializeField] ModifierType type;
    [SerializeField] float baseValue;

    public Modifier(ModifierType type, float value)
    {
        this.type = type;
        baseValue = value; 
    }

    public float Value(floatRef f) => Type == ModifierType.Flat ? BaseValue : BaseValue * f.BaseValue;
    public int Value(intRef i) => Type == ModifierType.Flat ? (int)BaseValue : (int)(BaseValue * i.BaseValue);
    public float Value(IModifiable m) => Type == ModifierType.Flat ? BaseValue : (BaseValue * m.BaseValue);
}

public interface IModifiable
{
    public List<Modifier> modifiers { get; set; }
    public float Value { get; set; }
    public float BaseValue { get; set; }
}
