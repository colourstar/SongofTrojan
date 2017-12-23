using UnityEngine;
using System.Collections;

public class ConditionBase
{
	// Use this for initialization
	public virtual void Init ()
	{

	}

	// Update is called once per frame
	public void Update ()
	{

	}

	// 是否满足条件
	public virtual bool IsSignal()
	{
		return true;
	}
}

