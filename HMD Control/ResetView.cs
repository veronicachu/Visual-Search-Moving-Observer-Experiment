using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ResetView : MonoBehaviour {

    void Awake()
    {
    }

	// Update is called once per frame
	void Update ()
    {
        // Resets the orientation of the Rift
        if (Input.GetKeyDown("r") || Input.GetKeyDown("joystick button 5"))
        {
            InputTracking.Recenter();
        }
    }
}
