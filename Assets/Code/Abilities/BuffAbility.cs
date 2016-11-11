using UnityEngine;
using System.Collections;

public class BuffAbility : Ability
{
    public override AbilityType GetAbilityType()
    {
        return AbilityType.Buff;
    }

    protected override bool RequiresTarget()
    {
        return true;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        if (e.TargetedCharacter.GetType() != caster.GetType()) return;
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, Buff));
    }

    IEnumerator Buff(Character caster, Character charSelected)
    {
        charSelected.Buff(Data.Power);
        //yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
        yield return new WaitForSeconds(Effectduration);
        charSelected.EndBuff();
    }
}
