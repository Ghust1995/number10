using System.Collections;
using UnityEngine;

public class NukeAbility : Ability
{
    [SerializeField]
    private GameObject _nukeObjectPrefab;

    protected override AbilityType GetAbilityType()
    {
        return AbilityType.Nuke;
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        StartCoroutine(DoCastLogic(caster, e.TargetEnemy.GetComponent<Character>(), Nuke));
        Cooldown.ResetCooldown();
    }

    private IEnumerator Nuke(Character caster, Character target)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        var nukeObject = Instantiate(_nukeObjectPrefab);
        Vector3 direction = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * 360 / (2 * Mathf.PI);
        nukeObject.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
        nukeObject.transform.position = transform.position + direction/2;
        for (int i = 0; i < Data.Ticks; i++)
        {
            target.Health.Damage(Data.Power);
            yield return new WaitForSeconds(Data.Effectduration/Data.Ticks);
            if (caster.Stun.IsStunned) break;
        }
        Destroy(nukeObject);
    }
}
