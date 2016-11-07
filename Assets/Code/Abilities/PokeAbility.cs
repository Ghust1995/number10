using System.Collections;
using UnityEngine;

public class PokeAbility : Ability
{
    [SerializeField]
    private PokeBullet _pokeBulletPrefab;

    protected override AbilityType GetAbilityType()
    {
        return AbilityType.Poke;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(Poke(caster, e.TargetEnemy));
    }

    IEnumerator Poke(Character caster, GameObject target)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        if (caster.Stun.IsStunned) yield break;
        var bullet = Instantiate(_pokeBulletPrefab, this.transform) as PokeBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Data.Power, target);
    }
}
