using System.Collections;
using UnityEngine;

public class StunAbility : Ability
{
    [SerializeField]
    private StunBullet _stunBulletPrefab;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Stun;
    }

    protected override void Start()
    {
        base.Start();
        if (_stunBulletPrefab == null)
        {
            _stunBulletPrefab = Resources.Load<StunBullet>("Prefabs/StunBullet");
        }
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetEnemy.GetComponent<Character>(), Stun));
    }

    IEnumerator Stun(Character caster, Character target)
    {
        var bullet = Instantiate(_stunBulletPrefab, this.transform) as StunBullet;
        bullet.transform.localPosition = Vector3.zero;
        bullet.Initialize(Data.Objectspeed, Power, Data.Effectduration, target.gameObject);
        yield break;
    }
}
