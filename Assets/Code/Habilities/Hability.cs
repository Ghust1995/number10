using System;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    public abstract class Hability : MonoBehaviour
    {
        protected Cooldown Cooldown;

        protected virtual void Start()
        {
            Cooldown = GetComponentInChildren<Cooldown>();
        }

        public abstract void Cast(HabilityCastEventArgs e);
    }
}
