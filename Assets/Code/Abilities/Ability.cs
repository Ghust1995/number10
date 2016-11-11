using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Ability : MonoBehaviour
{
    protected Abilities Constants;

    [SerializeField]
    protected AbilitiesData Data;

    protected float Power
    {
        get { return Data.Power * _powerMultiplier; }
    }

    protected float Effectduration
    {
        get { return Data.Effectduration * _powerMultiplier; }
    }

    protected abstract bool RequiresTarget();
    
    private float _powerMultiplier = 1;
    public float PowerMultiplier { get {return Mathf.Max(_powerMultiplier, 1.0f);} private set { _powerMultiplier = value; } }

    public virtual void IncreasePower(float value)
    {
        PowerMultiplier *= value;
    }

    public virtual void ResetPowerIncrease()
    {
        PowerMultiplier = 0;
    }

    [ExecuteInEditMode]
    public abstract AbilityType GetAbilityType();

    [SerializeField]
    private Cooldown _cooldownPrefab;

    public Cooldown Cooldown { get; private set; }

    protected virtual void Reset()
    {
        ResetData();
    }

    protected void ResetData()
    {
        Constants = Resources.Load<Abilities>("Abilities");
        for (int i = 0; i < Constants.dataArray.Length; i++)
        {
            if (Constants.dataArray[i].ABILITYTYPE != GetAbilityType()) continue;
            Data = Constants.dataArray[i];
            break;
        }
    }

    private static readonly Dictionary<AbilityType, string> AbilityCastSound = new Dictionary<AbilityType, string>
        {
            {AbilityType.Poke, "SFX/poke"},
            {AbilityType.Stun, "SFX/poke"},
            {AbilityType.Nuke, "SFX/nuke_strike"},
            {AbilityType.Heal, "SFX/heal"},
            {AbilityType.Swap, "SFX/swap"},
            {AbilityType.Barr, "SFX/barrier"},
            {AbilityType.Taunt, "SFX/taunt"},
            {AbilityType.Buff, "SFX/death_08"},
            {AbilityType.Copy, "SFX/ditto"},
        };

    private AudioSource _audioPlayer;
    private AudioClip _audioClip;

    protected virtual void Start()
    {
        ResetData();

        _audioClip = Resources.Load<AudioClip>(AbilityCastSound[GetAbilityType()]);
        _audioPlayer = GetComponent<AudioSource>();

        if (_cooldownPrefab == null)
        {
            _cooldownPrefab = Resources.Load<Cooldown>("Prefabs/Mechanics/Cooldown");
        }
        Cooldown = Instantiate(_cooldownPrefab, transform, false) as Cooldown;
        Cooldown.Initialize(Data.Cooldown);
    }

    public void TryCast(Character caster, AbilityCastEventArgs e)
    {
        if (Cooldown.OnCooldown) return;
        if (RequiresTarget() && e.TargetedCharacter == null) return;
        Cast(caster, e);
    }

    protected abstract void Cast(Character caster, AbilityCastEventArgs e);

    protected IEnumerator DoCastLogic(Character caster, Character target, Func<Character, Character, IEnumerator> castLogic, Action onCastEnded = null)
    {
        if (caster.TauntingTarget != null && caster.GetType() != caster.TauntingTarget.GetType()) target = caster.TauntingTarget;
        Cooldown.ResetCooldown();
        yield return new WaitForSeconds(Data.Casttime);
        if (caster.Stun.IsStunned)
        {
            if (onCastEnded != null) onCastEnded();
            yield break;
        }
        if (target != null)
        {
            caster.IsCasting = true;
            _audioPlayer.PlayOneShot(_audioClip);
            yield return castLogic(caster, target);
            caster.IsCasting = false;
        }
        if (onCastEnded != null) onCastEnded();
    }
}

