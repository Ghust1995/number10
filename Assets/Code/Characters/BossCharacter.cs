using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BossCharacter : Character
{
    [SerializeField]
    private AbilityType _abilityType = AbilityType.Poke;

    // Use this for initialization
    public override CharacterType GetCharacterType()
    {
        return CharacterType.Boss;
    }

    public override void Start()
    {
        base.Start();
        Ability = AbilityBuilder[_abilityType]();
        BossController.AbilityCast += CastAbility;
        OnDestroyCallbacks += () => { BossController.AbilityCast -= this.CastAbility; };
    }

    public void SetAbility(AbilityType type)
    {
        _abilityType = type;
    }

    public override void Update()
    {
        base.Update();
        Ability = AbilityBuilder[_abilityType]();
    }
}
