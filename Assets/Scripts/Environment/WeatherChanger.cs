using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    public GameObject EnvironmentalEffects;

    DynamicLightHandler DynamicWeather;

    public string Mode;

    private void Start()
    {
        //EnvironmentalEffects = GameObject.Find("EnvironmentalEffects");
        DynamicWeather =
            EnvironmentalEffects.GetComponent<DynamicLightHandler>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Mode == "Enter")
        {
            DynamicWeather.LightColorChangeFlag = "Dark";
            DynamicWeather.MakeStorm();
        }
        else
        {
            DynamicWeather.LightColorChangeFlag = "Light";
            DynamicWeather.EndStorm();
        }
    }
}
