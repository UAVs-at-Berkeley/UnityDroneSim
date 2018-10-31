using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FreeSpaceDetection : MonoBehaviour {

    public int numHorizontalPoints;
    public int numBins;
    //public int numVerticalPoints;
    public float fov_degrees = 0; // total horizontal field of view 
    //public float plane_forward_dist; // how far on ground plane to shoot raycasts
    //public float plane_forward_start_dist; // offset from drone in forward direction on ground plane for raycasts
    public float maxDist = 100;

    public bool displayFreespace = false;

    private NativeArray<RaycastCommand> commands;

	// Use this for initialization
	void Start () {
        if (Mathf.Abs(fov_degrees) < 1e-8)
            fov_degrees = Camera.main.fieldOfView;
        
        if (numBins > numHorizontalPoints){
            throw new UnityException("More bins than points!");
        }
        if (numHorizontalPoints % numBins != 0)
        {
            throw new UnityException("numHorizontalPoints must be a multiple of numBins!");
        }

        commands = new NativeArray<RaycastCommand>(numHorizontalPoints, Allocator.Temp);
	}
	
    public float[] batchRaycast() {
        // Perform a single raycast using RaycastCommand and wait for it to complete
        // Setup the command and result buffers
        var results = new NativeArray<RaycastHit>(numHorizontalPoints, Allocator.Temp);

        Vector3 origin = transform.localPosition;
        //Vector3 localForward = transform.rotation * transform.forward;

        //linearly spaced
        for (int i = 0; i < numHorizontalPoints; i++)
        {
            float theta = -(fov_degrees / 2.0f) + i * fov_degrees / numHorizontalPoints;
            var angle_rot = Quaternion.AngleAxis(theta, Vector3.up);
            var direction = transform.rotation * angle_rot * Vector3.forward;
            commands[i] = new RaycastCommand(origin, direction, maxDist);
        }
        // Set the data of the first command
        //Vector3 origin = Vector3.forward * -10;
        //Vector3 direction = Vector3.forward;

        //commands[0] = new RaycastCommand(origin, direction);

        // Schedule the batch of raycasts
        var handle = RaycastCommand.ScheduleBatch(commands, results, 1);

        // Wait for the batch processing job to complete
        handle.Complete();

        // Copy the result. If batchedHit.collider is null there was no hit
        RaycastHit batchedHit = results[0];

        // DISPLAY
        if (displayFreespace){
            
        }

        //set to zero
        float[] output = new float[numBins];
        for (int i = 0; i < numBins; i++)
        {
            output[i] = 0.0f;
        }
        
        //normalize and place in buckets
        int elsPerBin = ((int)results.Length / numBins);
        for (int i = 0; i < results.Length; i++)
        {
            int bin = i / elsPerBin;
            output[bin] += results[i].distance / elsPerBin; // average each bin over entries
        }

        //get sum
        float totalSum = 0.0f;
        for (int i = 0; i < output.Length; i++)
        {
            totalSum += output[i];
        }

        float[] norm_output = new float[numBins+1];
        for (int i = 0; i < output.Length; i++)
        {
            norm_output[i] = output[i] / totalSum;
        }
        norm_output[numBins] = totalSum;

        results.Dispose();
        return norm_output;
    }

	// Update is called once per frame
	void Update () {

	}

	private void OnDestroy()
	{
        commands.Dispose();
	}
}
