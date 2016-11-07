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

    protected override void Cast(AbilityCastEventArgs e)
    {
        StartCoroutine(Stun(e.TargetEnemy));
    }

    IEnumerator Stun(GameObject target)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        var bullet = Instantiate(_stunBulletPrefab, this.transform) as StunBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Data.Power, Data.Effectduration, target);
    }
}
