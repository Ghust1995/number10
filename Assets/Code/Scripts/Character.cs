using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.Classes
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private bool _isSelected = false;

        public float Cooldown = 5;

        [SerializeField]
        private float _timeSinceLastSkill;

        [SerializeField]
        private Hability _hability;

        [SerializeField]
        private Health _health;

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

        protected void ResetCooldown()
        {
            _timeSinceLastSkill = Cooldown;
        }

        void Update()
        {
            _timeSinceLastSkill -= Time.deltaTime;
        }

        public void Start()
        {
            _hability = GetComponent<Hability>();
            _health = GetComponentInChildren<Health>();
            PlayerController.HabilityCast += this.CastHability;
            PlayerController.Deselect += this.Desselect;
        }

        private void CastHability(object sender, HabilityCastEventArgs e)
        {
            if (!_isSelected) return;
            if (_timeSinceLastSkill > 0) return;
            _hability.Cast(e, ResetCooldown);
        }
    }
}
