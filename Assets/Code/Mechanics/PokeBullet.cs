﻿using UnityEngine;
using System.Collections;

public class PokeBullet : MonoBehaviour {

    [SerializeField]
	private float _speed = 10f;

    [SerializeField]
    private float _damageDone = 10;

    private GameObject _target;

    [SerializeField]
    private float _timeToDestroy = 0.5f;

    private bool _hitTarget = false;

    private bool _initialized = false;
    private Vector3 _direction;

    private AudioClip _onHitSound;
    private AudioClip _onMissSound;

    public void Initialize(float speed, float damageDone, GameObject target)
    {
        if (_initialized)
        {
            Debug.LogError("Bullet already initialized!");
            return;
        }
        _speed = speed;
        _damageDone = damageDone;
        _target = target;
        _initialized = true;
        _direction = (_target.transform.position - transform.position).normalized;

        _onHitSound = Resources.Load<AudioClip>("SFX/poke_hit");
        _onMissSound = Resources.Load<AudioClip>("SFX/poke_miss");
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!_initialized)
        {
            Debug.LogError("Bullet not initialized!");
            return;
        }
        if (_hitTarget) return;
        float directionAngle = Mathf.Atan2(_direction.y, _direction.x);
        directionAngle *= 360 / (2 * Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, directionAngle);
        transform.localPosition += transform.right * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var barrierHit = other.gameObject.GetComponent<Barrier>();
        if (barrierHit && barrierHit.Owner.gameObject == _target)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            _hitTarget = true;
            StartCoroutine(Destroy());
            GetComponent<AudioSource>().PlayOneShot(_onMissSound);
        }
        if (other.gameObject != _target) return;
        
        _hitTarget = true;
        var targetChar = other.transform.GetComponent<Character>();
        if (targetChar)
        {
            targetChar.GetComponent<SpriteRenderer>().color = Color.gray;
            targetChar.Health.Damage(_damageDone);
            targetChar.GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<AudioSource>().PlayOneShot(_onHitSound);
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
		