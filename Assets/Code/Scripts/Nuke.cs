using UnityEngine;
using System.Collections;
using Assets.Code.Classes;

public class Nuke : BaseCharacter
{

    protected override void CastHability(HabilityCastEventArgs e)
    {
        Debug.Log("Just Casted a nuke!");
    }
}
