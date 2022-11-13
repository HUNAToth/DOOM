using UnityEngine;
using System.Collections;
 
public class DateOfTime : MonoBehaviour
{

public GameObject sun;
public GameObject Clock;
public int day;
public int hour;
public int min;

public System.DateTime startTime;
public float prevTime;


        public float rotationSpeedX;
        public float rotationSpeedy;
        public float rotationSpeedz;

void Start(){
     sun = GameObject.Find("Sun");
     startTime = System.DateTime.UtcNow;
}

/*mp:fok
1:0,25*/




 void Update(){
   var diffInSeconds = (System.DateTime.UtcNow - startTime).TotalSeconds;

    if((int)(diffInSeconds) > prevTime/100){
            if(min+1==60){
                min = 0;
                if(hour+1==24){
                    hour = 0;
                    day += 1;
                }else{
                    hour += 1;
                }
            }else{
                min += 1;
            }
            Clock.GetComponent<UnityEngine.UI.Text>().text  =day+".nap "+hour+":"+min;
    }


//TODO intensity legyen exponenciális 1 ig
    if((int)(diffInSeconds*100) > prevTime){
        sun.transform.Rotate (rotationSpeedX,rotationSpeedy,rotationSpeedz,Space.World);
        if(
               sun.transform.localRotation.eulerAngles.x>0.0f&&
                sun.transform.localRotation.eulerAngles.x<180.0f
               ){
                    if(sun.active==false){
                        sun.SetActive(true);
                    }
                    if(sun.active&&sun.transform.localRotation.eulerAngles.x==90.0f){
                        sun.GetComponent<Light>().intensity = 1.0f;
                    }else{
                       sun.GetComponent<Light>().intensity = 1.0f-1.0f/(90.0f/Mathf.Abs(90.0f-sun.transform.localRotation.eulerAngles.x)) ;
           
                    }

               }else if(sun.active){
                    sun.SetActive(false);         
                     sun.GetComponent<Light>().intensity= 0.11f   ;
               }
      

        prevTime= (int)(diffInSeconds*100);


       // Debug.Log(day+"-"+hour+":"+min);

    }

  




    //VAGY az alap quaternion x, és w (rotation)
 /*   if(
               sun.transform.localRotation.eulerAngles.x>0.0f&&
                sun.transform.localRotation.eulerAngles.x<15.0f
    ){
        Debug.Log("1 óra");
    }else  if(
               sun.transform.localRotation.eulerAngles.x>15.0f&&
                sun.transform.localRotation.eulerAngles.x<30.0f
    ){
        Debug.Log("2 óra");
    }else{
        Debug.Log("nics 1/2 óra");
    }*/
 }
   
}