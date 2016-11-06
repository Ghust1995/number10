using System;
using Assets.Code.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerCharacter : Character
{
    [SerializeField]
    private bool _isSelected = false;

    public void Select()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        _isSelected = true;
    }

    public void Deselect(object sender, EventArgs e)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        _isSelected = false;
    }

    public override void Start()
    {
        base.Start();
        Ability = GetComponent<Ability>();
        PlayerController.AbilityCast += this.CastAbility;
        PlayerController.Deselect += this.Deselect;
    }

    protected void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if (!_isSelected) return;
        base.CastAbility(sender, e);
    }
}
