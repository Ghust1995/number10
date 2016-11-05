using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Slider _healthBar;

    [SerializeField]
    private float _maxHealth = 100;

    [SerializeField]
    private float _health;
    
    public void Damage(float damageDealt)
    {
        _health = Mathf.Clamp(_health - damageDealt, 0, _maxHealth);
    }
    
    public void Heal(float healingDone)
    {
        _health = Mathf.Clamp(_health + healingDone, 0, _maxHealth);
    }

    // Use this for initialization
    void Start ()
	{
	    _healthBar = GetComponentInChildren<Slider>();
	    _health = _maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _healthBar.value = _health/_maxHealth;
	}
}
