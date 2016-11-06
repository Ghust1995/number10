﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public enum AbilityType
{
    Poke,
    //Stun,
    Nuke,
    Heal,
    Swap,
    Barr,
    //Buff,
    //Wall,
    //Tank,
};

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Character : MonoBehaviour
{
    public Dictionary<AbilityType, Ability> AbilityBuilder;

    protected Ability Ability;

    public Health Health;

    public virtual void Start()
    {
        Health = GetComponentInChildren<Health>();
        // The collider is trigger
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    protected virtual void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if(Ability == null)
        {
            Debug.Log("No ability selected");
            return;
        }
        Ability.Cast(sender, e);
    }
}
