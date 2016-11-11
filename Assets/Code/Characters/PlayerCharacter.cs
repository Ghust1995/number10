using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerCharacter : Character
{
    [SerializeField]
    private bool _isSelected;

    private GameObject _selectionSprite;

    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            _selectionSprite.SetActive(value);
            _isSelected = value;
        }
    }

    public void Select()
    {
        IsSelected = true;
    }

    public void Deselect(object sender, EventArgs e)
    {
        IsSelected = false;
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
        if (_selectionSprite == null)
        {
            _selectionSprite = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SelectionSprite"), transform) as GameObject;
            _selectionSprite.transform.localPosition = _selectionSprite.transform.position;
            //_selectionSprite.SetActive(false);
        }
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
