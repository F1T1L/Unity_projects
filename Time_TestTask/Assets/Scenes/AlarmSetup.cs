using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSetup : MonoBehaviour
{
    [SerializeField] Text alarmDigits;
    [SerializeField] Text alarmButtonText;
    [SerializeField] InputField alarmInput;
   
    DateTime alarmDatetime = new DateTime(2000, 1, 1, 0, 0, 0, 0);
    bool toggleMenu;
   
    private void Awake()
    {       
        UpdateString(alarmDatetime);
        toggleMenu = gameObject.active;        
    }
    void UpdateString(DateTime dt)
    {
        alarmDigits.text = dt.Hour.ToString("0#") + " : " + dt.Minute.ToString("0#") +
                         " : " + dt.Second.ToString("0#");
    }
    
    public void ChangeHoursAdd()
    {
        alarmDatetime = alarmDatetime.AddHours(+1f);  
        UpdateString(alarmDatetime);
    }
    public void ChangeMinutesAdd()
    {
        alarmDatetime = alarmDatetime.AddMinutes(+1f);
        UpdateString(alarmDatetime);
    }
    public void ChangeSecondsAdd()
    {
        alarmDatetime = alarmDatetime.AddSeconds(+1f);
        UpdateString(alarmDatetime);
    }  
    public void OnClickButton()
    {
        if (toggleMenu)
        {
            print("TryParse Alarm: "+ DateTime.TryParse(alarmInput.text,out alarmDatetime));
            UpdateString(alarmDatetime);
            alarmInput.gameObject.active=false;
            alarmButtonText.text = "ALARM";
            gameObject.active = false;
            toggleMenu = false;
        }
        else
        {           
            alarmInput.gameObject.active=true;
            alarmButtonText.text = "SAVE";
            toggleMenu = true;
            gameObject.active = true;
        } 
    }
}
