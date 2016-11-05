using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Nuke : Hability
{

    public override void Cast(HabilityCastEventArgs e)
    {
        Debug.Log("Just Casted a nuke!");
        Cooldown.ResetCooldown();
    }
}
