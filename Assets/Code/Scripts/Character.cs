using System;
using Assets.Code.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private bool _isSelected = false;

    [SerializeField] private Hability _hability;

    public Health Health;
    public Cooldown Cooldown;

    public void Select()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        _isSelected = true;
    }

    public void Desselect(object sender, EventArgs e)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        _isSelected = false;
    }

    public void Start()
    {
        Health = GetComponentInChildren<Health>();
        Cooldown = GetComponentInChildren<Cooldown>();
        _hability = GetComponent<Hability>();
        PlayerController.HabilityCast += this.CastHability;
        PlayerController.Deselect += this.Desselect;
    }

    private void CastHability(object sender, HabilityCastEventArgs e)
    {
        if (!_isSelected) return;
        if (Cooldown.OnCooldown) return;
        _hability.Cast(e, Cooldown.ResetCooldown);
    }
}
