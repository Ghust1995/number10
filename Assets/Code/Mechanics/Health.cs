using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Slider _healthBar;

    [SerializeField]
    private float _maxHealth = 100;

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public float Value { get; private set; }
    
    public void Damage(float damageDealt)
    {
        Value = Mathf.Clamp(Value - damageDealt, 0, _maxHealth);
    }
    
    public void Heal(float healingDone)
    {
        Value = Mathf.Clamp(Value + healingDone, 0, _maxHealth);
    }

    // Use this for initialization
    void Start ()
	{
	    _healthBar = GetComponentInChildren<Slider>();
	    Value = _maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _healthBar.value = Value/_maxHealth;
	}
}
