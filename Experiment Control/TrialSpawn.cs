using UnityEngine;
using System.Collections.Generic;

public class TrialSpawn : MonoBehaviour
{
    // reference to other scripts


    // for array locations
    [HideInInspector]
    public List<Transform> waypoints = new List<Transform>();
    [HideInInspector]
    public List<GameObject> redDistractors = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> greenDistractors = new List<GameObject>();
    [HideInInspector]
    public Transform targetWaypoint;
    [HideInInspector]
    public string targetWaypointName;
    public int subsetNum;
    [HideInInspector]
    public GameObject[] arrayItems;    // for storing locations into a file
    [HideInInspector]
    public string waypointtag;
    [HideInInspector]
    public int targetSignal;

    // for storing the number of red and green distractors
    private int redNum;
    private int greenNum;

    // for switching cases
    private string targetTag;
    
    public void spawnShapes()
    {
        // Collects the search array locations
        GameObject[] arrayObjects = GameObject.FindGameObjectsWithTag(waypointtag);

        waypoints.Clear();
        for (int i = 0; i < arrayObjects.Length; i++)
        {
            waypoints.Add(arrayObjects[i].transform);
        }

        // Set up the game object array for object clones
       GameObject[] searchArray = new GameObject[subsetNum];

        // Get the current target
        GameObject target = GetComponent<TrialCue>().activeTarget;
        
        // Get the signal for whether or not there will be a target
        targetSignal = Random.Range(0, 3);                                      // determines if there will be a target in this subtrial

        // Collect the possible distractor game objects
        redDistractors.Clear();
        greenDistractors.Clear();
        redDistractors.AddRange(GameObject.FindGameObjectsWithTag("RedCube"));
        greenDistractors.AddRange(GameObject.FindGameObjectsWithTag("GreenCube"));

        // Removes the current target from the list of distractors
        for (int i = 0; i < redDistractors.Count; i++)
        {
            if (redDistractors[i].name == target.name)
                redDistractors.RemoveAt(i);
        }
        for (int i = 0; i < greenDistractors.Count; i++)
        {
            if (greenDistractors[i].name == target.name)
                greenDistractors.RemoveAt(i);
        }

        //targetSignal = 0;

        // If target present in current sub-trial, place target in random location and label as corresponding color case
        if (targetSignal == 0)
        {
            // Select random waypoint location
            int targetIndex = Random.Range(0, waypoints.Count);
            targetWaypoint = waypoints[targetIndex];
            targetWaypointName = waypoints[targetIndex].name;

            // Place target clone; establish name for switch-case
            searchArray[0] = Instantiate(target, targetWaypoint.position, target.transform.rotation) as GameObject;
            //searchArray[0].transform.SetParent(targetWaypoint);
            waypoints.RemoveAt(targetIndex);
            targetTag = target.tag;
        }
        // If target absent in current sub-trial, place target in the 'no target location' and label as "NoTarget" case
        if (targetSignal > 0)
        {
            // Get the no target waypoint location
            targetWaypoint = GameObject.Find("No Target Waypoint").transform;
            targetWaypointName = "No Target";

            // Place and tag target clone; establish name for switch-case
            GameObject noTarget = Instantiate(target, targetWaypoint.position, target.transform.rotation) as GameObject;
            noTarget.tag = "Clone";
            targetTag = "NoTarget";
        }

        int tempSubsetNum = subsetNum;      // store subsetNum into a temp that is useable
        Debug.Log(targetTag);

        // Places the distractors in the other locations
        switch (targetTag)
        {
            // Places distractors when no target is present
            case "NoTarget":
                redNum = tempSubsetNum / 2;
                greenNum = tempSubsetNum / 2;
                
                // Red Distractors
                for (int i = 0; i < redNum; i++)
                {
                    int distIndex = Random.Range(0, redDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];

                    searchArray[i] = Instantiate(redDistractors[distIndex], distWaypoint.position, redDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                // Green Distractors
                for (int i = redNum; i < redNum + greenNum; i++)
                {
                    int distIndex = Random.Range(0, greenDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];
                    searchArray[i] = Instantiate(greenDistractors[distIndex], distWaypoint.position, greenDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                arrayItems = searchArray;
                break;

            // Places distractors when target is red
            case "RedCube":
                tempSubsetNum = tempSubsetNum - 1;
                redNum = tempSubsetNum / 2;
                greenNum = tempSubsetNum - redNum;

                // Red Distractors
                for (int i = 0; i < redNum; i++)
                {
                    int distIndex = Random.Range(0, redDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];
                    searchArray[i + 1] = Instantiate(redDistractors[distIndex], distWaypoint.position, redDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                // Green Distractors
                for (int i = redNum; i < redNum + greenNum; i++)
                {
                    int distIndex = Random.Range(0, greenDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];
                    searchArray[i + 1] = Instantiate(greenDistractors[distIndex], distWaypoint.position, greenDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                arrayItems = searchArray;
                break;

            // Places distractors when target is green
            case "GreenCube":
                tempSubsetNum = tempSubsetNum - 1;
                greenNum = tempSubsetNum / 2;
                redNum = tempSubsetNum - greenNum;

                // Red Distractors
                for (int i = 0; i < redNum; i++)
                {
                    int distIndex = Random.Range(0, redDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];
                    searchArray[i + 1] = Instantiate(redDistractors[distIndex], distWaypoint.position, redDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                // Green Distractors
                for (int i = redNum; i < redNum + greenNum; i++)
                {
                    int distIndex = Random.Range(0, greenDistractors.Count);
                    int tempIndex = Random.Range(0, waypoints.Count);
                    Transform distWaypoint = waypoints[tempIndex];
                    searchArray[i + 1] = Instantiate(greenDistractors[distIndex], distWaypoint.position, greenDistractors[distIndex].transform.rotation) as GameObject;
                    waypoints.RemoveAt(tempIndex);
                }
                arrayItems = searchArray;
                break;
        }

        // Tag all the objects as "Clone" for easy deletion later
        for (int i = 0; i < searchArray.Length; i++)
        {
            searchArray[i].transform.parent = transform;
            searchArray[i].tag = "Clone";
        }
    }
}
