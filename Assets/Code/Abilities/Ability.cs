using System;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    public abstract class Ability : MonoBehaviour
    {
        protected Cooldown Cooldown;

        protected virtual void Start()
        {
            Cooldown = GetComponentInChildren<Cooldown>();
        }

        public void Cast(object sender, AbilityCastEventArgs e)
        {
            if (Cooldown.OnCooldown) return;
            Cast(e);
        }

        protected abstract void Cast(AbilityCastEventArgs e);
    }
}
