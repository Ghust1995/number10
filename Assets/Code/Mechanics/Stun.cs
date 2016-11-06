using System;
using UnityEngine;
using UnityEngine.UI;

public class Stun : MonoBehaviour {

    private Slider _stunBar;

    [SerializeField]
    private float _stunLeft;

    [SerializeField]
    private float _totalStun;

    private float _value
    {
        get
        {
            return _totalStun < 0.0001 ? 0 : Mathf.Clamp(_stunLeft/_totalStun, 0, 1);
        }
    }

    public bool IsStunned
    {
        get { return _stunLeft > 0; }
    }

    public void DoStun(float time)
    {
        _totalStun = time;
        _stunLeft = _totalStun;
    }

    void Start()
    {
        _stunBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _stunLeft -= Time.deltaTime;
        _stunBar.gameObject.SetActive(_value > 0);
        _stunBar.value = _value;
    }
}
