using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Nuke : Hability
{

    public override void Cast(HabilityCastEventArgs e, Action resetCooldown)
    {
        Debug.Log("Just Casted a nuke!");
        resetCooldown();
    }
}
