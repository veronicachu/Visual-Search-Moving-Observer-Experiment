using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class FlickerTest : MonoBehaviour {

    // Setup variables for controlling flicker color and frequency
    public float Frequency;
    public Texture[] textures = new Texture[2];
    int textureCounter = 0;
    RawImage img;

    // Use this for initialization
    void Start()
    {
        img = this.GetComponent<RawImage>();

        float freq = (1.0f / (Frequency * 2f));
        InvokeRepeating("CycleColors", freq, freq);
    }

    // This controls cycling between the two colors
    void CycleColors()
    {
        textureCounter = ++textureCounter % textures.Length;
        img.texture = textures[textureCounter];
    }
}
