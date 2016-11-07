using UnityEngine;
using System.Collections;

public class HealAbility : Ability
{
    protected override AbilityType GetAbilityType()
    {
        return AbilityType.Heal;
    }
    
    //[SerializeField]
    //private float _healingDone = ;

    //[SerializeField]
    //private float _timeToHeal;// = ImportData.GetContainer("ability_heal").GetData("cast_time").ToFloat();

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(Heal(caster, charSelected));
            }
        }
    }

    IEnumerator Heal(Character caster, Character charSelected)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        if (caster.Stun.IsStunned) yield break;
        charSelected.Health.Heal(Data.Power);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
