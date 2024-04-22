using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCtrl : MonoBehaviour
{
    public LibPdInstance patch;
    public float speed = 1.0f; // Speed of the scale playback

    private float t;
    private int scaleIndex = 0; // Current index in the scale
    private int[] scale = new int[] {60, 64, 67, 69, 72}; // C major scale

    void Update()
    {
        t += Time.deltaTime;

        // Update scaleIndex based on the speed and time, resetting when it exceeds the scale length
        if (t >= 1 / speed)
        {
            t = 0; // Reset time since we're moving to the next note
            scaleIndex = (scaleIndex + 1) % scale.Length; // Move to the next note in the scale

            int pitch = scale[scaleIndex];
            patch.SendMidiNoteOn(0, pitch, 80); // Send the MIDI note

            /
            float scaleFactor = 1.0f / ((pitch - 59) / (12.0f)); 
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
}
