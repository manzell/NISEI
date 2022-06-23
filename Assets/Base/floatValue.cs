using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="dataType/float")]
public class FloatValue : ScriptableObject
{
    public float Value; 
}

[System.Serializable]
public class floatRef
{
    public bool constant;
    public float constValue;
    public FloatValue floatValue;

    public List<Modifier> modifiers = new List<Modifier>();

    float modification
    {
        get
        {
            float flat = 0,
                percent = 0;

            foreach (Modifier mod in modifiers)
                if (mod.type == Modifier.ModifierType.Flat)
                    flat += mod.value;
                else
                    percent += mod.value;

            return baseValue * percent + flat; 
        }
    }


    public float baseValue
    {
        get { return constant ? constValue : floatValue.Value; }
        set { floatValue.Value = value; }
    }

    public float Value
    {
        get { return baseValue + modification; }
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

    public static implicit operator float(floatRef f) => f.baseValue;
    public static implicit operator string(floatRef f) => f.baseValue .ToString("#.##");
    public static floatRef operator +(floatRef a, floatRef b) { a.Value += b.Value; return a; }
    public static floatRef operator -(floatRef a, floatRef b) { a.Value -= b.Value; return a; }
}