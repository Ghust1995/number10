using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Transform _center;

    [SerializeField]
    private float _radius = 2;

    [SerializeField]
    private float _speed = 3;

    private float _startingAngle;
    private bool _initialized;

    public void Initialize(float speed)
    {
        if (_initialized)
        {
            Debug.LogError("Barrier already initialized!");
            return;
        }
        _speed = speed;
        _initialized = true;
    }

    public void SetCenter(Transform center)
    {
        _center = center;
    }

    public void Start()
    {
        _startingAngle = Random.value * Mathf.PI;
    }
	
	// Update is called once per frame
	void Update () {
        if (!_initialized)
        {
            Debug.LogError("Barrier not initialized!");
            return;
        }

        transform.position = new Vector2(_center.position.x + _radius*Mathf.Cos(_startingAngle + _speed * Time.time), _center.position.y + _radius * Mathf.Sin(_startingAngle + _speed * Time.time));
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (_startingAngle + _speed * Time.time)*180.0f/Mathf.PI);
    }
}
