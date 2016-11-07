using UnityEngine;
using System.Collections;

public class BarrAbility : Ability
{
    [SerializeField]
    private Barrier _barrierPrefab;

    private Barrier _barrier;

    protected override AbilityType GetAbilityType()
    {
        return AbilityType.Barr;
    }

    protected override void Start()
    {
        base.Start();
        _barrier = Instantiate(_barrierPrefab);
        _barrier.transform.parent = transform;
        _barrier.SetCenter(transform);
        _barrier.Initialize(Data.Objectspeed);
    }

    protected override void Cast(AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.magenta;
                StartCoroutine(PutBarrier(charSelected));
            }
        }
    }

    IEnumerator PutBarrier(Character charSelected)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Cooldown);        
        _barrier.transform.parent = charSelected.transform;
        _barrier.SetCenter(charSelected.transform);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
