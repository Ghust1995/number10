using UnityEngine;
using System.Collections;

public class Ball1Controller : MonoBehaviour {
	public GameObject bulletPrefab;
	public bool isSelected = false;
	float waitTime = 1f;
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (isSelected) {
			if(Input.GetMouseButtonDown(0)){
				if (waitTime <= 0) {
					Debug.Log ("entrou");
					GameObject boss;
					GameObject poke;
					boss = GameObject.FindGameObjectWithTag("Boss");
					Vector3 direcao;
					direcao = new Vector3();
					direcao = boss.transform.position;
					direcao -= this.transform.position;

					poke = Instantiate (bulletPrefab);
					//poke.transform.position += direcao;
					float angulo;
					angulo = Vector3.Angle(direcao,new Vector3(1,0,0));
					poke.transform.rotation = Quaternion.Euler(0,0,angulo);
					waitTime = 1f;
				}
			}
		}
	}
}
