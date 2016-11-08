using UnityEngine;
using System.Collections;

public class StunBullet : MonoBehaviour {

	[SerializeField]
	private float _speed = 10f;

	[SerializeField]
	private float _damageDone = 10;

    [SerializeField]
    private float _stunTime = 3;

    [SerializeField]
    private float _timeToDestroy = 0.5f;

    private bool _hitTarget = false;
    private bool _initialized;
    private GameObject _target;
    private Vector3 _direction;

    public void Initialize(float speed, float damageDone, float stunTime, GameObject target)
    {
        if (_initialized)
        {
            Debug.LogError("Bullet already initialized!");
            return;
        }
        _speed = speed;
        _damageDone = damageDone;
        _target = target;
        _stunTime = stunTime;
        _initialized = true;
        _direction = (_target.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
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
        if (barrierHit && barrierHit.transform.parent.gameObject == _target)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            _hitTarget = true;
            StartCoroutine(Destroy());
        }
        if (other.gameObject != _target) return;

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _hitTarget = true;
        var targetChar = other.transform.GetComponent<Character>();
        if (targetChar)
        {
            targetChar.GetComponent<SpriteRenderer>().color = Color.gray;
            targetChar.Health.Damage(_damageDone);
            targetChar.Stun.DoStun(_stunTime);
            targetChar.GetComponent<SpriteRenderer>().color = Color.white;
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
		