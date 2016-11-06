using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Interfaces;

public class BossCharacter : Character
{
    [SerializeField]
    private List<Ability> _habilities;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        _habilities = GetComponents<Ability>().ToList();
        BossController.AbilityCast += this.CastAbility;
    }
}
