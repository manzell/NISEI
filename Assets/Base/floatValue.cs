using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="dataType/float")]
public class FloatValue : ScriptableObject
{
    public float Value; 
}

[System.Serializable]
public class floatRef : IModifiable
{
    public bool constant;
    public float constValue;
    public FloatValue floatValue;

    public List<Modifier> modifiers { get; set; } = new List<Modifier>();

    float modification
    {
        get
        {
            float flat = 0,
                percent = 0;

            foreach (Modifier mod in modifiers)
                if (mod.Type == Modifier.ModifierType.Flat)
                    flat += mod.BaseValue;
                else
                    percent += mod.BaseValue;

            return BaseValue * percent + flat; 
        }
    }

    public float BaseValue
    {
        get { return constant ? constValue : floatValue.Value; }
        set { floatValue.Value = value; }
    }

    public float Value
    {
        get { return BaseValue + modification; }
        set { floatValue.Value = value; }
    }

    public floatRef(FloatValue f)
    {
        constant = false;
        floatValue = f; 
    }
    public floatRef(float f)
    {
        constant = true;
        constValue = f;
    }

    public static implicit operator float(floatRef f) => f.BaseValue;
    public static implicit operator string(floatRef f) => f.BaseValue .ToString("#.##");
    public static floatRef operator +(floatRef a, floatRef b) { a.Value += b.Value; return a; }
    public static floatRef operator -(floatRef a, floatRef b) { a.Value -= b.Value; return a; }
}