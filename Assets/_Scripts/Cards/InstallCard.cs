using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Card/Install")]
public class InstallCard : Card
{
    [SerializeField] Program program;

    protected override bool CanPlay(Rig rig) => base.CanPlay(rig) && rig.AvailableMemory >= program.MemoryCost;

    protected override void Play(Rig rig)
    {
        rig.InstallProgram(program); 
        base.Play(rig);
    }
}