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

    protected override void Cast(AbilityCastEventArgs e)
    {
        StartCoroutine(Poke(e.TargetEnemy));
    }

    IEnumerator Poke(GameObject target)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        var bullet = Instantiate(_pokeBulletPrefab, this.transform) as PokeBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Data.Power, target);
    }
}
