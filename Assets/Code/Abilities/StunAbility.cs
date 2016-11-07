using System.Collections;
using UnityEngine;

public class StunAbility : Ability
{
    [SerializeField]
    private StunBullet _stunBulletPrefab;

    protected override AbilityType GetAbilityType()
    {
        return AbilityType.Stun;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetEnemy.GetComponent<Character>(), Stun));
    }

    IEnumerator Stun(Character caster, Character target)
    {
        var bullet = Instantiate(_stunBulletPrefab, this.transform) as StunBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Data.Power, Data.Effectduration, target.gameObject);
        yield break;
    }
}
