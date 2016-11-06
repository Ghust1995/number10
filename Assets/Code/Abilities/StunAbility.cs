using UnityEngine;

public class StunAbility : Ability
{
    [SerializeField]
    private StunBullet _stunBulletPrefab;

    protected override void Cast(AbilityCastEventArgs e)
    {
		Vector3 direction = e.TargetEnemy.transform.position - transform.position;
		float directionAngle = Mathf.Atan2(direction.y, direction.x);
        var bullet = Instantiate(_stunBulletPrefab);
		directionAngle *= 360 / (2 * Mathf.PI);
		bullet.Target = e.TargetEnemy;
        bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);

        Cooldown.ResetCooldown();
    }
}
