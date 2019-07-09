using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;


public class ScheduleParser : MonoBehaviour
{
    private string fileToParse = "";
    // some public variables, to configure this script
    public string filePath = "/MetaData/";
    public string fileName = "Metadata";
    public string fileExtension = "csv";
    public int headersLineNumber = 0;
    public int valuesFromLine = 1;
    string[] headers;
    void Start()
    {
        Debug.Log(Application.dataPath);
        fileToParse = Application.dataPath + filePath + fileName + "." + fileExtension;
        Debug.Log(fileToParse);
        FileInfo theSourceFile = null;
        TextReader reader = null;  // NOTE: TextReader, superclass of StreamReader and StringReader
        theSourceFile = new FileInfo(fileToParse);
        if (theSourceFile.Exists)
        {
            reader = theSourceFile.OpenText();  // returns StreamReader
            Debug.Log("Created Stream Reader for " + fileToParse + " (in Datapath)");
            // Read each line from the file/resource
            bool goOn = true;
            int lineCounter = 0;
            while (goOn)
            {
                string buf = reader.ReadLine();
                if (buf == null)
                {
                    goOn = false;
                    return;
                }
                else
                {
                    string[] values;
                    if (lineCounter == headersLineNumber)
                    {
                        //headers = this.SplitCsvLine(buf);
                        headers = buf.Split(';');
                        Debug.Log("Found header " + headers[0]);
                    }
                    if (lineCounter >= valuesFromLine)
                    {
                        //, ; or -delimited string with data
                        //getting the ID and other values here
                        //values = this.SplitCsvLine(buf);
                        values = buf.Split(';');
                        if (values[0].Length == 0)
                        {
                            Debug.Log("Empty ID!");
                        }
                        else
                        {
                            //handle the actually significant stuff
                            Debug.Log("Found values " + int.Parse(values[0]));
                            GameObject gameObject = null;
                            if (gameObject == null)
                            {
                                bool found = false;
                                foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
                                {
                                    if (gameObj.name.Contains(values[0]))
                                    {
                                        gameObject = gameObj;
                                        Debug.Log("Found ID : " + values[0]);
                                        gameObject.AddComponent<MetaData>();
                                        MetaData meta = gameObject.GetComponent<MetaData>();
                                        meta.values = values;
                                        meta.keys = headers;
                                        found = true;
                                    }
                                }
                                if (!found)
                                {
                                    Debug.Log("Could not find object with ID: " + values[0]);
                                }
                            }
                        }
                    }
                    //Debug.Log("Current Line : " + lineCounter + " : " + buf);

                    lineCounter++;
                }
            }
        }
        else
        {
            Debug.Log("File at: " + fileToParse + " not found!");
        }
    }
    private string[] SplitCsvLine(string line)
    {
        string pattern = @"""";

        string[] values = (from Match m in Regex.Matches(line, pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) select m.Groups[1].Value).ToArray();

        return values;
    }
}


