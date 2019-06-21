using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using System.IO;
public class DataLogger : MonoBehaviour {
	
	string currentDirectory;
	string userName;

	char[] ignoreChar = {'(', ')'};


	public System.Collections.Generic.List<UserStudyData> StudyData;
	public int FlushIntervalInFrameCount = 3600;
	System.Collections.Generic.List<StreamWriter> streamWriterList = new System.Collections.Generic.List<StreamWriter>() ;
	int[] frameCount ;
	void Start()
	{
		// create data directory for each user
		userName = PlayerPrefs.GetString ("Username");
		var rootDirectory = System.IO.Directory.GetCurrentDirectory(); 
		var dataDirectory = System.IO.Path.Combine (System.IO.Directory.GetParent (rootDirectory).ToString (), "rodandframe_data");
		currentDirectory = System.IO.Path.Combine (dataDirectory, userName);
		System.IO.Directory.CreateDirectory (currentDirectory);
		Debug.Log (currentDirectory);
		if (StudyData != null) {
			frameCount = new int[StudyData.Count];
            Debug.Log("NUMBER OF DATA =" + StudyData.Count.ToString());
			for (int i = 0; i < StudyData.Count; i++) {
                Debug.Log("FILE NAME =  " + StudyData[i].ToString());
				string str = System.IO.Path.Combine (currentDirectory, StudyData [i].FileName);
				streamWriterList.Add (new StreamWriter (str, false));
			}
		}
	}
	void OnDestroy()
	{
		for (int i = 0; i < StudyData.Count; i++) {
			if (streamWriterList [i] != null) {
				streamWriterList [i].Flush ();
				streamWriterList [i].Close ();
			}
			
		}
	}
	void Update () {
		for (int i = 0; i < StudyData.Count; i++) {
			if (StudyData [i].IsRecorded) {
				streamWriterList [i].WriteLine (StudyData [i].ToString ());
				frameCount [i] += 1;
				if (frameCount [i] == FlushIntervalInFrameCount) {
					streamWriterList [i].Flush ();
					frameCount [i] = 0;
				}
				if (StudyData [i].IsContinuous == false) {
					StudyData [i].IsRecorded = false;
				
				}

			} else {
				if (frameCount[i]>0) {
					streamWriterList [i].Flush ();
					frameCount [i] = 0;
//					streamWriterList [i].Close ();
				}
			}
		}

	}
}
