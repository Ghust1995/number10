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
        StartCoroutine(Stun(caster, e.TargetEnemy));
    }

    IEnumerator Stun(Character caster, GameObject target)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        if (caster.Stun.IsStunned) yield break;
        var bullet = Instantiate(_stunBulletPrefab, this.transform) as StunBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Data.Power, Data.Effectduration, target);
    }
}
