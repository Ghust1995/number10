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
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                StartCoroutine(DoCastLogic(caster, charSelected, Heal));
            }
        }
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
