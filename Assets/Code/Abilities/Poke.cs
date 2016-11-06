using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Poke : Ability
{
	public PokeBullet pokeBulletPrefab;

    protected override void Cast(AbilityCastEventArgs e)
    {
		Vector3 direction = FindObjectOfType<BossCharacter>().transform.position - transform.position;
        float directionAngle = Vector3.Angle(direction, new Vector3(1, 0, 0));

        var bullet = Instantiate(pokeBulletPrefab);
        bullet.Target = FindObjectOfType<BossCharacter>().gameObject;
        bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);

        Cooldown.ResetCooldown();
    }
}
