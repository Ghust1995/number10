using System;
using System.Linq;
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

    protected override void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if (!_isSelected) return;
        base.CastAbility(sender, e);
    }

    public override CharacterType GetCharacterType()
    {
        // Really bad
        if(Ability == null) Ability = GetComponent<Ability>();
        return (CharacterType)Ability.GetAbilityType();
    }

    public override void Start()
    {
        base.Start();
        Ability = GetComponents<Ability>().First((c) => c.enabled);
        PlayerController.AbilityCastEvent += this.CastAbility;
        PlayerController.DeselectEvent += this.Deselect;

        OnDestroyCallbacks += () => {
            PlayerController.AbilityCastEvent -= this.CastAbility;
            PlayerController.DeselectEvent -= this.Deselect;
            _isSelected = false;
        };
    }

    public override void Update()
    {
        Ability = GetComponents<Ability>().First((c) => c.enabled);
        base.Update();
    }
}
