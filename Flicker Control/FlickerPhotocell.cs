using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlickerPhotocell : MonoBehaviour {

    // Setup variables for controlling flicker color and frequency
    public float Frequency;
    public Texture[] textures = new Texture[2];
    int textureCounter = 0;
    RawImage img;

    // Use this for initialization
    void Start()
    {
        img = this.GetComponent<RawImage>();
    }

    // This method starts the flicker at the given frequency rate
    public void beginFlicker()
    {
        float freq = (1.0f / (Frequency * 2f));
        InvokeRepeating("CycleColors", freq, freq);
    }

    // This method stops the flicker
    public void stopFlicker()
    {
        CancelInvoke("CycleColors");
        img.texture = textures[0];
    }

    // This controls cycling between the two colors
    void CycleColors()
    {
        textureCounter = ++textureCounter % textures.Length;
        img.texture = textures[textureCounter];
    }
}
