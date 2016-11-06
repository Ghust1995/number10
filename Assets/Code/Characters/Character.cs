using System;
using Assets.Code.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
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
