using System;
using UnityEngine;
public class AnswerData:UserStudyData
{
	
	bool _answer;
	public AnswerData ()
	{
		fileName = "answer.txt";
	}
	public void UpdateSettings(int run)
	{


	}
	void OnEnable()
	{		
		IsContinuous = false;
		FlushIntervalInFrameCount = 1;
	}
	public override string ToString ()
	{
		dataText = base.ToString ();
		dataText += System.Convert.ToInt64 (_answer).ToString ();
		return dataText;
	}
}


