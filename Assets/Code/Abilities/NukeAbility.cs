using System.Collections;
using UnityEngine;

public class NukeAbility : Ability
{
    [SerializeField]
    private GameObject _nukeObjectPrefab;

    [SerializeField]
    private GameObject _nukeTargetingPrefab;

    private GameObject _nukeTargeting;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Nuke;
    }

    protected override bool RequiresTarget()
    {
        return false;
    }

    private AudioClip _nukeWarning;

    protected override void Start()
    {
        base.Start();
        if (_nukeObjectPrefab == null)
        {
            _nukeObjectPrefab = Resources.Load<GameObject>("Prefabs/UI/NukeSprite");
        }
        if (_nukeTargetingPrefab == null)
        {
            _nukeTargetingPrefab = Resources.Load<GameObject>("Prefabs/UI/NukeTarget");
        }
        _nukeWarning = Resources.Load<AudioClip>("SFX/nuke_alert");
    }

    private void OnTaunt(object sender, TauntEventArgs e)
    {
        Vector3 direction = e.TauntingCharacter.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * 360 / (2 * Mathf.PI);
        _nukeTargeting.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
    }

    void OnStun()
    {
        Destroy(_nukeTargeting);
        GetComponent<AudioSource>().Stop();
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        TauntAbility.TauntEvent += OnTaunt;
        var target = e.TargetEnemy;
        if (caster.TauntingTarget != null && caster.GetType() != caster.TauntingTarget.GetType()) target = caster.TauntingTarget;


        GetComponent<SpriteRenderer>().color += Color.cyan;
        GetComponent<SpriteRenderer>().color /= 2;
        _nukeTargeting = Instantiate(_nukeTargetingPrefab, transform) as GameObject;
        _nukeTargeting.transform.localPosition = Vector3.zero;
        _nukeTargeting.transform.localScale = Vector3.one;
        Vector3 direction = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * 360 / (2 * Mathf.PI);
        _nukeTargeting.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
        
        caster.Stun.OnStun += OnStun;

        GetComponent<AudioSource>().PlayOneShot(_nukeWarning);
        StartCoroutine(DoCastLogic(caster, e.TargetEnemy.GetComponent<Character>(), Nuke, () =>
        {
            caster.Stun.OnStun -= OnStun;
            TauntAbility.TauntEvent -= OnTaunt;
        }));
    }

    private IEnumerator Nuke(Character caster, Character target)
    {
        caster.Stun.OnStun -= OnStun;
        TauntAbility.TauntEvent -= OnTaunt;
        GetComponent<SpriteRenderer>().color *= 2;
        GetComponent<SpriteRenderer>().color -= Color.cyan;
        var nukeObject = Instantiate(_nukeObjectPrefab, transform) as GameObject;
        nukeObject.transform.localPosition = Vector3.zero;
        nukeObject.transform.localScale = Vector3.one;
        //nukeObject.transform.localScale.Scale(transform.localScale);
        Vector3 direction = target.transform.position - transform.position;
        float directionAngle = Mathf.Atan2(direction.y, direction.x)*360/(2*Mathf.PI);
        nukeObject.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
        //nukeObject.transform.position = transform.position + direction/2;
        for (int i = 0; i < Data.Ticks; i++)
        {
            target.Health.Damage(Power);
            yield return new WaitForSeconds(Data.Effectduration/Data.Ticks);
            if (caster.Stun.IsStunned) break;
        }
        Destroy(nukeObject);
        Destroy(_nukeTargeting);
    }
}
