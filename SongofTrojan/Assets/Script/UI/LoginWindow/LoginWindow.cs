using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LoginWindow : UIWindowBase 
{

    //UI的初始化请放在这里
	public override void OnOpen()
    {
		Dictionary<string,object> akConfigData = TabtoyConfigManager.GetData ("Config") as Dictionary<string,object>;
		Debug.Log (akConfigData ["Tool"]);
		List<object> testlist = akConfigData ["Item"] as List<object>;
		Dictionary<string,object> testdic = testlist[0] as Dictionary<string,object>;
		Debug.Log (testdic["ID"]);

		AddOnClickListener ("btlogin", OnLoginClicked);
    }

    //请在这里写UI的更新逻辑，当该UI监听的事件触发时，该函数会被调用
    public override void OnRefresh()
    {

    }

    //UI的进入动画
    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject, 0, 1, callBack:(object[] obj)=>
        {
            StartCoroutine(base.EnterAnim(l_animComplete, l_callBack, objs));
        });

        yield return new WaitForEndOfFrame();
    }

    //UI的退出动画
    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject , null, 0, callBack:(object[] obj) =>
        {
            StartCoroutine(base.ExitAnim(l_animComplete, l_callBack, objs));
        });

        yield return new WaitForEndOfFrame();
    }
		
	public void OnLoginClicked(IInputEventBase e)
	{
		Debug.Log ("OnLoginClicked");
	}
}