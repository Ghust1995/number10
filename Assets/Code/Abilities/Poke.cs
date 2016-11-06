using UnityEngine;

public class Poke : Ability
{
    [SerializeField]
    private PokeBullet _pokeBulletPrefab;

    protected override void Cast(AbilityCastEventArgs e)
    {
		Vector3 direction = FindObjectOfType<BossCharacter>().transform.position - transform.position;
        float directionAngle = Vector3.Angle(direction, new Vector3(1, 0, 0));

        var bullet = Instantiate(_pokeBulletPrefab);
        bullet.Target = e.TargetEnemy;
        bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.Euler(0,0,directionAngle);

        Cooldown.ResetCooldown();
    }
}
