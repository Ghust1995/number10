using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class AbilitiesData
{
  [SerializeField]
  AbilityType abilitytype;
  public AbilityType ABILITYTYPE { get {return abilitytype; } set { abilitytype = value;} }
  
  [SerializeField]
  float casttime;
  public float Casttime { get {return casttime; } set { casttime = value;} }
  
  [SerializeField]
  float cooldown;
  public float Cooldown { get {return cooldown; } set { cooldown = value;} }
  
  [SerializeField]
  float power;
  public float Power { get {return power; } set { power = value;} }
  
  [SerializeField]
  float effectduration;
  public float Effectduration { get {return effectduration; } set { effectduration = value;} }
  
  [SerializeField]
  int ticks;
  public int Ticks { get {return ticks; } set { ticks = value;} }
  
  [SerializeField]
  float objectspeed;
  public float Objectspeed { get {return objectspeed; } set { objectspeed = value;} }
  
}