using System;
using UnityEngine;

public class UserStudyData:MonoBehaviour
{
	protected string fileName;
	protected string stringFormat = "0.#####";
	protected char[] ignoreChar = {'(', ')'};
	protected string dataText;
	protected int runNumber;
	protected float originalRodRotation;
	protected float originalFrameRotation;
	public int FlushIntervalInFrameCount;

	public string FileName {get { return fileName; }}
	public UserStudyData ()
	{
	}
	public bool IsRecorded;
	public bool IsContinuous;
	public virtual void UpdateSettings(int run, float rodRotation, float frameRotation)
	{
		runNumber = run;
		originalRodRotation = rodRotation;
		originalFrameRotation = frameRotation;

	}
	public override string ToString()
	{
		dataText = runNumber.ToString () + " ";
		dataText += Time.realtimeSinceStartup.ToString () + " ";
		dataText+= originalFrameRotation.ToString(stringFormat) + " ";
		dataText += originalRodRotation.ToString (stringFormat) + " ";
		return dataText;
	}
}


