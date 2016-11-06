using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField]
    public Cooldown Cooldown { get; private set; }

    protected virtual void Start()
    {
        Cooldown = GetComponentInChildren<Cooldown>();
    }

    public void Cast(object sender, AbilityCastEventArgs e)
    {
        if (Cooldown.OnCooldown) return;
        Cast(e);
    }

    protected abstract void Cast(AbilityCastEventArgs e);
}

