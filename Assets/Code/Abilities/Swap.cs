using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;

public class Swap : Ability
{
    public const float TimeToSwap = 1.0f;

    [SerializeField]
    private Character _char1;
    [SerializeField]
    private Character _char2;

    protected override void Cast(AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                charSelected.GetComponent<SpriteRenderer>().color = Color.cyan;
                if (_char1 == null)
                {
                    _char1 = charSelected;
                }
                else if (_char2 == null)
                {
                    _char2 = charSelected;
                    // Swap characters after some time
                    StartCoroutine(SwapCharacters());
                }
            }
        }
    }

    IEnumerator SwapCharacters()
    {
        yield return new WaitForSeconds(TimeToSwap);
        var char1pos = _char1.transform.position;
        _char1.transform.position = _char2.transform.position;
        _char2.transform.position = char1pos;
        _char1.GetComponent<SpriteRenderer>().color = Color.white;
        _char2.GetComponent<SpriteRenderer>().color = Color.white;
        _char1 = null;
        _char2 = null;
        Cooldown.ResetCooldown();
    }
}
