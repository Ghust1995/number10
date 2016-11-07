using UnityEngine;
using System.Collections;

public class HealAbility : Ability
{
    [SerializeField]
    [ImportData("ability_heal", "healing_done", ImportType.Float)]
    private float _healingDone;

    [SerializeField]
    [ImportData("ability_heal", "cast_time", ImportType.Float)]
    private float _timeToHeal;// = ImportData.GetContainer("ability_heal").GetData("cast_time").ToFloat();

    protected override void Cast(AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(Heal(charSelected));
            }
        }
    }

    IEnumerator Heal(Character charSelected)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(_timeToHeal);
        charSelected.Health.Heal(_healingDone);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
