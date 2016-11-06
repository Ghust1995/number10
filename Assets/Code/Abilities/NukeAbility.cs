using UnityEngine;

public class NukeAbility : Ability
{
    protected override void Cast(AbilityCastEventArgs e)
    {
        Debug.Log("Just Casted a nuke!");
        Cooldown.ResetCooldown();
    }
}
