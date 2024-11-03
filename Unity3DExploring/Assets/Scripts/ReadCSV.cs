using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    // Public property to access poseData
    public Dictionary<string, Dictionary<int, (float xPos, float rotation)>> PoseData { get; private set; }

    public string csvFileName;

    // Start is called before the first frame update
    void Start()
    {
        PoseData = new Dictionary<string, Dictionary<int, (float, float)>>(); // Initialize the dictionary
        ReadCSVFile();
    }

    void ReadCSVFile()
    {
        StreamReader strReader = new StreamReader("Assets/Locations/" + csvFileName);
        bool endOfFile = false;

        strReader.ReadLine(); // Skip the header line

        while (!endOfFile)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }

            var dataValues = dataString.Split(',');

            int frame = int.Parse(dataValues[0]);
            string objectName = dataValues[1];
            float xPos = float.Parse(dataValues[2]) / 100;
            float rotation = float.Parse(dataValues[3]);

            if (!PoseData.ContainsKey(objectName))
            {
                PoseData[objectName] = new Dictionary<int, (float, float)>();
            }

            PoseData[objectName][frame] = (xPos, rotation);
        }
    }

    // Function to return only the data for a specific object by name
    public Dictionary<int, (float xPos, float rotation)> GetObject(string objectName)
    {
        // Check if the object exists in the PoseData dictionary
        if (PoseData.ContainsKey(objectName))
        {
            return PoseData[objectName];
        }
        else
        {
            Debug.LogError($"Object '{objectName}' not found in PoseData.");
            return null; // Return null if the object is not found
        }
    }
}
