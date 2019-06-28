using UnityEngine;
using System.Collections.Generic;

public class TrialSetup : MonoBehaviour
{
    public int totalTrials;
    public int trialsPerCue;
    public bool loadComplete = false;
    public List<GameObject> cueTypes = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> targetTrials = new List<GameObject>();
    //[HideInInspector]
    public List<bool> changeRedFreq = new List<bool>();
    [HideInInspector]
    public List<bool> changeGreenFreq = new List<bool>();

    public void Awake()
    {
        trialsPerCue = totalTrials / cueTypes.Count;

        // this loop creates the target for trials
        int numTrials = cueTypes.Count;
        for (int j = 0; j < trialsPerCue; j++)
        {
            for (int i = 0; i < numTrials; i++)
            {
                targetTrials.Add(cueTypes[i]);
            }
        }

        // this loop creates the flicker change for trials
        for (int i = 0; i < totalTrials / 4; i++)
        {
            changeRedFreq.Add(true);
            changeRedFreq.Add(false);

            changeGreenFreq.Add(true);
            changeGreenFreq.Add(false);
        }

        loadComplete = true;

    }
}


