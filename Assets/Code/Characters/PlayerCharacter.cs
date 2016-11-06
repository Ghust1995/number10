using System;
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

    protected void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if (!_isSelected) return;
        base.CastAbility(sender, e);
    }

    public override void Start()
    {
        base.Start();
        Ability = GetComponent<Ability>();
        PlayerController.AbilityCast += this.CastAbility;
        PlayerController.Deselect += this.Deselect;
        OnDestroyCallbacks += () => {
            PlayerController.AbilityCast -= this.CastAbility;
            PlayerController.Deselect -= this.Deselect;
            _isSelected = false;
        };
    }

    public override void Update()
    {
        base.Update();
    }
}
