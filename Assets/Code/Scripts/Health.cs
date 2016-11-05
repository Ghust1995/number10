using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Slider healthBar;

    public float MaxHealth = 100;

    [SerializeField]
    private float _health;
    
    public void Damage(float damageDealt)
    {
        _health = Mathf.Clamp(_health - damageDealt, 0, MaxHealth);
    }
    
    public void Heal(float healingDone)
    {
        _health = Mathf.Clamp(_health + healingDone, 0, MaxHealth);
    }

    // Use this for initialization
    void Start ()
	{
	    healthBar = GetComponentInChildren<Slider>();
	    _health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    healthBar.value = _health/MaxHealth;
	}
}
