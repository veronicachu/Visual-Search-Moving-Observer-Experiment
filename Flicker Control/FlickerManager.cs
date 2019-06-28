using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlickerManager : MonoBehaviour
{
    private TrialSetup m_ExpSetup;
    private TrialManager m_ExpManager;
    public float[] Frequencies = new float[2];

    [HideInInspector] public GameObject[] redArray = new GameObject[2];
    [HideInInspector] public GameObject[] greenArray = new GameObject[2];
    [HideInInspector] public GameObject[] arrayHUD = new GameObject[4];

    // for debugging purposes
    //int RF1 = 0;
    //int RF2 = 0;
    //int GF1 = 0;
    //int GF2 = 0;

    void Start()
    {
        m_ExpSetup = this.GetComponent<TrialSetup>();
        m_ExpManager = this.GetComponent<TrialManager>();

        // Find all the HUD elements
        redArray = GameObject.FindGameObjectsWithTag("RedHUD");
        greenArray = GameObject.FindGameObjectsWithTag("GreenHUD");
        
        for(int i = 0; i < 2; i++)
        {
            arrayHUD[i] = redArray[i];
            arrayHUD[i+2] = greenArray[i];
        }
    }

    public void setFlickerFreq()
    {
        // frequency lists for red and green trials
        List<bool> changeRedFreq = m_ExpSetup.changeRedFreq;
        List<bool> changeGreenFreq = m_ExpSetup.changeGreenFreq;

        // get target color for trial
        string targColor = m_ExpManager.targetColor; 
        
        if (targColor == "RedCube")
        {
            // select one of the flicker settings at random and remove from list
            int n = Random.Range(0, changeRedFreq.Count);
            bool freqSetting = changeRedFreq[n];
            changeRedFreq.RemoveAt(n);

            if (freqSetting)
            {
                //RF1++;
                //Debug.Log("RF1 = " + RF1);
                for (int i = 0; i < redArray.Length; i++)
                {
                    redArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[0];
                    greenArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[1];
                }
            }
            else
            {
                //RF2++;
                //Debug.Log("RF2 = " + RF2);
                for (int i = 0; i < redArray.Length; i++)
                {
                    redArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[1];
                    greenArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[0];
                }
            }
        }
        else if (targColor == "GreenCube")
        {
            // select one of the flicker settings at random and remove from list
            int n = Random.Range(0, changeGreenFreq.Count);
            bool freqSetting = changeGreenFreq[n];
            changeGreenFreq.RemoveAt(n);

            if (freqSetting)
            {
                //GF1++;
                //Debug.Log("GF1 = " + GF1);
                for (int i = 0; i < redArray.Length; i++)
                {
                    redArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[0];
                    greenArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[1];
                }
            }
            else
            {
                //GF2++;
                //Debug.Log("GF2 = " + GF2);
                for (int i = 0; i < redArray.Length; i++)
                {
                    redArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[1];
                    greenArray[i].GetComponent<FlickerControl>().Frequency = Frequencies[0];
                }
            }
        }
        
    }

    public void startAllFlicker()
    {
        for(int i = 0; i < arrayHUD.Length; i++)
        {
            arrayHUD[i].GetComponent<FlickerControl>().beginFlicker();
        }
    }

    public void stopAllFlicker()
    {
        for (int i = 0; i < arrayHUD.Length; i++)
        {
            arrayHUD[i].GetComponent<FlickerControl>().stopFlicker();
        }
    }

}
