using UnityEngine;
using System.Collections.Generic;

public class CharacterPositioning : MonoBehaviour
{
    [SerializeField]
    private List<Character> _availableCharacters;
    private List<Character> _instantiatedCharacters = new List<Character>();

    [SerializeField]
    private Transform _center;

    [SerializeField]
    private float _radius;

    // Use this for initialization
    void Start ()
    {
        foreach (var t in _availableCharacters)
        {
            _instantiatedCharacters.Add(Instantiate(t));
        }
        var initialAngle = Random.value * Mathf.PI * 2;
        for (int i = 0; i < _instantiatedCharacters.Count; i++)
        {
            _instantiatedCharacters[i].transform.position = (Vector2)_center.position +
                                                            new Vector2(
                                                                _radius *
                                                                Mathf.Cos(initialAngle + i * 2 * Mathf.PI / _instantiatedCharacters.Count),
                                                                _radius *
                                                                Mathf.Sin(initialAngle + i * 2 * Mathf.PI / _instantiatedCharacters.Count));
        }

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
