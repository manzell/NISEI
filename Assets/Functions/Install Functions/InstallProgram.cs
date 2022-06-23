using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InstallProgram : PlayBehavior
{
    [SerializeField] ProgramData programData;
    public override void Do(Rig rig, Card card) => rig.Install(programData);
}
