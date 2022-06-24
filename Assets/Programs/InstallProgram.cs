using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Program/Install")]
public class InstallProgram : PlayBehavior
{
    [SerializeField] Program programData;
    public override void Do(Rig rig, Card card) => rig.InstallProgram(programData);
}