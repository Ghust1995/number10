using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BossCharacter))]
public class BossController : MonoBehaviour
{
    public static event AbilityCastEventHandler AbilityCast;

    private BossCharacter _boss;

    private List<GameObject> _targetCharacters;

    void Start()
    {
        _boss = GetComponent<BossCharacter>();
    }

    // Update is called once per frame
    private int _iteration;
    void Update()
    {
        // Update available characters, could be done with some death event
        _targetCharacters = FindObjectOfType<CharacterPositioning>().InstantiatedCharacters.Select(c => c.gameObject).ToList();

        //Choose which skill to use
        _boss.SetAbility(Random.value > 0.25 ? AbilityType.Poke : AbilityType.Stun);
        if (AbilityCast != null && _boss.CanCastAbility && _targetCharacters.Count > 0)
        {
            var targetEnemy = _targetCharacters[(++_iteration) % _targetCharacters.Count];
            AbilityCast.Invoke(this, new AbilityCastEventArgs
            {
                Position = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                TargetEnemy = targetEnemy
            });
        }

    }

}
