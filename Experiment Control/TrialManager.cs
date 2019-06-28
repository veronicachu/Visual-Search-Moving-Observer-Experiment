using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TrialManager : MonoBehaviour {

    //Time Variables
    public float cueTime;
    public float subtrialTime;
    public int trialNumber;
    public float timer;

    // Within trial fucntion call flags
    bool e_cueCall = false;
    bool e_trialCall = false;
    bool subTrial1 = false;
    bool subTrial2 = false;
    bool subTrial3 = false;
    bool subTrial4 = false;
    bool subTrial5 = false;
    bool subTrial6 = false;
    bool e_respDestroyCall = false;
    bool e_flickStartCall = false;

    // Trial progression flag variables
    private bool cueDone = false;
    private bool flickerDone = false;
    private bool responseDone = false;

    // Exp script references
    public GameObject CameraRef;
    public iTweenMove CarMoveRef;
    public InputField InputRef;
    public GameObject InputGameObjectRef;
    private FlickerManager flickManagerRef;
    private FlickerControl flickControlRef;

    public TrialSetup trialSetupRef; 
    public TrialCue trialCueRef;
    public TrialSpawn trialSpawnRef;
    public LocationData locationDataRef;
    public ResponseData responseDataRef;

    public string pathname;

    // Server Manager reference
    [HideInInspector] public int marker = 0;

    // Reference to photocell flicker object
    private GameObject pcObjectRef;
    private FlickerPhotocell pcFlickerRef;

    //Temporary data storage list
    [HideInInspector] public string targetColor;
    [HideInInspector] public string targetOrientation;
    [HideInInspector] public string targetLocation1;
    [HideInInspector] public string targetLocation2;
    [HideInInspector] public string targetLocation3;
    [HideInInspector] public string targetLocation4;
    [HideInInspector] public string targetLocation5;
    [HideInInspector] public string targetLocation6;
    [HideInInspector] public int resp;
    [HideInInspector] public int targetCounter = 0;

    //Waypoints Names
    public List<string> waypointNames = new List<string>();

    // Scene to load
    public int nextSceneNum;

    // Use this for initialization
    void Start () {
        CameraRef = GameObject.Find("HUDCamera"); 
        CarMoveRef = CameraRef.GetComponent<iTweenMove>();
        flickManagerRef = this.GetComponent<FlickerManager>();
        flickControlRef = GameObject.Find("InnerGreen").GetComponent<FlickerControl>();
        pcObjectRef = GameObject.Find("PhotocellCue");
        pcFlickerRef = pcObjectRef.GetComponent<FlickerPhotocell>();

        trialSetupRef = this.GetComponent<TrialSetup>();
        trialCueRef = this.GetComponent<TrialCue>();
        trialSpawnRef = this.GetComponent<TrialSpawn>();

        locationDataRef = this.GetComponent<LocationData>();
        responseDataRef = this.GetComponent<ResponseData>();
    }

    // Update is called once per frame
    void Update() {
        // Calls the different stages of the the trial
        if (!cueDone && !flickerDone && !responseDone)
          if (trialSetupRef.loadComplete == true)
                    CueScene();
        if (cueDone && !flickerDone && !responseDone)
            FlickerScene();
        if (cueDone && flickerDone && !responseDone)
            ResponseScene();
        if (cueDone && flickerDone && responseDone)
            trialUpdate();
    }

    void CueScene()
    {
        // Generates the trial's cue
        if (!e_cueCall)
        {
            Debug.Log("Cue phase start");
            trialCueRef.ShowCue();                    // Show cue for the trial

            // Store the current target's color and orientation temporarily
            targetColor = trialCueRef.activeTarget.tag;
            targetOrientation = trialCueRef.activeTarget.name;

            flickManagerRef.setFlickerFreq();       // Set the flicker frequencies for the trial
            pcFlickerRef.Frequency = flickControlRef.Frequency;     // Set photocell object frequency for the trial

            e_cueCall = true;
        }

        // Keeps track of time for when minimum cue phase reached
        timer += Time.deltaTime;
        if (timer >= cueTime && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            cueDone = true;                         // Sets the cueDone flag to true
            timer = 0f;                             // Reset the timer for the next experiment phase
        }
    }

    void FlickerScene()
    {
        //  Start flickering the HUD
        if (!e_flickStartCall)
        {
            Debug.Log("Flicker start");

            flickManagerRef.startAllFlicker();      // start flicker for HUD
            pcFlickerRef.beginFlicker();            // start flicker for PC object

            locationDataRef.NewFile();
            CarMoveRef.MoveCar();

            e_flickStartCall = true;
        }

        // Generates the sub-trials of search arrays
        timer += Time.deltaTime;

        if (!e_trialCall )
        {
            // Sub-trial #1
            if (!subTrial1 && timer >= 0)
            {
                Debug.Log("subt1 start");

                DestroyClones();                                                    // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[0];
                trialSpawnRef.spawnShapes();                                        // generate search array
                RotateClones(waypointNames[0], -135);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation1 = trialSpawnRef.targetWaypointName;                 // store temp target value (if present)
                locationDataRef.writeData();

                marker = 1;
                subTrial1 = true;
            }

            // Sub-trial #2
            if (!subTrial2 && timer >= subtrialTime)
            {
                Debug.Log("subt2 start");

                DestroyClones();                                                // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[1];
                trialSpawnRef.spawnShapes();                                      // generate search array
                RotateClones(waypointNames[0], -80);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation2 = trialSpawnRef.targetWaypointName;          // store temp target value (if present)
                locationDataRef.writeData();

                marker = 2;
                subTrial2 = true;
            }
            // Sub-trial #3
            if (!subTrial3 && timer >= subtrialTime * 2)
            {
                Debug.Log("subt3 start");

                DestroyClones();                                                // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[2];
                trialSpawnRef.spawnShapes();                                      // generate search array
                RotateClones(waypointNames[0], -15);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation3 = trialSpawnRef.targetWaypointName;          // store temp target value (if present)
                locationDataRef.writeData();

                marker = 3;
                subTrial3 = true;
            }
            // Sub-trial #4
            if (!subTrial4 && timer >= subtrialTime * 3)
            {
                Debug.Log("subt4 start");

                DestroyClones();                                                // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[3];
                trialSpawnRef.spawnShapes();                                      // generate search array
                RotateClones(waypointNames[0], 60);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation4 = trialSpawnRef.targetWaypointName;          // store temp target value (if present)
                locationDataRef.writeData();

                marker = 4;
                subTrial4 = true;
            }
            // Sub-trial #5
            if (!subTrial5 && timer >= subtrialTime * 4)
            {
                Debug.Log("subt5 start");

                DestroyClones();                                                // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[4];
                trialSpawnRef.spawnShapes();                                      // generate search array
                RotateClones(waypointNames[0], 90);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation5 = trialSpawnRef.targetWaypointName;          // store temp target value (if present)
                locationDataRef.writeData();

                marker = 5;
                subTrial5 = true;
            }
            // Sub-trial #6
            if (!subTrial6 && timer >= subtrialTime * 5)
            {
                Debug.Log("subt6 start");

                DestroyClones();                                                // destroys previous cubes
                trialSpawnRef.waypointtag = waypointNames[5];
                trialSpawnRef.spawnShapes();                                      // generate search array
                RotateClones(waypointNames[0], 135);                               // hard coded y-axis rotation*

                if (trialSpawnRef.targetSignal == 0)                                // add to target counter for response correct later
                    targetCounter++;
                targetLocation6 = trialSpawnRef.targetWaypointName;          // store temp target value (if present)
                locationDataRef.writeData();

                marker = 6;
                subTrial6 = true;
            }
        }
        
        //  When full trial time is reached, end the trial
        if (timer >= (subtrialTime * 6))
        {
            Debug.Log("Flicker stop");

            flickManagerRef.stopAllFlicker();       // turns off the HUD flicker
            pcFlickerRef.stopFlicker();             // turns off the PC object flicker

            timer = 0f;                             // Reset the timer for the next experiment phase
            flickerDone = true;                     // Sets the flickerDone flag to true
        }
    }

    void ResponseScene()
    {
        Debug.Log("Waiting for subject response...");

        // Destroys the mask array and reveals input
        if (!e_respDestroyCall)
        {
            DestroyClones();
            InputGameObjectRef.SetActive(true);
            InputRef.ActivateInputField();
            InputRef.text = "";

            marker = 7;
            e_respDestroyCall = true;
        }

        // Wait for the subject's response
        InputRef.Select();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (InputRef.text == "")
            {
                Debug.Log("input is empty");
            }
            else if (InputRef.text != "")
            {
                resp = int.Parse(InputRef.text);
                InputRef.DeactivateInputField();
                InputGameObjectRef.SetActive(false);
                Debug.Log("input is " + resp);

                // Write the location data and the response data to local text file
                locationDataRef.saveData();
                responseDataRef.writeData();

                // Sets the responseDone flag to true
                //Debug.Log("A valid response has been recorded...");

                responseDone = true;                                        // Sets the responeDone flag to true
            }
        }
    }

    void trialUpdate()
    {
        trialNumber++;                              // Increment the trial number by one

        // Closes the experiment out on the last trial
        if (trialNumber == trialSetupRef.totalTrials)
        {
            responseDataRef.saveData();                        // Saves the written data to a csv file
            SceneManager.LoadScene(nextSceneNum);              // Switches Trial Scene to End Scene
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            responseDataRef.saveData();              // Saves the written data to a csv file
            SceneManager.LoadScene(nextSceneNum);              // Switches Trial Scene to End Scene
        }

        // Reset car position
        CarMoveRef.ReplaceCar();
            
        // Reset all experiment phase progression flags to false for the next trial
        cueDone = false;
        flickerDone = false;
        responseDone = false;

        marker = 0;
        targetCounter = 0;

        // Reset all calls to exp and flicker code to false for the next trial
        e_cueCall = false;
        e_trialCall = false;
        subTrial1 = false;
        subTrial2 = false;
        subTrial3 = false;
        subTrial4 = false;
        subTrial5 = false;
        subTrial6 = false;
        e_flickStartCall = false;
        e_respDestroyCall = false;
    }

    private void RotateClones(string array, float rotationy)
    {
        GameObject[] arrayClones = GameObject.FindGameObjectsWithTag("Clone");
        for (int i = 0; i < arrayClones.Length; i ++)
        {
            arrayClones[i].transform.Rotate(0, rotationy, 0, Space.World);
        }
    }

    private void DestroyClones()
    {
        // Find all the cube clones and destroy them
        GameObject[] arrayClones = GameObject.FindGameObjectsWithTag("Clone");
        for (int i = 0; i < arrayClones.Length; i++)
        {
            Destroy(arrayClones[i]);
        }
    }
}
