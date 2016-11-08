using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BossCharacter))]
public class BossController : MonoBehaviour
{
    public static event AbilityCastEventHandler AbilityCast;

    private BossCharacter _boss;

    private List<Character> _targetCharacters;

    void Start()
    {
        _boss = GetComponent<BossCharacter>();
    }

    // Update is called once per frame
    private int _iteration;
    void Update()
    {
        // Update available characters, could be done with some death event
        _targetCharacters = FindObjectOfType<CharacterPositioning>().InstantiatedCharacters;

        //Choose which skill to use
        AbilityType chosenAbility;
        if (Random.value < 0.10)
        {
            chosenAbility = AbilityType.Nuke;
        }
        else if (Random.value < 0.30)
        {
            chosenAbility = AbilityType.Stun;
        }
        else
        {
            chosenAbility = AbilityType.Poke;
        }
        _boss.SetAbility(chosenAbility);
        if (AbilityCast != null && _boss.CanCastAbility && _targetCharacters.Count > 0)
        {
            var targetEnemy = _targetCharacters[(++_iteration) % _targetCharacters.Count];
            AbilityCast.Invoke(this, new AbilityCastEventArgs
            {
                //Position = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                TargetEnemy = targetEnemy
            });
        }

    }

}
