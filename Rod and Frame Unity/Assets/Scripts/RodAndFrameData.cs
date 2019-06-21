using UnityEngine;
using System.Collections;

public class RodAndFrameData : UserStudyData {
	public GameObject Rod;
	public GameObject Frame;
	// Use this for initialization
	void OnEnable()
	{
		fileName = gameObject.name + ".txt";
		IsContinuous = true;
	}

	public void UpdateSettings(int run, float rodRotation, float frameRotation, bool isContinuous)
	{
		base.UpdateSettings (run, rodRotation, frameRotation);
		IsContinuous = isContinuous;

	}
	public override string ToString()
	{
		dataText = base.ToString ();
		// originalframerotation originalrodrotation currentrodrotation 
		dataText += Rod.transform.rotation.eulerAngles.z.ToString(stringFormat);
		return dataText;
	}
}
