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

    protected override bool RequiresTarget()
    {
        return true;
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
            _barrierPrefab = Resources.Load<Barrier>("Prefabs/Mechanics/Barrier");
        }
        
        _barrier = Instantiate(_barrierPrefab);
        _barrier.SetCenter(transform);
        _barrier.Initialize(Data.Objectspeed);
        _barrier.Owner = gameObject.GetComponent<Character>();
        _barrier.Owner.OnDestroyCallbacks += OnOwnerDestroy;
    }

    public void OnDestroy()
    {
        //Destroy(_barrier.gameObject);
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        GetComponent<SpriteRenderer>().color += Color.magenta;
        GetComponent<SpriteRenderer>().color /= 2;
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, PutBarrier));
    }

    IEnumerator PutBarrier(Character caster, Character charSelected)
    {
        GetComponent<SpriteRenderer>().color *= 2;
        GetComponent<SpriteRenderer>().color -= Color.magenta;
        if (_barrier.Owner != null)
        {
            _barrier.Owner.OnDestroyCallbacks -= OnOwnerDestroy;
        }
        _barrier.gameObject.SetActive(true);
        //_barrier.transform.parent = charSelected.transform;
        _barrier.Owner = charSelected;
        _barrier.SetCenter(charSelected.transform);
        
        _barrier.Owner.OnDestroyCallbacks += OnOwnerDestroy;
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        yield break;
    }

    void OnOwnerDestroy()
    {
        if (_barrier == null) return;
        _barrier.Owner.OnDestroyCallbacks -= OnOwnerDestroy;
        _barrier.Owner = null;
        _barrier.gameObject.SetActive(false);
    }
}
