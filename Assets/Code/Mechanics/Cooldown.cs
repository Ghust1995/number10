using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    private Slider _cooldownBar;

    [SerializeField]
    private float _cooldown = 5;

    [SerializeField]
    private float _time = 0;

    public bool OnCooldown
    {
        get { return _time > 0; }
    }

    public void ResetCooldown()
    {
        _time = _cooldown;
    }

    void Start()
    {
        _cooldownBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
        _cooldownBar.value = _time / _cooldown;
    }
}
