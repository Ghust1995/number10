using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Code.Interfaces;

public class BossCharacter : Character
{
    private List<Hability> habilities;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        Hability = GetComponent<Hability>();
        PlayerController.HabilityCast += this.CastHability;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
