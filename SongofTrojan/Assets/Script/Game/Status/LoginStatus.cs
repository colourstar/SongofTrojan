using UnityEngine;
using System.Collections;


public class LoginStatus : IApplicationStatus
{
	public override void OnEnterStatus()
	{
		OpenUI<LoginWindow> ();
	}
}

