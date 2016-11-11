using UnityEngine;
using System.Collections;

public class HealAbility : Ability
{
    private GameObject _healingSprite;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Heal;
    }

    protected override bool RequiresTarget()
    {
        return true;
    }

    protected override void Start()
    {
        base.Start();
        if (_healingSprite == null)
        {
            _healingSprite = Instantiate(Resources.Load<GameObject>("Prefabs/UI/HealingSprite"), transform) as GameObject;
            _healingSprite.SetActive(false);
        }
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        GetComponent<SpriteRenderer>().color += Color.yellow;
        GetComponent<SpriteRenderer>().color /= 2;
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, Heal));
    }

    IEnumerator Heal(Character caster, Character charSelected)
    {
        GetComponent<SpriteRenderer>().color *= 2;
        GetComponent<SpriteRenderer>().color -= Color.yellow;
        _healingSprite.SetActive(true);
        _healingSprite.transform.position = charSelected.transform.position;
        //charSelected.GetComponent<SpriteRenderer>().color = Color.yellow;
        for (int i = 0; i < Data.Ticks; i++)
        {
            charSelected.Health.Heal(Power);
            yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
            if (caster.Stun.IsStunned) break;
        }
        //charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        _healingSprite.SetActive(false);
    }
}
