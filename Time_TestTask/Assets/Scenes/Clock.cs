using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;


public class Clock : MonoBehaviour
{
    [SerializeField] Transform hoursPivot, secondsPivot, munitesPivot;
    [SerializeField] Text digits;
    [SerializeField] int whenUpdateInMinutes = 60;
    public TimeSpan SetDateTime { get; set; }
    DateTime datetime, oldDateTime;
    TimeSpan timeDifference;
    Values items;    
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    private void Awake()
    {
        GetTimestampFromApi();
        GetTimeFromJsonApi();
        CalculateDateTime();
    }
    private void Update()
    {
        if ( whenUpdateInMinutes < (DateTime.Now.Minute - oldDateTime.Minute) )
        {            
            GetTimeFromJsonApi();
        }
        UpdateTimeInGame();
    }
    
    private void UpdateTimeInGame()
    {
        SetDateTime = timeDifference + DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0, 0, hoursToDegrees * (float)SetDateTime.TotalHours);
        secondsPivot.localRotation = Quaternion.Euler(0, 0, secondsToDegrees * (float)SetDateTime.TotalSeconds);
        munitesPivot.localRotation = Quaternion.Euler(0, 0, minutesToDegrees * (float)SetDateTime.TotalMinutes);
        digits.text = SetDateTime.Hours.ToString() + ":" + SetDateTime.Minutes.ToString("0#") + ":" + SetDateTime.Seconds.ToString("0#");
    }

    private void CalculateDateTime()
    {
        DateTime datevalue1 = datetime;
        DateTime datevalue2 = DateTime.Now;
        oldDateTime = datevalue2;
        timeDifference = datevalue1 - datevalue2;
        SetDateTime = timeDifference + DateTime.Now.TimeOfDay;
    }
    private void GetTimestampFromApi()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://time100.ru/api.php");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        TimeSpan info = TimeSpan.FromSeconds(int.Parse(jsonResponse));
        // 'Всемирное координированное время"
        print("TimeSpan from:<color=green> https://time100.ru/api.php </color> : "
               + info.Hours + ": " + info.Minutes + ":" + info.Seconds);
    }

    private void GetTimeFromJsonApi()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://worldtimeapi.org/api/timezone/Europe/London");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        items = JsonUtility.FromJson<Values>(jsonResponse);
        datetime = DateTime.Parse(items.datetime);
        CalculateDateTime();        
        print("Time from:<color=yellow> http://worldtimeapi.org/api/timezone/Europe/London </color>: " + datetime);
       
    }
}

