using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityRandomizer : MonoBehaviour
{
    public GameObject light;
    public GameObject sun;
    public GameObject environmentEffects;
    // Start is called before the first frame update
    void Start()
    {
        
             sun = GameObject.Find("Sun");
             environmentEffects = GameObject.Find("EnvironmentEffects");
    }

    // Update is called once per frame
    void Update(){

    float randomNumber = Random.Range(98, 100);
    if(environmentEffects.GetComponent<DateOfTime>().hour <  8 ||
        environmentEffects.GetComponent<DateOfTime>().hour >  18
         ){ 
            light.GetComponent<Light>().intensity = randomNumber/100;
         }else{
            light.GetComponent<Light>().intensity = 0.0f;
        }
       /* float randomNumber = Random.Range(0, 
       1000);
        if(environmentEffects.GetComponent<DateOfTime>().hour <  10 ||
        environmentEffects.GetComponent<DateOfTime>().hour >  16
         ){

            if(randomNumber%127 == 0){
                light.GetComponent<Light>().intensity = 0.85f;
            }
            else if(randomNumber%13 == 0 && randomNumber%7 == 0 ){
                light.GetComponent<Light>().intensity = 0.60f;
            }
           else{    
                light.GetComponent<Light>().intensity = 1;
            }

        }else{
            light.GetComponent<Light>().intensity = 0.0f;
        }
        */
    }
}
