using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected Abilities Constants;

    [SerializeField]
    protected AbilitiesData Data;

    [ExecuteInEditMode]
    protected abstract AbilityType GetAbilityType();

    [SerializeField]
    private Cooldown _cooldownPrefab;

    public Cooldown Cooldown { get; private set; }

    protected virtual void Reset()
    {
        ResetData();
    }

    protected void ResetData()
    {
        Constants = AssetDatabase.LoadAssetAtPath<Abilities>("Assets/Resources/Abilities.asset");
        for (int i = 0; i < Constants.dataArray.Length; i++)
        {
            if (Constants.dataArray[i].ABILITYTYPE != GetAbilityType()) continue;
            Data = Constants.dataArray[i];
            break;
        }
    }

    protected virtual void Start()
    {
        ResetData();
        Cooldown = Instantiate(_cooldownPrefab, transform, false) as Cooldown;
        Cooldown.Initialize(Data.Cooldown);
    }

    public void TryCast(Character caster, AbilityCastEventArgs e)
    {
        if (Cooldown.OnCooldown) return;
        Cast(caster, e);
    }

    protected abstract void Cast(Character caster, AbilityCastEventArgs e);

    protected IEnumerator DoCastLogic(Character caster, Character target, Func<Character, Character, IEnumerator> castLogic)
    {
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        if (caster.Stun.IsStunned) yield break;
        caster.IsCasting = true;
        yield return castLogic(caster, target);
        caster.IsCasting = false;
    }

}

