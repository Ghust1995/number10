using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class CharactersData
{
  [SerializeField]
  CharacterType charactertype;
  public CharacterType CHARACTERTYPE { get {return charactertype; } set { charactertype = value;} }
  
  [SerializeField]
  float health;
  public float Health { get {return health; } set { health = value;} }
  
  [SerializeField]
  float startstun;
  public float Startstun { get {return startstun; } set { startstun = value;} }
  
}