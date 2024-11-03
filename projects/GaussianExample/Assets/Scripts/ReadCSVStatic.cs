using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCSVStatic : MonoBehaviour
{
    // Public property to access poseData with only xPos
    public Dictionary<string, float> PoseData { get; private set; } // Store only xPos for each unique objectName

    public string csvFileName;

    // Start is called before the first frame update
    void Start()
    {
        PoseData = new Dictionary<string, float>(); // Initialize the dictionary
        ReadCSVFile();
    }

    void ReadCSVFile()
    {
        StreamReader strReader = new StreamReader("Assets/Locations/" + csvFileName);
        bool endOfFile = false;

        strReader.ReadLine(); // Skip the header line

        HashSet<string> objectNameSet = new HashSet<string>(); // To track used names for uniqueness

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

            // Stop processing if we reach a frame other than 0
            if (frame != 0)
            {
                break;
            }

            string objectName = dataValues[1];
            float xPos = float.Parse(dataValues[2]) / 100;

            // If the object name already exists, generate a unique name
            if (objectNameSet.Contains(objectName))
            {
                int duplicateCount = 1;
                string newObjectName = objectName + duplicateCount;

                // Keep incrementing the number until a unique name is found
                while (objectNameSet.Contains(newObjectName))
                {
                    duplicateCount++;
                    newObjectName = objectName + duplicateCount;
                }

                objectName = newObjectName; // Use the new unique name
            }

            // Store the unique object name in the HashSet to ensure uniqueness
            objectNameSet.Add(objectName);

            // Store only xPos for this object in frame 0
            PoseData[objectName] = xPos;
        }
    }

    // Function to return the pose data for frame 0
    public Dictionary<string, float> GetObjectData()
    {
        return PoseData;
    }
}
