using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField]
    protected Abilities Constants;

    [SerializeField]
    protected AbilitiesData Data;

    [ExecuteInEditMode]
    protected abstract AbilityType GetAbilityType();

    [SerializeField]
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
        Cooldown = GetComponentInChildren<Cooldown>();
    }

    public void Cast(object sender, AbilityCastEventArgs e)
    {
        if (Cooldown.OnCooldown) return;
        Cast(e);
    }

    protected abstract void Cast(AbilityCastEventArgs e);
}

