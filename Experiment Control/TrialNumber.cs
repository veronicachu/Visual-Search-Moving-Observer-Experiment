using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrialNumber : MonoBehaviour {

    public GameObject expManagerObject;
    private TrialManager expManagerScript;
    private TrialSetup expSetupScript;
    private Text uitext;

    void Start()
    {
        expManagerScript = expManagerObject.GetComponent<TrialManager>();
        expSetupScript = expManagerObject.GetComponent<TrialSetup>();
        uitext = this.GetComponent<Text>();
    }

	void Update ()
    {
        int trialnum = expManagerScript.trialNumber;
        int totaltrial = expSetupScript.totalTrials;
        trialnum = trialnum + 1;
        uitext.text = "Trial " + trialnum.ToString() + "/" + totaltrial;
	}
}
