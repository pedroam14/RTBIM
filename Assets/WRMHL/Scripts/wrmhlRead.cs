using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
This script is used to read all the data coming from the device. For instance,
If arduino send ->
								{"1",
								"2",
								"3",}
readQueue() will return ->
								"1", for the first call
								"2", for the second call
								"3", for the thirst call

This is the perfect script for integration that need to avoid data loose.
If you need speed and low latency take a look to wrmhlReadLatest.
*/

public class wrmhlRead : MonoBehaviour
{

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

    [Tooltip("SerialPort of your device.")]
    public string portName = "COM3";

    [Tooltip("Baudrate")]
    public int baudRate = 50000;


    [Tooltip("Timeout")]
    public int ReadTimeout = 20;

    [Tooltip("QueueLenght")]
    public int QueueLenght = 1;
    public bool connected = false;
    string[] sensorInput;
    uint sensorIndex, sensorReading;
    SensorScript sensors;
    public void Connect()
    {
        portName = GameObject.FindGameObjectWithTag("COMSelect").GetComponent<InputField>().text;
        GameObject.FindGameObjectWithTag("COMSelect").gameObject.SetActive(false);
        GameObject.Find("Button").gameObject.SetActive(false);
        Debug.Log(portName);
        sensors = this.GetComponent<SensorScript>();
        myDevice.set(portName, baudRate, ReadTimeout, QueueLenght); //this method set the communication with the following vars; Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); //this method open the Serial communication with the vars previously given.
        connected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            sensorInput = myDevice.readQueue().Split(','); // myDevice.read() return the data coming from the device using thread.
            sensorIndex = uint.Parse(sensorInput[0]);
            sensorReading = uint.Parse(sensorInput[1]);

            sensors.index[sensorIndex - 1] = sensorIndex;
            sensors.sensorReading[sensorIndex - 1] = sensorReading;
        }
    }

    void OnApplicationQuit()
    { // close the Thread and Serial Port
        myDevice.close();
    }

}
