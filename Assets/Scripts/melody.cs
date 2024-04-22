using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melody : MonoBehaviour
{
    public LibPdInstance patch;
    public float speed = 1.0f; // Speed of the scale playback
    public float maxHeight = 5.0f; // Maximum height the object can move to

    private float t;
    private int scaleIndex = 0; // Current index in the scale
    //private int[] scale = new int[] {72, 74, 76, 79, 76, 74, 72, 76, 74, 79, 72, 74}; // C major scale
    private int[] scale = new int[] {64, 65, 62, 64, 64, 65, 62, 64, 67, 69, 65, 67, 74, 79, 74, 76}; // C major scale

    void Update()
    {
        t += Time.deltaTime;

        if (t >= 1 / speed)
        {
            t = 0; // Reset time since we're moving to the next note
            scaleIndex = (scaleIndex + 1) % scale.Length; // Move to the next note in the scale

            int pitch = scale[scaleIndex];
            patch.SendMidiNoteOn(0, pitch, 80); // Send the MIDI note

            // Map the pitch to a height value
            float height = MapPitchToHeight(pitch);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }

    float MapPitchToHeight(int pitch)
    {
        int minPitch = 60; // C4
        int maxPitch = 83; // B5

        // Normalize the pitch to a 0-1 range
        float normalizedPitch = (float)(pitch - minPitch) / (maxPitch - minPitch);
        
        // Map the normalized pitch to the height range
        return normalizedPitch * maxHeight;
    }
}