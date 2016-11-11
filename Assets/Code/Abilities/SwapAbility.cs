using UnityEngine;
using System.Collections;

public class SwapAbility : Ability
{
    [SerializeField]
    private Character _char1;
    [SerializeField]
    private Character _char2;

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Swap;
    }

    protected override bool RequiresTarget()
    {
        return true;
    }

    [SerializeField]
    private GameObject _banishObjectPrefab;

    protected override void Start()
    {
        base.Start();
        if (_banishObjectPrefab == null)
        {
            _banishObjectPrefab = Resources.Load<GameObject>("Prefabs/UI/BanishSprite");
        }
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        StartCoroutine(DoCastLogic(caster, e.TargetedCharacter, Ban));
    }

    IEnumerator Ban(Character caster, Character target)
    {
        var banishObject = Instantiate(_banishObjectPrefab);
        banishObject.transform.position = target.gameObject.transform.position;
        //target.gameObject.SetActive(false);
        target.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        target.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        target.gameObject.GetComponent<Character>().enabled = false;
        if (target.gameObject.GetComponent<Ability>().GetAbilityType() != AbilityType.Swap)
            target.gameObject.GetComponent<Ability>().enabled = false;
        //yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
        yield return new WaitForSeconds(Effectduration);
        //target.gameObject.SetActive(true);
        target.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        target.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        target.gameObject.GetComponent<Character>().enabled = true;
        target.gameObject.GetComponent<Ability>().enabled = true;
        Destroy(banishObject);
        yield break;
    }

    IEnumerator SwapCharacters(Character caster, Character target)
    {
        var char1pos = _char1.transform.position;
        _char1.transform.position = _char2.transform.position;
        _char2.transform.position = char1pos;
        _char1.GetComponent<SpriteRenderer>().color = Color.white;
        _char2.GetComponent<SpriteRenderer>().color = Color.white;
        _char1 = null;
        _char2 = null;
        yield break;
    }
}
