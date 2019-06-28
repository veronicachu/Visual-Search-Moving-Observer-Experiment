/*
This code will store the data into a csv file, which can be found in the Data folder of the project.
*/

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ResponseData : MonoBehaviour
{
    StringBuilder csv = new StringBuilder();
    private string expPath;

    private TrialManager e_expManagerRef;
    private FlickerControl redFlickRef;
    private FlickerControl greenFlickRef;
    public GameObject redFlickObject;
    public GameObject greenFlickObject;

    void Start()
    {
        // Start new stream writer
        expPath = FileName();
        Debug.Log(expPath);
        e_expManagerRef = this.GetComponent<TrialManager>();
        redFlickRef = redFlickObject.GetComponent<FlickerControl>();
        greenFlickRef = greenFlickObject.GetComponent<FlickerControl>();

        // Write first line with data information
        string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
            "Red Freq", "Green Freq", "Target Color", "Target Orientation", 
            "Target 1", "Target 2", "Target 3", "Target 4", "Target 5", "Target 6",
            "Response","Actual","Correct");
        csv.AppendLine(newLine);
    }

    public static string FileName()
    {
        Directory.CreateDirectory(Application.dataPath + "/ResponseData");

        // Creates the file path to store into the Data folder
        return string.Format("{0}/ResponseData/{1}.csv",
                             Application.dataPath,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void writeData()
    {
        // Collects target color
        string targColor = e_expManagerRef.targetColor;
        targColor = targColor.Remove(targColor.Length - 4);     // remove "Cube" at end

        // Collects target orientation
        string targOrientation = e_expManagerRef.targetOrientation;
        int findT = targOrientation.IndexOf("T");
        targOrientation = targOrientation.Substring(findT + 1);

        // Collects target locations (absent targets marked as -99)
        //List<string> TargetXLocs = new List<string>();
        //List<string> TargetYLocs = new List<string>();
        //TargetXLocs = TargetsX();               // call method to grab and place targets' x locations into a list
        //TargetYLocs = TargetsY();               // call method to grab and place targets' y locatiosn into a list
        List<string> TargetLocations = new List<string>();
        TargetLocations = TargetLocs();
        
        //// Collect color frequencies in the trial
        string redFreq = redFlickRef.Frequency.ToString();
        string greenFreq = greenFlickRef.Frequency.ToString();

        // Collect response data from TrialManager
        string response = e_expManagerRef.resp.ToString();

        // To check if the selection matches any of the sub-trial target locations
        string targetCount = e_expManagerRef.targetCounter.ToString();
        string correct = CorrectResp(targetCount,response);

        // Writes a line with target and selection data
        string newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
            redFreq, greenFreq, targColor, targOrientation,
            TargetLocations[0], TargetLocations[1], TargetLocations[2],
            TargetLocations[3], TargetLocations[4], TargetLocations[5],
            response, targetCount, correct);
        csv.AppendLine(newLine);
    }

    public void saveData()
    {
        File.WriteAllText(expPath, csv.ToString());
    }

    public List<string> TargetLocs()
    {
        List<string> TargetLocations = new List<string>();

        TargetLocations.Add(e_expManagerRef.targetLocation1);
        TargetLocations.Add(e_expManagerRef.targetLocation2);
        TargetLocations.Add(e_expManagerRef.targetLocation3);
        TargetLocations.Add(e_expManagerRef.targetLocation4);
        TargetLocations.Add(e_expManagerRef.targetLocation5);
        TargetLocations.Add(e_expManagerRef.targetLocation6);

        return TargetLocations;
    }

    public string CorrectResp(string targCount, string resp)
    {
        string correct;

        // In case where no target appears at all, correct if subject responds "None"
        if (resp == targCount)
            correct = "1";
        else
            correct = "0";

        return correct;
    }
}
