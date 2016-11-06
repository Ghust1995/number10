using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public enum AbilityType
{
    Poke,
    Stun,
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
    public Stun Stun;

    public bool CanCastAbility
    {
        get
        {
            return (this.Ability != null)
                   && (!Stun.IsStunned)
                   && (!Ability.Cooldown.OnCooldown);

        }
    }

    public virtual void Start()
    {
        Health = GetComponentInChildren<Health>();
        Stun = GetComponentInChildren<Stun>();
        // The collider is trigger
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public virtual void Update()
    {
        if (Health.Value <= 0.0)
        {
            Destroy(gameObject);
        }
    }

    public delegate void OnDestroyCallback();
    public OnDestroyCallback OnDestroyCallbacks;
    public void OnDestroy()
    {
        OnDestroyCallbacks.Invoke();
    }

    protected virtual void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if(Ability == null)
        {
            Debug.Log("No ability selected");
            return;
        }
        if (Stun.IsStunned) return;
        Ability.Cast(sender, e);
    }
}
