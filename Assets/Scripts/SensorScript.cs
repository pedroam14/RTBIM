using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SensorScript : MonoBehaviour
{
    public uint[] sensorReading;
    public uint[] index;
    public Text text;
    public bool alarm;
    public float threshold;
    public MeshRenderer[] colors;
    private void Awake()
    {
        sensorReading = new uint[2];
        index = new uint[2];
        text = this.GetComponentInChildren<Text>();
        colors = this.GetComponentsInChildren<MeshRenderer>();
    }
    private void Update()
    {
        text.text = "Sensor 1: " + sensorReading[0] + "ppm" + " \nSensor 2: " + sensorReading[1] + "ppm";
        colors[0].materials[0].color = Color.Lerp(Color.green, Color.red, (float)sensorReading[1] / threshold);
        colors[1].materials[0].color = Color.Lerp(Color.green, Color.red, (float)sensorReading[1] / threshold);
        if (sensorReading[0] > threshold)
        {

            alarm = true;
            text.text += "\nLeak detected on Sensor 1!";

        }
        if (sensorReading[1] > threshold)
        {

            alarm = true;
            text.text += "\nLeak detected on Sensor 2!";

        }
    }
}
