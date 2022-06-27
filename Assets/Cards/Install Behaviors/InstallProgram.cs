using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Program/Install")]
public class InstallProgram : PlayBehavior
{
    public Program program;
    public override void Play(Rig rig, Card card) => rig.InstallProgram(program);
}