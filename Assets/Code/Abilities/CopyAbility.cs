﻿using UnityEngine;
using System.Collections;

public class CopyAbility : Ability
{
    public override AbilityType GetAbilityType()
    {
        return AbilityType.Copy;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                StartCoroutine(DoCastLogic(caster, charSelected, Copy));
            }
        }
    }

    IEnumerator Copy(Character caster, Character charSelected)
    {
        caster.gameObject.GetComponent<CopyAbility>().enabled = false;
        var type = charSelected.GetComponent<Ability>().GetType();
        var component = caster.gameObject.AddComponent(type);
        var copySprite = caster.gameObject.GetComponent<SpriteRenderer>().sprite;
        caster.gameObject.GetComponent<SpriteRenderer>().sprite =
            charSelected.gameObject.GetComponent<SpriteRenderer>().sprite;
        caster.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        caster.IsCasting = false;
        yield return new WaitForSeconds(Data.Effectduration);
        Destroy(component);
        caster.gameObject.GetComponent<CopyAbility>().enabled = true;
        caster.gameObject.GetComponent<SpriteRenderer>().sprite = copySprite;
    }
}
