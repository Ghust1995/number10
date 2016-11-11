using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Character : MonoBehaviour
{
    protected Characters Constants;

    [SerializeField]
    protected CharactersData Data;

    public Dictionary<AbilityType, Func<Ability>> AbilityBuilder;

    protected Ability Ability;
    public Health Health;
    public Stun Stun;

    public bool IsCasting;

    public bool CanCastAbility
    {
        get
        {
            return (this.Ability != null)
                   && !Stun.IsStunned
                   && !Ability.Cooldown.OnCooldown
                   && !IsCasting;

        }
    }
    
    public void Buff(float value)
    {
        // TODO: Make this work on the boss
        Ability.IncreasePower(value);
    }

    public void EndBuff()
    {
        Ability.ResetPowerIncrease();
    }

    public Character TauntingTarget { get; private set; }

    protected void ResetData()
    {
        Constants = Resources.Load<Characters>("Characters");
        for (int i = 0; i < Constants.dataArray.Length; i++)
        {
            // Horrivel
            if (Constants.dataArray[i].CHARACTERTYPE != GetCharacterType()) continue;
            Data = Constants.dataArray[i];
            break;
        }
    }

    public abstract CharacterType GetCharacterType();

    private T GetAbility<T> () where T : Ability
    {
        var ability = GetComponent<T>();
        if (ability != null) return ability;
        return gameObject.AddComponent<T>();
    }

    public virtual void Start()
    {
        ResetData();
        Health = GetComponentInChildren<Health>();
        if (Health == null)
        {
            var _healthPrefab = Resources.Load<Health>("Prefabs/Mechanics/Health");
            Health = Instantiate(_healthPrefab, transform, false) as Health;
        }

        Stun = GetComponentInChildren<Stun>();
        if (Stun == null)
        {
            var _stunPrefab = Resources.Load<Stun>("Prefabs/Mechanics/Stun");
            Stun = Instantiate(_stunPrefab, transform, false) as Stun;
        }

        AbilityBuilder = new Dictionary<AbilityType, Func<Ability>>()
        {
            {AbilityType.Poke, GetAbility<PokeAbility>},
            {AbilityType.Stun, GetAbility<StunAbility>},
            {AbilityType.Nuke, GetAbility<NukeAbility>},
            {AbilityType.Heal, GetAbility<HealAbility>},
            {AbilityType.Swap, GetAbility<SwapAbility>},
            {AbilityType.Barr, GetAbility<BarrAbility>},
            {AbilityType.Taunt, GetAbility<TauntAbility>},
            {AbilityType.Buff, GetAbility<BuffAbility>},
            {AbilityType.Copy, GetAbility<CopyAbility>},
            
        };
        
        Health.Initialize(Data.Health);
        Stun.DoStun(Data.Startstun);

        var uiCircleColliderObject = new GameObject {layer = LayerMask.NameToLayer("UI")};
        uiCircleColliderObject.transform.position = transform.position;
        uiCircleColliderObject.name = "Selectable" + gameObject.name;
        gameObject.transform.parent = uiCircleColliderObject.transform;
        var uiCircleCollider = uiCircleColliderObject.AddComponent<CircleCollider2D>();
        uiCircleCollider.isTrigger = true;
        uiCircleCollider.radius = GetComponent<CircleCollider2D>().radius;

        


        // The collider is trigger
        GetComponent<CircleCollider2D>().isTrigger = true;
        // Make the collider more friendly
        GetComponent<CircleCollider2D>().radius *= 0.0f;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        // Register to taunt events
        TauntEventHandler tauntCb = (sender, args) => { TauntingTarget = args.TauntingCharacter; };
        TauntOverEventHandler tauntOverCb = (sender, args) => { TauntingTarget = null; };
        TauntAbility.TauntEvent += tauntCb;
        TauntAbility.TauntOverEvent += tauntOverCb;
        OnDestroyCallbacks += () =>
        {
            TauntAbility.TauntEvent -= tauntCb;
            TauntAbility.TauntOverEvent -= tauntOverCb;
        };
    }

    public virtual void Update()
    {
        transform.localScale = new Vector3(Ability.PowerMultiplier, Ability.PowerMultiplier, 1.0f);

        if (TauntingTarget == this)
        {
            transform.position = transform.position + 0.03f*new Vector3(Mathf.Sin(Time.time*15.0f), 0.0f, 0.0f);
        }

        if (Health.Value <= 0.0)
        {
            Destroy(gameObject);
        }
    }

    public delegate void OnDestroyCallback();
    public event OnDestroyCallback OnDestroyCallbacks;
    public void OnDestroy()
    {
        OnDestroyCallbacks.Invoke();
    }

    protected virtual void CastAbility(object sender, AbilityCastEventArgs e)
    {
        if(Ability == null)
        {
            Debug.Log("No ability selected");
            return;
        }
        if (Stun.IsStunned) return;
        if (IsCasting) return;
        Ability.TryCast(this, e);
    }
}
