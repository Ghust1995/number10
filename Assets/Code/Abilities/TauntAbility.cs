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

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                StartCoroutine(DoCastLogic(caster, charSelected, Taunt));
            }
        }
    }

    IEnumerator Taunt(Character caster, Character charSelected)
    {
        if(TauntEvent != null) TauntEvent.Invoke(caster, new TauntEventArgs { TauntingCharacter =  charSelected});
        yield return new WaitForSeconds(Data.Effectduration);
        if (TauntOverEvent != null) TauntOverEvent.Invoke(caster, null);
    }
}
