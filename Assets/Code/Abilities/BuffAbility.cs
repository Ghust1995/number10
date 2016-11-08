using UnityEngine;
using System.Collections;

public class BuffAbility : Ability
{
    public override AbilityType GetAbilityType()
    {
        return AbilityType.Buff;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                StartCoroutine(DoCastLogic(caster, charSelected, Buff));
            }
        }
    }

    IEnumerator Buff(Character caster, Character charSelected)
    {
        charSelected.Buff(Data.Power);
        //yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
        yield return new WaitForSeconds(Data.Effectduration);
        charSelected.EndBuff();
    }
}
