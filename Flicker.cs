using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

    public Material[] materials;
	//public Texture2D[] textures;
	public float Frequency;
	int textureCounter = 0;
	public int hadbeenpressed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (hadbeenpressed == 0)
			{
				hadbeenpressed = 1;
			}
			else if(hadbeenpressed == 1)
			{
				hadbeenpressed = 0;
			}

			if (hadbeenpressed == 0)
			{
				CancelInvoke("CycleTextures");
				//GetComponent<Renderer>().material.mainTexture = textures[1];
                GetComponent<Renderer>().material = materials[1];
            }
			else if (hadbeenpressed == 1)
			{
				beginExp();
			}
		}
//
//		if (hadbeenpressed == 0 && Input.GetKeyDown (KeyCode.Alpha1)) 
//		{
//			Frequency = 12.5f;
//		}
//
//		if (hadbeenpressed == 0 && Input.GetKeyDown (KeyCode.Alpha2)) 
//		{
//			Frequency = 15f;
//		}
//
//		if (hadbeenpressed == 0 && Input.GetKeyDown (KeyCode.Alpha3)) 
//		{
//			Frequency = 25f;
//		}
	}

	void beginExp ()
	{
		float freq = (1.0f / (Frequency * 2f));
		InvokeRepeating("CycleTextures", freq, freq);
	}

	void CycleTextures()
	{
		textureCounter = ++textureCounter % materials.Length;
		GetComponent<Renderer>().material = materials[textureCounter];
	}
}