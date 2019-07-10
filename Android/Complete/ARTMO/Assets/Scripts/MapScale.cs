using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScale : MonoBehaviour {
	  RectTransform rectTransform;
     public float zoomSpeed = 0.5f;
	 int c=0;
	 int tapCount=0;
	 float doubleTapTimer=0f; 
    void Update()
    {
        //  if (Input.touchCount == 2)
        //  {
        //      // Store both touches.
        //      Touch touchZero = Input.GetTouch(0);
        //      Touch touchOne = Input.GetTouch(1);
 
        //      // Find the position in the previous frame of each touch.
        //      Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //      Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
 
        //      // Find the magnitude of the vector (the distance) between the touches in each frame.
        //      float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //      float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
 
        //      // Find the difference in the distances between each frame.
        //      float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			 
        //     //  ... change the canvas size based on the change in distance between the touches.
        //     //  canvas.scaleFactor -= deltaMagnitudeDiff * zoomSpeed;
			
        //     //  // Make sure the canvas size never drops below 0.1
        //     //  canvas.scaleFactor = Mathf.Max(canvas.scaleFactor, 0.1f);
		// 	if(deltaMagnitudeDiff>0){
		// 	transform.localScale += new Vector3(0.05F, 0.05F, 0);	
		// 	}
		// 	else{
		// 		transform.localScale -= new Vector3(0.05F, 0.05F, 0);
		// 	}
			
        //  }
		 if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
         {
             tapCount++;
         }
         if (tapCount > 0)
         {
             doubleTapTimer += Time.deltaTime;
         }
         if (tapCount >= 2)
         {
             //What you want to do
             doubleTapTimer = 0.0f;
             tapCount = 0;
			 	if(c%2==0){
				transform.localScale += new Vector3(0.1F, 0.1F, 0);	
			}else{
				transform.localScale -= new Vector3(0.1F, 0.1F, 0);	
			}
			c++;
         }
         if (doubleTapTimer > 0.5f)
         {
             doubleTapTimer = 0f;
             tapCount = 0;
         }
    }
}
