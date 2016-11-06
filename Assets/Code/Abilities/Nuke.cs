using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Nuke : Ability
{
    protected override void Cast(AbilityCastEventArgs e)
    {
        Debug.Log("Just Casted a nuke!");
        Cooldown.ResetCooldown();
    }
}
