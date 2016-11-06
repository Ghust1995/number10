using System;
using UnityEngine;
using Assets.Code.Interfaces;

public class Poke : Ability
{
	public PokeBullet pokeBulletPrefab;

    protected override void Cast(AbilityCastEventArgs e)
    {
		Vector3 direction = e.targetEnemy.transform.position - transform.position;
		float directionAngle = Mathf.Atan2(direction.y, direction.x);
		Debug.Log (directionAngle);
        var bullet = Instantiate(pokeBulletPrefab);
		directionAngle *= 360 / (2 * Mathf.PI);
		bullet.Target = e.targetEnemy;
        bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);

        Cooldown.ResetCooldown();
    }
}
