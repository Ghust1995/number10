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
            {AbilityType.Poke, GetComponent<Poke>()},
            //{AbilityType.Stun, GetComponent<Stun>()},
            {AbilityType.Nuke, GetComponent<Nuke>()},
            {AbilityType.Heal, GetComponent<Heal>()},
            {AbilityType.Swap, GetComponent<Swap>()},
            {AbilityType.Barr, GetComponent<Barr>()},
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
