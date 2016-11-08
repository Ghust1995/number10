using UnityEngine;
using System.Collections;

public class HealAbility : Ability
{
    public override AbilityType GetAbilityType()
    {
        return AbilityType.Heal;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, Heal));
    }

    IEnumerator Heal(Character caster, Character charSelected)
    {
        charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
        for (int i = 0; i < Data.Ticks; i++)
        {
            charSelected.Health.Heal(Power);
            yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
            if (caster.Stun.IsStunned) break;
        }
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
