using UnityEngine;
using System.Collections;

public class BarrAbility : Ability
{
    [SerializeField]
    private Barrier _barrierPrefab;

    private Barrier _barrier;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Barr;
    }

    public override void IncreasePower(float value)
    {
        base.IncreasePower(value);
        _barrier.transform.localScale *= PowerMultiplier;
    }

    public override void ResetPowerIncrease()
    {
        _barrier.transform.localScale /= PowerMultiplier;
        base.ResetPowerIncrease();
    }

    protected override void Start()
    {
        base.Start();
        if (_barrierPrefab == null)
        {
            _barrierPrefab = Resources.Load<Barrier>("Prefabs/Barrier");
        }
        _barrier = Instantiate(_barrierPrefab);
        _barrier.SetCenter(transform);
        _barrier.Initialize(Data.Objectspeed);
    }

    public void OnDestroy()
    {
        Destroy(_barrier.gameObject);
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, PutBarrier));
    }

    IEnumerator PutBarrier(Character caster, Character charSelected)
    {
        //_barrier.transform.parent = charSelected.transform;
        _barrier.SetCenter(charSelected.transform);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        yield break;
    }
}
