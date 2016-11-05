using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Classes
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private bool _isSelected = false;

        public float Cooldown = 5;

        [SerializeField]
        private float _timeSinceLastSkill;

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

        void ResetCooldown()
        {
            _timeSinceLastSkill = Cooldown;
        }

        void Update()
        {
            _timeSinceLastSkill -= Time.deltaTime;
        }

        public void Start()
        {
            PlayerController.HabilityCast += this.CastHability;
            PlayerController.Deselect += this.Desselect;
        }

        private void CastHability(object sender, HabilityCastEventArgs e)
        {
            if (!_isSelected) return;
            if (_timeSinceLastSkill > 0) return;
            this.CastHability(e);
            ResetCooldown();
        }

        protected abstract void CastHability(HabilityCastEventArgs e);
    }
}
