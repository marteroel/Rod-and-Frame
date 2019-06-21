using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

namespace RodAndFrame { 
    public class CenterView : MonoBehaviour {

	    // Use this for initialization
	    void Start () {
		
		    InputTracking.disablePositionalTracking = true;

		    //Debug.Log (SceneManager.GetActiveScene ().buildIndex);

		    //if(SceneManager.GetActiveScene().buildIndex == 0)
		    //	UnityEngine.XR.InputTracking.Recenter();
	    }
	
	    // Update is called once per frame
	    void Update () {

		    if (Input.GetKeyDown ("c")) {
		    //	transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			    UnityEngine.XR.InputTracking.Recenter ();
		    }
		
	    }
    }
}
