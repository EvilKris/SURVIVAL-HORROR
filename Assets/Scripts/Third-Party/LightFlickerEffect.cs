using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class LightFlickerEffect : MonoBehaviour
{
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new Light light;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0.5f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 2.3f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 8;
    [Tooltip("Off Time (min)")]
    [Range(1, 10)]
    public float offDurationMin = 2f;

    [Tooltip("Off Time (max)")]
    [Range(1, 20)]
    public float offDurationMax = 10f;

    [Tooltip("On Time (min)")]
    [Range(1, 210)]
    public float onDurationMin = 0.5f;

    [Tooltip("On Time (max)")]
    [Range(1, 10)]
    public float onDurationMax = 2f;

        // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;


    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
       
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light>();
        }

        StartCoroutine(DoOnOff());
    }


    IEnumerator DoOnOff()
    {
        while (true)
        {
            enabled = false;
            light.intensity = 0;
            yield return new WaitForSeconds(Random.Range(offDurationMin, offDurationMax));
            enabled = true;
            yield return new WaitForSeconds(Random.Range(onDurationMin, onDurationMax));
        }
    }


    void Update()
    {
        if (light == null)
            return;

       
            // pop off an item if too big
            while (smoothQueue.Count >= smoothing)
            {
                lastSum -= smoothQueue.Dequeue();
            }

            // Generate random new item, calculate new average
            float newVal = Random.Range(minIntensity, maxIntensity);
            smoothQueue.Enqueue(newVal);
            lastSum += newVal;

            // Calculate new smoothed average
            light.intensity = lastSum / (float)smoothQueue.Count;

        
    }

}