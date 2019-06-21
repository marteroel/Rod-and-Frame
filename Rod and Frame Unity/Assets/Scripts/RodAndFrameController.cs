using UnityEngine;
using System;
using System.Collections;

class RodAndFrameSetting{
	float rodAngle;
	float frameAngle;
	public RodAndFrameSetting(float rAngle, float fAngle)
	{
		rodAngle = rAngle;
		frameAngle = fAngle;
	}
	public float RodAngle{
		get {return rodAngle;}
	}
	public float FrameAngle{
		get {return frameAngle;}
	}
	public override string ToString ()
	{
		return "Rod angle: " + rodAngle.ToString () + "; " + "Frame angle: " + frameAngle.ToString ();
	}
}
public static class MyExtension{
	private static System.Random rng = new System.Random();  

	public static void Shuffle<T>(this System.Collections.Generic.List<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}
public class RodAndFrameController : MonoBehaviour {
	public GameObject Rod;
	public GameObject Frame;
	//public EyeTrackerData eyetrackerData;
	public RodAndFrameData rodandframeData;

	public float Delta = 1f;
	bool isKeyboardActive = false;
	bool isStopConditionReached = false;
	bool isConfirmed = false;
	bool isETInitialized = false;

	float frameAngle, rodAngle, userAngle, deviationAngle;

	int count;
	int setCount = 0;
	int numberOfSets = 5;
	int runNumber;
	int counter = 0;

	//DataLogger dataLogger;
	UIFader panelFader;

	System.Collections.Generic.List<RodAndFrameSetting> rodandframeSettingList= new System.Collections.Generic.List<RodAndFrameSetting>();

	// Use this for initialization
	void Awake () {
//		var canvas = GameObject.FindObjectOfType<Canvas> ();
//		if (canvas!=null)
//		{
//			panelFader = canvas.GetComponent<UIFader> ();
//		}
		panelFader = GameObject.FindObjectOfType<UIFader> ();
		//dataLogger = GetComponent<DataLogger>();

		//if (dataLogger != null) {
			//if (rodandframeData != null)
				//dataLogger.StudyData.Add (rodandframeData);
		//}
		rodandframeSettingList.Add (new RodAndFrameSetting (20f, 18f));
		rodandframeSettingList.Add (new RodAndFrameSetting (340f, 18f));
		rodandframeSettingList.Add (new RodAndFrameSetting (20f, -18f));
		rodandframeSettingList.Add (new RodAndFrameSetting (340f, -18f));
		rodandframeSettingList.Shuffle ();

		StartCoroutine (StartGame ());
	}

	private IEnumerator WaitForConfirmation()
	{
		while (!isConfirmed) {
			yield return null;

		}
		isConfirmed = false;
	}
	private IEnumerator StartGame()
	{
		while(!isStopConditionReached)
		{
			// FadePanel in
 			yield return StartCoroutine(panelFader.FadeIn());
			// Wait for ET to be initialized
			
			Debug.Log ("ET Initialized");
			// Set rod and frame angles
			Debug.Log(rodandframeSettingList[count].ToString());
			frameAngle = rodandframeSettingList[count].FrameAngle;
			rodAngle = rodandframeSettingList[count++].RodAngle;
			Rod.transform.localRotation = Quaternion.Euler (0f, 0f, rodAngle);
			Frame.transform.localRotation = Quaternion.Euler (0f, 0f, frameAngle);

			// FadePanel out
			yield return StartCoroutine(panelFader.FadeOut());

			// Start recording Eyetracker, start recording rod and frame position
			runNumber+=1;

			rodandframeData.UpdateSettings (runNumber, rodAngle, frameAngle, true);
			rodandframeData.IsRecorded = true;

			// Set keyboard Active
			isKeyboardActive = true;
			// wait for confirm key to be pressed
			yield return StartCoroutine(WaitForConfirmation());

			rodandframeData.IsRecorded = false;

			// Set keyboard inactive
			isKeyboardActive = false;

			// Record current angle
			rodandframeData.UpdateSettings (runNumber, rodAngle, frameAngle, false);
			rodandframeData.IsRecorded = true;
//			userAngle = Rod.transform.rotation.eulerAngles.z;
//			deviationAngle = userAngle - rodAngle;
			// Write angle to files

			// Check stopping condition 
			if (count == rodandframeSettingList.Count) {
				count = 0;
				setCount += 1;
				rodandframeSettingList.Shuffle ();			
			}

			if (setCount == numberOfSets) {
				isStopConditionReached = true;

			}
		}
		yield return null;
		Application.Quit ();

	}
	void Update () {
		if (isKeyboardActive) {
			var z = Rod.transform.localRotation.eulerAngles.z;
			// Keyboard
			if (Input.GetKey ("left")) {
				
				Rod.transform.localRotation = Quaternion.Euler (Rod.transform.localRotation.x, Rod.transform.localRotation.y, z + Delta);
			}
			if (Input.GetKey ("right")) {
				Rod.transform.localRotation = Quaternion.Euler (Rod.transform.localRotation.x, Rod.transform.localRotation.y, z - Delta);
			}
            if (Input.GetKey("a"))
            {

                Rod.transform.localRotation = Quaternion.Euler(Rod.transform.localRotation.x, Rod.transform.localRotation.y, z + 0.1f * Delta);
            }
            if (Input.GetKey("d"))
            {
                Rod.transform.localRotation = Quaternion.Euler(Rod.transform.localRotation.x, Rod.transform.localRotation.y, z -0.1f* Delta);
            }
            if (Input.GetKeyDown("space"))
			{
				isConfirmed = true;	
			}
			// Joystick


			Rod.transform.localRotation = Quaternion.Euler (Rod.transform.localRotation.x, Rod.transform.localRotation.y, Rod.transform.localRotation.eulerAngles.z - Delta*Input.GetAxis("Horizontal"));
 
            /*
			if (Input.GetButtonDown("AButton"))
			{
				isConfirmed = true;	
			}*/
		}

	}

}
