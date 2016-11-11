using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterDescription
{
    public readonly Sprite Sprite;
    public readonly string Name;
    public readonly string Description;

    public CharacterDescription(Sprite sprite, string name, string description)
    {
        Sprite = sprite;
        Name = name;
        Description = description;
    }
}

public class Menu : MonoBehaviour
{
    public Text NameText;
    public Text DescriptionText;
    public Image SpriteImage;

    private int _currentCharacter = 0;


    private List<CharacterDescription> CharacterDescriptions;
	void Start () {
        CharacterDescriptions = new List<CharacterDescription>
        {
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Poker"), "Poker (Number 1)", "Click anywhere to throw a damaging projectile towards the boss."),
            //new CharacterDescription(Resources.Load<Sprite>("Sprites/Stunner"), "Poker (Number 1)", "Click anywhere to throw a damaging projectile towards the boss"),
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Barrier"), "Barrier (Number 3)", "Click on an ally to pass the barrier to him."),
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Healer"), "Healer (Number 4)", "Click on an ally to heal him."),
            //new CharacterDescription(Resources.Load<Sprite>("Sprites/Poker"), "Poker (Number 1)", "Click anywhere to throw a damaging projectile towards the boss"),
            //new CharacterDescription(Resources.Load<Sprite>("Sprites/Taunter"), "Poker (Number 1)", "Click anywhere to throw a damaging projectile towards the boss"),
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Nuker"), "Nuker (Number 7)", "Click anywhere to throw a damaging and stunning projectile towards the boss"),
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Swapper"), "Swapper (Number 8)", "Click on a character to remove it from the game temporarily"),
            new CharacterDescription(Resources.Load<Sprite>("Sprites/Ditto"), "Ditto (Number 9)", "Click on an ally to temporarily copy his ability."),
        };
	    ChangeDisplayingCharacter();
	}

    void ChangeDisplayingCharacter()
    {
        NameText.text = CharacterDescriptions[_currentCharacter].Name;
        DescriptionText.text = CharacterDescriptions[_currentCharacter].Description;
        SpriteImage.sprite = CharacterDescriptions[_currentCharacter].Sprite;
    }

    public void NextCharacter()
    {
        _currentCharacter = (_currentCharacter + 1 + CharacterDescriptions.Count) % CharacterDescriptions.Count;
        ChangeDisplayingCharacter();
    }

    public void PrevCharacter()
    {
        _currentCharacter = (_currentCharacter - 1 + CharacterDescriptions.Count) % CharacterDescriptions.Count;
        ChangeDisplayingCharacter();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
    }
}
