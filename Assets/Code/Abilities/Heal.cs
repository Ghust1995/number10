using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;

public class Heal : Ability
{
    [SerializeField]
    private float _healingDone = 10;

    [SerializeField]
    private float _timeToHeal = 1;

    protected override void Cast(AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(DoHeal(charSelected));
            }
        }
    }

    IEnumerator DoHeal(Character charSelected)
    {
        yield return new WaitForSeconds(_timeToHeal);
        charSelected.Health.Heal(_healingDone);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        Cooldown.ResetCooldown();
    }
}
