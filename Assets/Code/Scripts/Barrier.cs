using UnityEngine;

public class Barrier : MonoBehaviour
{

    [SerializeField]
    private Transform _center;

    [SerializeField]
    private float _radius = 2;

    [SerializeField]
    private float _velocity = 3;

    public void SetCenter(Transform center)
    {
        _center = center;
    }
	
	// Update is called once per frame
	void Update () {
	    transform.position = new Vector2(_center.position.x + _radius*Mathf.Cos(_velocity*Time.time), _center.position.y + _radius * Mathf.Sin(_velocity * Time.time));
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (_velocity * Time.time)*180.0f/Mathf.PI);
    }
}
