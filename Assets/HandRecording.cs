using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HandRecording : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject ButtonModel;
    private int numOfDataPoints;
    private int maxNumDataPoints;
    private Vec3Ser[,] dataPoints;
    //private float[,] dataPoints;
    bool hasExported;

    // Start is called before the first frame update
    void Start()
    {
        numOfDataPoints = 0;
        maxNumDataPoints = 3500;
        dataPoints = new Vec3Ser[2, maxNumDataPoints];
        hasExported = false;
        //Was here for testing, can work
        //StartCoroutine(Upload());
    }

    // Update is called once per frame
    void Update()
    {
      if(numOfDataPoints < maxNumDataPoints)
      {
        //Get position of hands & button and save data points into array
        float buttonPos = ButtonModel.transform.position.y;

        dataPoints[0,numOfDataPoints] = new Vec3Ser(LeftHand.transform.position, buttonPos);
        dataPoints[1,numOfDataPoints] = new Vec3Ser(RightHand.transform.position, buttonPos);

        //dataPoints[0,numOfDataPoints] = new Vec3Ser(LeftHand.transform.position);
        //dataPoints[1,numOfDataPoints] = new Vec3Ser(RightHand.transform.position);

        //dataPoints[0,0,numOfDataPoints] = LeftHand.transform.position.x;
        //dataPoints[0,1,numOfDataPoints] = LeftHand.transform.position.x;
        //dataPoints[0,2,numOfDataPoints] = LeftHand.transform.position.x;
        //dataPoints[1,0,numOfDataPoints] = RightHand.transform.position.x;
        //dataPoints[1,1,numOfDataPoints] = RightHand.transform.position.x;
        //dataPoints[1,2,numOfDataPoints] = RightHand.transform.position.x;

        numOfDataPoints++;
      }
      else if(hasExported == false)
      {
        // Stop recording
        hasExported = true;
        StartCoroutine(Upload());
      }
    }

    IEnumerator Upload()
    {
      // Save data points to JSON file
      string filePath = Application.dataPath + "dataPoints.json";
      string jsonData = JsonConvert.SerializeObject(new SerializableDataPoints(dataPoints));
      //string jsonData = JsonConvert.SerializeObject(new int[] {1,2,3,4,5});
      //File.WriteAllText(filePath, jsonData);

      //Post data
      string url = "http://sweng10.csproject.org/api/sessions";

      //UnityWebRequest www = UnityWebRequest.Put(url, "[1,2,3,4,5]");
      UnityWebRequest www = UnityWebRequest.Put(url, jsonData);

      www.SetRequestHeader("Content-Type", "application/json");
      yield return www.SendWebRequest();
    }


    //Data class for converting Vector3->float
    [System.Serializable]
    class Vec3Ser
    {
      public float x, y, z, buttonPos;
      public Vec3Ser(Vector3 v, float buttonPos)
      {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.buttonPos = buttonPos;
      }
    }

    // Data class for serialization
    [System.Serializable]
    private class SerializableDataPoints
    {
      public Vec3Ser[,] dataPoints;

      public SerializableDataPoints(Vec3Ser[,] dataPoints)
      {
        this.dataPoints = dataPoints;
      }
    }
}
