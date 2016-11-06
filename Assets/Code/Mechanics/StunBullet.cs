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
	private float _timeToDamage = 0.2f;

    [SerializeField]
    private float _timeToDestroy = 0.5f;

    public GameObject Target { set; private get; }

    private bool _hitTarget = false;
	
	// Update is called once per frame
	void Update ()
	{
	    if (_hitTarget) return;
		transform.localPosition += transform.right * _speed * Time.deltaTime; //Bullet going in boss direction (your own right direction)
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        var barrierHit = other.gameObject.GetComponent<Barrier>();
        if (barrierHit && barrierHit.transform.parent.gameObject == Target)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            _hitTarget = true;
            StartCoroutine(Destroy());
        }
        if (other.gameObject != Target) return;

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _hitTarget = true;
        var targetChar = other.transform.GetComponent<Character>();
        if (targetChar)
        {
            targetChar.GetComponent<SpriteRenderer>().color = Color.gray;
            StartCoroutine(DoDamage(targetChar));
        }
    }

    IEnumerator DoDamage(Character targetChar)
	{
		yield return new WaitForSeconds (_timeToDamage);
		targetChar.Health.Damage(_damageDone);
        targetChar.Stun.DoStun(_stunTime);
		targetChar.GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
		