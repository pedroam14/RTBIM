using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public GameObject selectedObject;
    public MetadataOnGUI metadata;
    public Color[] bufferedColor;
    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    public bool blueflash = false;

    private void Update()
    {
        if (metadata.colly)
        {
            if (!metadata.colly.Equals(selectedObject))
            {
                if (selectedObject && selectedObject.GetComponent<Renderer>() != null)
                {
                    for (int i = 0; i < bufferedColor.Length; i++)
                    {
                        selectedObject.GetComponent<Renderer>().materials[i].color = bufferedColor[i];
                    }
                }
                selectedObject = metadata.colly.gameObject;
            }
        }
        if (selectedObject && selectedObject.GetComponent<Renderer>() != null)
        {
            bufferedColor = new Color[selectedObject.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < bufferedColor.Length; i++)
            {
                bufferedColor[i] = selectedObject.GetComponent<Renderer>().materials[i].color;
                selectedObject.GetComponent<Renderer>().materials[i].color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
            }
        }
        if (!startedFlashing)
        {
            startedFlashing = !startedFlashing;
            StartCoroutine(FlashObject());
        }
    }

    public void SetBlue()
    {
        blueflash = !blueflash;
    }

    IEnumerator FlashObject()
    {
        while (lookingAtObject)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn)
            {
                if (!blueflash)
                {
                    if (redCol <= 30)
                    {
                        flashingIn = false;
                    }
                    else
                    {
                        redCol -= 25;
                        greenCol -= 1;
                    }
                }
                else
                {
                    if (blueCol <= 30)
                    {
                        flashingIn = false;
                    }
                    else
                    {
                        blueCol -= 25;
                        greenCol -= 1;
                    }
                }
            }
            if (!flashingIn)
            {
                if (!blueflash)
                {
                    if (redCol >= 250)
                    {
                        flashingIn = true;
                    }
                    else
                    {
                        redCol += 25;
                        greenCol += 1;
                    }
                }
                else
                {
                    if (blueCol >= 250)
                    {
                        flashingIn = true;
                    }
                    else
                    {
                        blueCol += 25;
                        greenCol += 1;
                    }
                }
            }
        }
    }
}