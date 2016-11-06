using System;
using UnityEngine;


public delegate void AbilityCastEventHandler(object sender, AbilityCastEventArgs e);
public delegate void DeselectEventHandler(object sender, EventArgs e);

public class AbilityCastEventArgs : EventArgs
{
    public Vector2 Position;
}

public class PlayerController : MonoBehaviour
{
    public static event AbilityCastEventHandler AbilityCast;
    public static event DeselectEventHandler Deselect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Deselect != null)
            {
                Deselect.Invoke(this, null);
            }

            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            if (hit)
            {
                var charSelected = hit.transform.GetComponent<PlayerCharacter>();
                if (charSelected)
                {
                    charSelected.Select();
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (AbilityCast != null)
            {
                AbilityCast.Invoke(this, new AbilityCastEventArgs
                {
                    Position = Camera.main.ScreenToWorldPoint(Input.mousePosition)
                });
            }

        }

    }

}
