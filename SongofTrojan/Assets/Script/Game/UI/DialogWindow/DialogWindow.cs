using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogWindow : UIWindowBase 
{

    //UI的初始化请放在这里
    public override void OnOpen()
    {
        AddOnClickListener ("btRecvClick", OnRecvClick);
    }

    //请在这里写UI的更新逻辑，当该UI监听的事件触发时，该函数会被调用
    public override void OnRefresh()
    {

    }

    //UI的进入动画
//    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
//    {
//        AnimSystem.UguiAlpha(gameObject, 0, 1, callBack:(object[] obj)=>
//        {
//            StartCoroutine(base.EnterAnim(l_animComplete, l_callBack, objs));
//        });
//
//        yield return new WaitForEndOfFrame();
//    }

    //UI的退出动画
    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
//        AnimSystem.UguiAlpha(gameObject , null, 0, callBack:(object[] obj) =>
//        {
//            StartCoroutine(base.ExitAnim(l_animComplete, l_callBack, objs));
//        });

        yield return new WaitForEndOfFrame();
    }

    // 窗口刷新消息
    public void OnRefreshContent(string content,int iRoleID)
    {
		table.Config kConfig = TabtoyConfigManager.GetConfig();
		table.RoleDefine roleconfig = kConfig.GetRoleByID (iRoleID);
		GetText ("Text").text = content;
		GetImage ("roleback").sprite = Resources.Load (roleconfig.NormalDrawing,typeof(Sprite)) as Sprite;
    }

    // 按钮消息接受
    public void OnRecvClick(InputUIOnClickEvent e)
    {
		MessageManager.Message kMsg = new MessageManager.Message ();
		kMsg.m_eType = MessageManager.MessageType.MT_DialogClick;
		kMsg.m_params = new Dictionary<string,string> ();
		LogicMain.m_MessageManager.AddMessage (kMsg);
        Debug.Log("Dialog Window On Clicked");
    }
}