using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float acceleration = 0.1f;
    public float maxSpeed = 1.0f;
    protected float currentSpeed = 0.0f;


    /// <summary>
    /// How much the player should displace given its currentSpeed;
    /// </summary>
    /// <returns></returns>
    public float GetDisplacement()
    {
        return currentSpeed * Time.deltaTime;
    }

    private void Update()
    {
        bool shouldAccelerate = IsTapping();
        float accelerationSign = shouldAccelerate ? 1.0f : -1.0f;

        float v = accelerationSign * acceleration * Time.deltaTime;
        currentSpeed = currentSpeed + v;
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed);
    }

    protected bool IsTapping()
    {
        return true;
    }
}
