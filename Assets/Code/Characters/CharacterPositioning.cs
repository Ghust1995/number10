using UnityEngine;
using System.Collections.Generic;

public class CharacterPositioning : MonoBehaviour
{
    [SerializeField]
    private List<Character> _availableCharacters;
    public List<Character> InstantiatedCharacters = new List<Character>();

    [SerializeField]
    private Transform _center;

    [SerializeField]
    private float _radius;

    public float InitialAngle = 0 * Mathf.PI * 2;

    // Use this for initialization
    void Awake ()
    {
        foreach (var t in _availableCharacters)
        {
            var goChar = Instantiate(t);
            InstantiatedCharacters.Add(goChar);
            goChar.OnDestroyCallbacks += () => { InstantiatedCharacters.Remove(goChar); };
        }
        
        for (int i = 0; i < InstantiatedCharacters.Count; i++)
        {
            InstantiatedCharacters[i].transform.position = (Vector2)_center.position +
                                                            new Vector2(
                                                                _radius *
                                                                Mathf.Cos(InitialAngle + i * 2 * Mathf.PI / InstantiatedCharacters.Count),
                                                                _radius *
                                                                Mathf.Sin(InitialAngle + i * 2 * Mathf.PI / InstantiatedCharacters.Count));
        }

    }
}
