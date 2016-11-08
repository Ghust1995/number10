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

    [SerializeField]
    private GameObject _banishObjectPrefab;

    protected override void Start()
    {
        base.Start();
        if (_banishObjectPrefab == null)
        {
            _banishObjectPrefab = Resources.Load<GameObject>("Prefabs/BanishSprite");
        }
    }

    protected override void Cast(Character caster, AbilityCastEventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(e.Position, Vector2.zero, 0f);
        if (hit)
        {
            var charSelected = hit.transform.GetComponent<Character>();
            if (charSelected)
            {
                StartCoroutine(DoCastLogic(caster, charSelected, Ban));
                //charSelected.GetComponent<SpriteRenderer>().color = Color.cyan;
                //if (_char1 == null)
                //{
                //    _char1 = charSelected;
                //}
                //else if (_char2 == null)
                //{
                //    _char2 = charSelected;
                //    // Swap characters after some time
                //    StartCoroutine(DoCastLogic(caster, null, SwapCharacters));
                //}
            }
        }
    }

    IEnumerator Ban(Character caster, Character target)
    {
        var banishObject = Instantiate(_banishObjectPrefab);
        banishObject.transform.position = target.gameObject.transform.position;
        target.gameObject.SetActive(false);
        //yield return new WaitForSeconds(Data.Effectduration / Data.Ticks);
        yield return new WaitForSeconds(Data.Effectduration);
        target.gameObject.SetActive(true);
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
