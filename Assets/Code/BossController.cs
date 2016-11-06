using UnityEngine;


public class BossController : MonoBehaviour
{
    public static event AbilityCastEventHandler AbilityCast;

    // Update is called once per frame
    void Update()
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
