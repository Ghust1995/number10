using UnityEngine;
using System.Collections;

public class PokeBullet : MonoBehaviour {

	[SerializeField]
	private float _pokeVelocity = 10f;

	[SerializeField]
	private float _damageDone = 10;

	[SerializeField]
	private float _timeToDamage = 1;

    [SerializeField]
    private float _timeToDestroy = 0.5f;

    private bool _hitTarget = false;
	
	// Update is called once per frame
	void Update ()
	{
	    if (_hitTarget) return;
		transform.localPosition += transform.right * _pokeVelocity * Time.deltaTime; //Bullet going in boss direction (your own right direction)
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
		targetChar.GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }



    public GameObject Target { set; private get; }
}
		