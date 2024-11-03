using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maybeNewMovement : MonoBehaviour
{
    // Movement variables
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVel = 0.5f;
    public float maxRunVel = 2.0f;

    // Animation variables
    private Animator animator;
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;

    // Frame-based data
    private Dictionary<int, (float xPos, float rotation)> objectData;
    private int currentFrame = 0;
    private int frameStep = 1; // Jump 10 frames ahead for next target
    public float frameTime = 0.5f; // How much time to take for each frame (smoothness)
    private float timeSinceLastFrame = 0f;

    // Reference to the ReadCSV component
    private ReadCSV csvReader;
    public string objectName; // Name of the object to control (set in Inspector)

    void Start()
    {
        animator = GetComponent<Animator>();

        // Get the ReadCSV component from the scene (it should be attached to a GameObject)
        csvReader = FindObjectOfType<ReadCSV>();

        // Fetch the data for the specific object
        if (csvReader != null)
        {
            objectData = csvReader.GetObject(objectName);

            // Initialize position to the first frame's position
            if (objectData != null && objectData.ContainsKey(0))
            {
                var initialFrame = objectData[0];
                transform.position = new Vector3(initialFrame.xPos, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, initialFrame.rotation, 0); // Set initial rotation
            }
        }
        else
        {
            Debug.LogError("ReadCSV component not found!");
        }
    }

    void Update()
    {
        if (objectData == null || objectData.Count == 0) return;

        timeSinceLastFrame += Time.deltaTime;

        if (timeSinceLastFrame >= frameTime)
        {
            // Move to the next frame
            currentFrame += frameStep;

            if (!objectData.ContainsKey(currentFrame)) return; // Stop if there are no more frames

            var targetFrame = objectData[currentFrame];

            // Update target position and rotation based on the next frame
            Vector3 targetPosition = new Vector3(targetFrame.xPos, transform.position.y, transform.position.z);
            Quaternion targetRotation = Quaternion.Euler(0, targetFrame.rotation, 0);

            // Smoothly move and rotate towards the target using interpolation
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * acceleration);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * acceleration);

            // Set animation parameters based on movement
            Vector3 movement = targetPosition - transform.position;
            velocityX = movement.x;
            velocityZ = movement.z;

            // Update animator parameters
            animator.SetFloat("Velocity Z", velocityZ);
            animator.SetFloat("Velocity X", velocityX);

            // Reset time since last frame update
            timeSinceLastFrame = 0f;
        }
    }
}
