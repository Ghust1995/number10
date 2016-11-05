using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Classes;
using Assets.Code.Interfaces;

public class Poke : Hability
{
    public override void Cast(HabilityCastEventArgs e, Action resetCooldown)
    {
        Debug.Log("Just Casted a poke!");
        resetCooldown();
    }
}
