using UnityEngine;
using System.Collections;


public class StartStatus : IApplicationStatus
{
	public override void OnEnterStatus()
	{
		OpenUI<StartWindow> ();
	}
}

