using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
       if(other.tag == "Test Tag"){
           Debug.Log("Camera Entered");
       }
   }
   private void OnTriggerExit(Collider other) {
       if(other.tag == "Test Tag"){
           Debug.Log("Camera exit");
       }
   }
   private void OnTriggerStay(Collider other) {
       if(other.tag == "Test Tag")
        {
            Debug.Log("Camera stay");
        }
    }
}
