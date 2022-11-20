using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightHandler : MonoBehaviour
{
    public GameObject Sun;

    public GameObject WindScreen;

    public string LightColorChangeFlag;

    public float rotationSpeedX;

    public float rotationSpeedy;

    public float rotationSpeedz;

    void Start()
    {
        Sun = GameObject.Find("Sun");
    }

    void Update()
    {
        /*    Sun.transform.Rotate (rotationSpeedX,rotationSpeedy,rotationSpeedz,Space.World);
           if(
               Sun.transform.localRotation.eulerAngles.x>0.0f&&
                Sun.transform.localRotation.eulerAngles.x<180.0f
               ){
                    if(Sun.active==false){
                        Sun.SetActive(true);
                    }
                    if(Sun.active&&Sun.transform.localRotation.eulerAngles.x==90.0f){
                        Sun.GetComponent<Light>().intensity = 1.0f;
                    }else{
                       Sun.GetComponent<Light>().intensity = 1.0f-1.0f/(90.0f/Mathf.Abs(90.0f-Sun.transform.localRotation.eulerAngles.x)) ;
                    }

               }else if(Sun.active){
                    Sun.SetActive(false);
               }
                                                                    
                if(LightColorChangeFlag=="Dark" ){
                    Sun.GetComponent<Light>().color -= (Color.white / 5.0f) * Time.deltaTime;
                }else if(LightColorChangeFlag=="Light"){
                    Sun.GetComponent<Light>().color += (Color.white / 5.0f) * Time.deltaTime;
                }

        }*/
    }

    public void MakeStorm()
    {
        Debug.Log("MAKESTORM");
        WindScreen.GetComponent<WindZone>().windMain = 5.0f;
        WindScreen.GetComponent<WindZone>().windTurbulence = 10.0f;
        WindScreen.GetComponent<WindZone>().windPulseFrequency = 1.0f;
        WindScreen.GetComponent<WindZone>().windPulseMagnitude = 10.0f;
    }

    public void EndStorm()
    {
        Debug.Log("ENDSTORM");
        WindScreen.GetComponent<WindZone>().windMain = 0.15f;
        WindScreen.GetComponent<WindZone>().windTurbulence = 0.4f;
        WindScreen.GetComponent<WindZone>().windPulseFrequency = 0.5f;
        WindScreen.GetComponent<WindZone>().windPulseMagnitude = 1.0f;
    }
}
