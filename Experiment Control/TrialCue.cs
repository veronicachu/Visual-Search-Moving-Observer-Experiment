using UnityEngine;
using System.Collections.Generic;

public class TrialCue : MonoBehaviour
{
    //[HideInInspector]
    public GameObject activeTarget;
    [HideInInspector]
    public GameObject cueClone;
    private TrialSetup trialSetupRef;
    private Transform fixationLoc;

    void Start()
    {
        trialSetupRef = this.GetComponent<TrialSetup>();
        fixationLoc = GameObject.Find("FixationCross").transform;
    }

    public void ShowCue()
    {
        List<GameObject> tTrials = trialSetupRef.targetTrials;

        Debug.Log("There are " + tTrials.Count + " trials left.");

        // select one of the trials at random and update the list of trials with the selected trial removed
        int n = Random.Range(0, tTrials.Count);
        activeTarget = tTrials[n];
        tTrials.RemoveAt(n);

        Debug.Log(activeTarget.name);

        // display cue in front of subject
        cueClone = Instantiate(activeTarget, fixationLoc.position, fixationLoc.rotation) as GameObject;
        cueClone.transform.parent = fixationLoc;

        string targOrientation = cueClone.name;
        int findT = targOrientation.IndexOf("T");
        targOrientation = targOrientation.Substring(findT + 1);
        targOrientation = targOrientation.Remove(targOrientation.Length - 7);

        if (targOrientation == "0")
        {
            cueClone.transform.localRotation = Quaternion.Euler(180, 270, -180);
        }
        if (targOrientation == "90")
        {
            cueClone.transform.localRotation = Quaternion.Euler(180, 180, -180);
        }
        if (targOrientation == "180")
        {
            cueClone.transform.localRotation = Quaternion.Euler(180, 90, -180);
        }
        if (targOrientation == "270")
        {
            cueClone.transform.localRotation = Quaternion.Euler(180, 0, -180);
        }

        cueClone.tag = "Clone";
    }

}



