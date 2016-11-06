using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BossCharacter : Character
{
    [SerializeField]
    private AbilityType _abilityType = AbilityType.Poke;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        AbilityBuilder = new Dictionary<AbilityType, Ability>()
        {
            {AbilityType.Poke, GetComponent<PokeAbility>()},
            //{AbilityType.Stun, GetComponent<Stun>()},
            {AbilityType.Nuke, GetComponent<NukeAbility>()},
            {AbilityType.Heal, GetComponent<HealAbility>()},
            {AbilityType.Swap, GetComponent<SwapAbility>()},
            {AbilityType.Barr, GetComponent<BarrAbility>()},
            //{AbilityType.Buff, GetComponent<Buff>()},
            //{AbilityType.Wall, GetComponent<Wall>()},
            //{AbilityType.Tank, GetComponent<Tank>()},
        };
        Ability = AbilityBuilder[_abilityType];
        BossController.AbilityCast += CastAbility;
    }

    void Update()
    {
        Ability = AbilityBuilder[_abilityType];
    }
}
