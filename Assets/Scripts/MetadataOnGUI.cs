using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class MetadataOnGUI : MonoBehaviour
{

    private string buffer = "";
    private bool displayHit = false;
    public Text displayText;
    public RectTransform canvas;
    public Vector3 offset;
    public Camera targetCamera;
    bool fullview = false;
    public Collider colly;
    MetaData meta;
    Ray ray;
    // Update is called once per frame
    void Update()
    {

        canvas.rotation = targetCamera.transform.rotation;
        canvas.position = targetCamera.transform.position + (offset - targetCamera.transform.position) / 2;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //uncomment the displayText stuff later
            fullview = !fullview;
            if (fullview)
            {
                GetMetaData(colly.gameObject.name.Split('[')[0]);
                displayText.text = buffer;
            }
            else
            {
                buffer = colly.gameObject.name.Split('[')[0];
                displayText.text = buffer;
            }
        }
        RaycastHit hit;
        ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit,1000))
            {
                displayHit = true;
                Vector3 orto = hit.normal;
                float dist = hit.distance;
                colly = hit.collider;
                //uncomment the offset later as well
                offset = colly.transform.position;

                //only visible in Editor, uncomment only if things start getting buggy
                Debug.DrawLine(ray.origin, hit.point);
                

                //get MetaData component from collided object
                if (!fullview)
                {
                    buffer = colly.gameObject.name.Split('[')[0];
                }
                else
                {
                    GetMetaData(colly.gameObject.name.Split('[')[0]);
                }
            }
            else
            {
                displayHit = false;
            }
            //Debug.Log(buffer);
        }
        
        
        if (displayHit)
        {
            displayText.text = buffer;
        }
    }

    void GetMetaData(string collyName)
    {
        meta = colly.gameObject.GetComponent<MetaData>();
        //check if we actually have a MetaData component
        if (!meta)
        {
            //if no MetaData is attached, it might be attached to the parent
            Transform myParent = colly.gameObject.transform.parent;
            if (myParent != null)
            {
                meta = myParent.GetComponent<MetaData>();
                collyName = myParent.name;
            }

            buffer = colly.name + " has no metadata uploaded.";

        }
        //do something with the MetaData
        else
        {
            buffer = collyName + "\n";
            for (int i = 0; i < meta.keys.Length; i++)
            {
                Debug.Log("Keys: " + meta.keys.Length);
                if (meta.keys[i].Length != 0)
                {
                    if (meta.values[i].Length != 0)
                    {
                        Debug.Log(meta.keys[i]);
                        buffer += meta.keys[i] + " :   " + meta.values[i] + "\n";
                    }

                }
            }
        }
    }

    
    void OnGUI()
    {
        // Display Popup only when we hit an object
        ///*
        {
            // POPUP configuration
            float popupWidth = 400; // width of our popup dialog
            float padding = 10; // some space
            float paddingBottom = 60; // distance from bottom of screen

            float xpos = Screen.width - popupWidth - padding;
            float w = popupWidth;
            float h = Screen.height/4 + buffer.Split('\n').Length*20 - padding * 2 - paddingBottom;

            GUI.BeginGroup(new Rect(xpos, padding, w, h));
            GUI.Box(new Rect(0, 0, w, h), "Object Info");
            GUI.Label(new Rect(padding, 20 + padding, w - padding * 2, h - padding * 2 - 20), buffer);
            GUI.EndGroup();
        }
         //*/
    }
    

}