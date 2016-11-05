using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Poke : Hability
{
	public GameObject pokeBulletPrefab;

    public override void Cast(HabilityCastEventArgs e)
    { 
		Boss boss;
		GameObject bullet;
		boss = FindObjectOfType<Boss>();
		Vector3 direction;
		direction = boss.transform.position;
		direction -= transform.position;
		bullet = Instantiate (pokeBulletPrefab);
		bullet.transform.position = transform.position;
		float directionAngle;
		directionAngle = Vector3.Angle(direction,new Vector3(1,0,0));
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);
		Debug.Log("Just Casted a poke!");

        Cooldown.ResetCooldown();
    }
}
