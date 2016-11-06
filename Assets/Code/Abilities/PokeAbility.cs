using UnityEngine;

public class PokeAbility : Ability
{
    [SerializeField]
    private PokeBullet _pokeBulletPrefab;

    protected override void Cast(AbilityCastEventArgs e)
    {
		Vector3 direction = e.TargetEnemy.transform.position - transform.position;
		float directionAngle = Mathf.Atan2(direction.y, direction.x);
		Debug.Log (directionAngle);
        var bullet = Instantiate(_pokeBulletPrefab);
		directionAngle *= 360 / (2 * Mathf.PI);
		bullet.Target = e.TargetEnemy;
        bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);

        Cooldown.ResetCooldown();
    }
}
