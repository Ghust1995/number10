using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    public abstract class Hability : MonoBehaviour
    {
        public abstract void Cast(HabilityCastEventArgs e, Action resetCooldown);
    }
}
