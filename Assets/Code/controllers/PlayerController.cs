using System;
using UnityEngine;



public delegate void DeselectEventHandler(object sender, EventArgs e);

public delegate void AbilityCastEventHandler(object sender, AbilityCastEventArgs e);
public class AbilityCastEventArgs : EventArgs
{
    //public Vector2 Position;
    public Character TargetEnemy;
    public Character TargetedCharacter;
}

public class PlayerController : MonoBehaviour
{
    public static event AbilityCastEventHandler AbilityCastEvent;
    public static event DeselectEventHandler DeselectEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (DeselectEvent != null)
            {
                DeselectEvent.Invoke(this, null);
            }

            var rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            var hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            if (hit)
            {
                var charSelected = hit.transform.GetComponentInChildren<PlayerCharacter>();
                if (charSelected)
                {
                    charSelected.Select();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            var rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            var targetedCharacter = !hit ? null : hit.transform.GetComponentInChildren<Character>();
            if (AbilityCastEvent != null)
            {
                AbilityCastEvent.Invoke(this, new AbilityCastEventArgs
                {
                    //Position = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    TargetEnemy = FindObjectOfType<BossCharacter>(),
                    TargetedCharacter = targetedCharacter
                });
            }
        }
    }
}
