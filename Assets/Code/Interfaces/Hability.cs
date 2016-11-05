using System;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    public abstract class Hability : MonoBehaviour
    {
        public abstract void Cast(HabilityCastEventArgs e, Action resetCooldown);
    }
}
