using System.Collections;
using UnityEngine;

public class PokeAbility : Ability
{
    [SerializeField]
    private PokeBullet _pokeBulletPrefab;

    protected override void Start()
    {
        base.Start();
        if (_pokeBulletPrefab == null)
        {
            _pokeBulletPrefab = Resources.Load<PokeBullet>("Prefabs/PokeBullet");
        }
    }

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Poke;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetEnemy.GetComponent<Character>(), Poke));
    }

    IEnumerator Poke(Character caster, Character target)
    {
        var bullet = Instantiate(_pokeBulletPrefab, this.transform) as PokeBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Power, target.gameObject);
        yield break;
    }
}
