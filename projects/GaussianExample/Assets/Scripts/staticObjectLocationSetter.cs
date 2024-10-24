using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectLocationSetter : MonoBehaviour
{
    // Reference to the ReadCSVStatic component
    private ReadCSVStatic csvReader;
    public string objectName; // Name of the object to control (set in Inspector)

    private float xPos;

    void Start()
    {
        // Get the ReadCSVStatic component from the scene (it should be attached to a GameObject)
        csvReader = FindObjectOfType<ReadCSVStatic>();

        // Fetch the data for the specific object
        if (csvReader != null)
        {
            // Get the object data for the specified objectName
            Dictionary<string, float> objectData = csvReader.GetObjectData();

            // Check if the objectName exists in the data
            if (objectData != null && objectData.ContainsKey(objectName))
            {
                // Set the xPos from the data for the object
                xPos = objectData[objectName];

                // Initialize position to the first frame's xPos, keeping the current y and z position
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }
            else
            {
                Debug.LogError($"Object '{objectName}' not found in PoseData.");
            }
        }
        else
        {
            Debug.LogError("ReadCSVStatic component not found!");
        }
    }
}
