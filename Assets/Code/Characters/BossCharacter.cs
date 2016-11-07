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
            {AbilityType.Stun, GetComponent<StunAbility>()},
            {AbilityType.Nuke, GetComponent<NukeAbility>()},
            {AbilityType.Heal, GetComponent<HealAbility>()},
            {AbilityType.Swap, GetComponent<SwapAbility>()},
            {AbilityType.Barr, GetComponent<BarrAbility>()},
            //{AbilityType.Buff, GetComponent<BuffAbility>()},
            //{AbilityType.Wall, GetComponent<WallAbility>()},
            //{AbilityType.Tank, GetComponent<TankAbility>()},
        };
        Ability = AbilityBuilder[_abilityType];
        BossController.AbilityCast += CastAbility;
        OnDestroyCallbacks += () => { BossController.AbilityCast -= this.CastAbility; };
        Stun.DoStun(10);
    }

    public void SetAbility(AbilityType type)
    {
        _abilityType = type;
    }

    public override void Update()
    {
        base.Update();
        Ability = AbilityBuilder[_abilityType];
    }
}
