using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;

public class Barr : Hability
{
    [SerializeField]
    private Barrier _barrierPrefab;

    private Barrier _barrier;

    [SerializeField]
    private float _healingDone = 10;

    [SerializeField]
    private float _timeToHeal = 1;

    void Start()
    {
        _barrier = Instantiate(_barrierPrefab);
        _barrier.SetCenter(transform);
    }

    public override void Cast(HabilityCastEventArgs e, Action resetCooldown)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.magenta;
                StartCoroutine(PutBarrier(charSelected, resetCooldown));
            }
        }
    }

    IEnumerator PutBarrier(Character charSelected, Action resetCooldown)
    {
        yield return new WaitForSeconds(_timeToHeal);
        _barrier.SetCenter(charSelected.transform);
        charSelected.GetComponent<SpriteRenderer>().color = Color.white;
        resetCooldown();
    }
}
