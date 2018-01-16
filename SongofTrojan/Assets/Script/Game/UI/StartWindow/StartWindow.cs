using UnityEngine;
using System.Collections;

public class StartWindow : UIWindowBase 
{
    private float m_iCurrentTime = 0.0f;
    //UI的初始化请放在这里
    public override void OnOpen()
    {
        StartCoroutine(TextAnim(GetGameObject("Text"), 1, 0));
    }

    //请在这里写UI的更新逻辑，当该UI监听的事件触发时，该函数会被调用
    public override void OnRefresh()
    {

    }

    public void Update()
    {
        m_iCurrentTime += Time.deltaTime;
        float fAlpha = (m_iCurrentTime % 3.0f) / 3.0f;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            OnStart();
        }
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
        
	// 按键消息接受
	public void OnStart()
	{
		ApplicationStatusManager.GetStatus<StartStatus>().CloseUI<StartWindow>();
		ApplicationStatusManager.EnterStatus<GameStatus>();
	}

    // 动态效果
    public IEnumerator TextAnim(GameObject gameobject,float fromvalue,float tovalue)
    {
        AnimSystem.UguiAlpha(gameobject, fromvalue, tovalue, callBack:(object[] obj)=>
            {
                StartCoroutine(TextAnim(gameobject, tovalue, fromvalue));
            });

        yield return new WaitForEndOfFrame();
    }
}