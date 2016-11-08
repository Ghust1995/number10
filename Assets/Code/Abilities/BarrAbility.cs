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

    protected override void Start()
    {
        base.Start();
        if (_barrierPrefab == null)
        {
            _barrierPrefab = Resources.Load<Barrier>("Prefabs/Barrier");
        }
        _barrier = Instantiate(_barrierPrefab, transform) as Barrier;
        _barrier.SetCenter(transform);
        _barrier.Initialize(Data.Objectspeed);
    }

    public void OnDestroy()
    {
        Destroy(_barrier.gameObject);
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.magenta;
                StartCoroutine(DoCastLogic(caster, charSelected, PutBarrier));
            }
        }
    }

    IEnumerator PutBarrier(Character caster, Character charSelected)
    {
        _barrier.transform.parent = charSelected.transform;
        _barrier.SetCenter(charSelected.transform);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        yield break;
    }
}
