using System;
using Assets.Code.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Hability Hability;

    public Health Health;

    public virtual void Start()
    {
        Health = GetComponentInChildren<Health>();
        // The collider is trigger
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    protected virtual void CastHability(object sender, HabilityCastEventArgs e)
    {
        Hability.Cast(e);
    }
}
