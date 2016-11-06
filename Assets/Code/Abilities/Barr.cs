using UnityEngine;
using System.Collections;

public class Barr : Ability
{
    [SerializeField]
    private Barrier _barrierPrefab;

    private Barrier _barrier;

    [SerializeField]
    private float _timeToApplyBarrier = 1;

    protected override void Start()
    {
        base.Start();
        _barrier = Instantiate(_barrierPrefab);
        _barrier.SetCenter(transform);
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
        yield return new WaitForSeconds(_timeToApplyBarrier);
        _barrier.SetCenter(charSelected.transform);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
