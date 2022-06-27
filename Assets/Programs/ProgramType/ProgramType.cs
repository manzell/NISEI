using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Program/Program Type")]
public class ProgramType : ScriptableObject
{
    public new string name;
    public List<ICEType> targetBitTypes; 
}
