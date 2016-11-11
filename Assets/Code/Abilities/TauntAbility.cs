using System;
using UnityEngine;
using System.Collections;

public delegate void TauntEventHandler(object sender, TauntEventArgs e);
public class TauntEventArgs : EventArgs
{
    public Character TauntingCharacter;
}

public delegate void TauntOverEventHandler(object sender, EventArgs e);

public class TauntAbility : Ability
{

    public static event TauntEventHandler TauntEvent;
    public static event TauntOverEventHandler TauntOverEvent;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Taunt;
    }

    protected override bool RequiresTarget()
    {
        return true;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, Taunt));
    }

    IEnumerator Taunt(Character caster, Character charSelected)
    {
        if (TauntEvent != null) TauntEvent.Invoke(caster, new TauntEventArgs {TauntingCharacter = charSelected});
        yield return new WaitForSeconds(Effectduration);
        if (TauntOverEvent != null) TauntOverEvent.Invoke(caster, null);
    }
}
