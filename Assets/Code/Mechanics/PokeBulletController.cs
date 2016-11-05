using UnityEngine;
using System.Collections;

public class PokeBulletController : MonoBehaviour {

	[SerializeField]
	private float _pokeVelocity = 10f;

	[SerializeField]
	private float _damageDone = 10;

	[SerializeField]
	private float _timeToDamage = 1;

	private bool _crashed;

	// Use this for initialization
	void Awake () {
		_crashed = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition += transform.right * _pokeVelocity * Time.deltaTime; //Bullet going in boss direction (your own right direction)
		if (_crashed) { 
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		Debug.Log ("Collidiu com algo");
		if (other.name == "Poke")
			return;
		if (other.gameObject.tag == "Boss") {
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			var targetChar = other.transform.GetComponent<Character> ();

			if (targetChar) {
				targetChar.GetComponent<SpriteRenderer> ().color = Color.magenta;
				StartCoroutine (DoDamage (targetChar));
			}
					
		} else {
			_crashed = true;
		}


	}

	IEnumerator DoDamage(Character targetChar)
	{
		yield return new WaitForSeconds (_timeToDamage);
		targetChar.Health.Damage(_damageDone);
		targetChar.GetComponent<SpriteRenderer>().color = Color.white;
		_crashed = true;
	}
}
		