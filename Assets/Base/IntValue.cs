using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "dataType/int")]
public class IntValue : ScriptableObject
{
    public int Value;
}

[System.Serializable]
public class intRef : IModifiable
{
    public string name => intValue?.name; 
    public bool constant;
    public int constValue;
    public IntValue intValue;

    public List<Modifier> modifiers { get; set; } = new List<Modifier>(); 

    public intRef()
    {
        modifiers = new List<Modifier>(); 
    }

    public intRef(int i)
    {
        constant = true;
        constValue = i;
        modifiers = new List<Modifier>();
    }
    public intRef(IntValue i)
    {
        constant = false;
        intValue = GameObject.Instantiate(i);
        modifiers = new List<Modifier>();
    }
    public intRef(intRef i)
    {
        constant = i.constant;
        modifiers = i.modifiers;
        if (i.constant)
            constValue = i.constValue;
        else
            intValue = i.intValue;
    }

    public float Value
    {
        get { return baseValue + modification; }
        set { intValue.Value = (int)value; }
    }

    public float baseValue
    {
        get { return constant ? constValue : intValue.Value; }
        set { intValue.Value = (int)value; }
    }

    int modification
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

            return (int)(baseValue * percent + flat);
        }
    }

    public static implicit operator int(intRef i) { return (int)i.Value; }
    public static implicit operator string(intRef s) => s.Value.ToString();
    public static intRef operator +(intRef a, intRef b) { a.Value += b.Value; return a; }
    public static intRef operator +(intRef a, int b) { a.Value += b; return a; }
    public static intRef operator -(intRef a, intRef b) { a.Value -= b.Value; return a; }
    public static intRef operator -(intRef a, int b) { a.Value -= b; return a; }
}