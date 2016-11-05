using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;
using Assets.Code.Classes;
using Assets.Code.Interfaces;

public class Heal : Hability
{
    [SerializeField]
    private float _healingDone = 10;

    [SerializeField]
    private float _timeToHeal = 1;

    public override void Cast(HabilityCastEventArgs e, Action resetCooldown)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
                StartCoroutine(DoHeal(charSelected, resetCooldown));
            }
        }
        resetCooldown();
    }

    IEnumerator DoHeal(Character charSelected, Action resetCooldown)
    {
        yield return new WaitForSeconds(_timeToHeal);
        charSelected.Health.Heal(_healingDone);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        resetCooldown();
    }
}
