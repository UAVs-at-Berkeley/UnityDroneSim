using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPulseNoise : MonoBehaviour {

    public Rigidbody drone;

    public bool apply_force = true;

    public float strength_coef = 0.0015f;

    //mean/variance of force strength setpt
    public float strength_mean = 30.0f;
    public float strength_variance = 20.0f;

    //variance of force vector magnitude during the pulse around the setpt
    public float strength_hold_variance = 1000.0f;

    //mean/variance time between pulses
    public float pulse_period_mean = 7; // seconds
    public float pulse_period_variance = 5;

    //mean/variance duration of pulses
    public float pulse_duration_mean = 10; // seconds
    public float pulse_duration_variance = 2;

    //mean/variance amount of time for each motion direction target
    public float motion_period_mean = 8; //seconds
    public float motion_period_variance = 3f;

    //mean/variance speed of wind vector rotation
    public float wind_change_speed_mean = 0.05f;
    public float wind_change_speed_variance = 0.01f;

    public float strength_off_speed = 50.0f;
    public float strength_on_speed = 70.0f;

    //mean/variance rate of change for wind direction

    System.Random r;

    float pulse_timer = 0.0f;
    float pulse_period = 0.0f;
    float pulse_duration = 0.0f;
    float base_strength = 0.0f;
    float strength = 0.0f;

    //0: decide pulse period
    //1: wait for pulse, then decide pulse duration, strength
    //2: during pulse
    int pulse_mode = 0;


    float motion_timer = 0.0f;
    float motion_period = 0.0f;
    float wind_change_speed = 0.0f;
    public Quaternion targetDirection; // wind noise direction

    //0: decide target direction, speed of change, and motion period
    //1: slerp given the above
    int motion_mode = 0;

	// Use this for initialization
	void Start () {
        r = new System.Random();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // random walk

        if (pulse_mode == 0)
        {
            pulse_timer = 0.0f; //reset
            pulse_period = SamplePositive(pulse_period_mean, pulse_period_variance);
            pulse_mode = 1;
        } 
        else if (pulse_mode == 1) 
        {
            pulse_timer += Time.deltaTime;

            //slerp the wind speed back to 0
            strength = strength - Time.deltaTime * strength_off_speed;
            if (strength < 0.0f) {
                strength = 0.0f;
            }

            if (pulse_timer >= pulse_period){
                pulse_timer = 0.0f; //reset
                pulse_duration = SamplePositive(pulse_duration_mean, pulse_duration_variance);
                base_strength = SamplePositive(strength_mean, strength_variance);
                pulse_mode = 2;
            }
        } 
        else if (pulse_mode == 2)
        {
            pulse_timer += Time.deltaTime;
            if (pulse_timer >= pulse_duration) {
                pulse_timer = 0.0f; //reset
                pulse_mode = 0;
            } else {
                //apply force here
                float target_strength = Sample(base_strength, strength_hold_variance);

                //within 10%
                if (Mathf.Abs(strength - target_strength) / (target_strength + 1e-8) < 0.4)
                {
                    strength = target_strength;
                }
                else
                {
                    // slerp to ramp on and in between values
                    int dir = target_strength > strength ? 1 : -1;
                    strength = strength + Time.deltaTime * strength_on_speed;

                    if (dir * strength > dir * target_strength)
                    {
                        strength = target_strength;
                    }
                }


            }
        } 



        if (motion_mode == 0) 
        {
            motion_timer = 0.0f;
            motion_period = SamplePositive(motion_period_mean, motion_period_variance);
            wind_change_speed = SamplePositive(wind_change_speed_mean, wind_change_speed_variance);
            targetDirection = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
            motion_mode = 1;
        }
        else if (motion_mode == 1)
        {
            motion_timer += Time.deltaTime;
            //do the slerp here
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * wind_change_speed);
            if (motion_timer > motion_period) {
                motion_timer = 0.0f;
                motion_mode = 0; 

            }
        }

        Vector3 ray = strength * (transform.rotation * Vector3.forward);

        if (apply_force)
        {
            drone.AddForce(ray * strength_coef, ForceMode.Impulse);
        }
        Debug.DrawRay(drone.position, ray, Color.green);
	}

    public float Sample(float mean, float var)
    {
        float n = NextGaussianDouble();

        return n * Mathf.Sqrt(var) + mean;
    }

    public float SamplePositive(float mean, float var) {
        return Mathf.Abs(Sample(mean, var));
    }

    public float NextGaussianDouble()
    {
        float u, v, S;

        do
        {
            u = 2.0f * (float) r.NextDouble() - 1.0f;
            v = 2.0f * (float) r.NextDouble() - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }
}
