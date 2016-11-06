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
        _targetCharacters = FindObjectsOfType<PlayerCharacter>().Select(c=> c.gameObject).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Random.value > 0.01) return;
        if (AbilityCast != null)
        {
            var targetEnemy = _targetCharacters[Random.Range(0, _targetCharacters.Count)];
            AbilityCast.Invoke(this, new AbilityCastEventArgs
            {
                Position = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                TargetEnemy = targetEnemy
            });
        }

    }

}
