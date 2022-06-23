using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "dataType/i")]
public class IntValue : ScriptableObject
{
    public int Value;
}

[System.Serializable]
public class intRef
{
    public bool constant;
    public int constValue;
    public IntValue intValue = new IntValue();

    public List<Modifier> modifiers = new List<Modifier>();

    public int Value
    {
        get { return baseValue + modification; }
        set { intValue.Value = value; }
    }

    int baseValue
    {
        get { return constant ? constValue : intValue.Value; }
        set { intValue.Value = value; }
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

    public intRef(int i)
    {
        constant = true;
        constValue = i; 
    }
    public intRef(IntValue i)
    {
        constant = false;
        intValue = i; 
    }

    public static implicit operator int(intRef i) { return i.Value; }
    public static explicit operator string(intRef s) => s.Value.ToString();
    public static intRef operator +(intRef a, intRef b) { a.Value += b.Value; return a; }
    public static intRef operator +(intRef a, int b) { a.Value += b; return a; }
    public static intRef operator -(intRef a, intRef b) { a.Value -= b.Value; return a; }
    public static intRef operator -(intRef a, int b) { a.Value -= b; return a; }
}