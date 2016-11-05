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
        _hability = GetComponent<Hability>();

        // The collider is trigger
        GetComponent<CircleCollider2D>().isTrigger = true;

        PlayerController.HabilityCast += this.CastHability;
        PlayerController.Deselect += this.Desselect;
    }

    private void CastHability(object sender, HabilityCastEventArgs e)
    {
        if (!_isSelected) return;
        _hability.Cast(e);
    }
}
