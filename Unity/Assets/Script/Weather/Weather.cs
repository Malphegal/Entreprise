using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour {

    public Light sun; // GameObject du soleil
    public float secondsInDay; // Nombre de secondes dans une journée
    [Range(0, 1)]
    public float currentTimeOfDay; // Moment actuel dans la journée
    private float timeMultiplier; // Vitesse du temps
    private float sunInitialIntensity; // Intensité de base du soleil
    private bool runCycle; // Le cycle est en cours ou non ?

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        currentTimeOfDay = 0.3F;
        secondsInDay = 60F;
        timeMultiplier = 1F;
        //runCycle = true;
    }

    public void StartCycle(bool nouvelleValeur)
    {
        runCycle = nouvelleValeur;
    }

    void Update()
    {
        if (runCycle)
        {
            UpdateSun();
            currentTimeOfDay += (Time.deltaTime / secondsInDay) * timeMultiplier;
            if (currentTimeOfDay >= 1)
                currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360F) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23F || currentTimeOfDay >= 0.75F)
            intensityMultiplier = 0;
        else if (currentTimeOfDay <= 0.25F)
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23F) * (1 / 0.02F));
        else if (currentTimeOfDay >= 0.73F)
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73F) * (1 / 0.02F)));

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
