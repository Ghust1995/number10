using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Poke : Hability
{
	public GameObject pokeBulletPrefab;

    public override void Cast(HabilityCastEventArgs e, Action resetCooldown)
    { 
		GameObject boss;
		GameObject poke;
		GameObject bullet;
		boss = GameObject.FindGameObjectWithTag("Boss");
		poke = GameObject.Find ("Poke");
		Vector3 direction;
		direction = boss.transform.position;
		direction -= poke.transform.position;
		bullet = Instantiate (pokeBulletPrefab);
		bullet.transform.position = poke.transform.position;
		float directionAngle;
		directionAngle = Vector3.Angle(direction,new Vector3(1,0,0));
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);
		Debug.Log("Just Casted a poke!");

        resetCooldown();
    }
}
