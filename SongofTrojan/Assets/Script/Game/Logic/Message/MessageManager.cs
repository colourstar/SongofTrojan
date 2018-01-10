/// <summary>
/// Logic main.
/// 消息管理模块
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MessageManager : IModuleBase
{
    public MessageManager() : base("MessageManager"){}

    public enum MessageType
    {
        MT_Null = 0,
        MT_DialogClick = 1,
    }

    public struct Message
    {
		Message(MessageType messagetype,Dictionary<string, string> msgparams)
		{
			m_eType = messagetype;
            m_dicParams = msgparams;
		}
        public MessageType m_eType;
        public Dictionary<string,string> m_dicParams;
    }

    public List<Message>    m_arrMessageList = new List<Message>();


    public override void Update()
    {
        if (m_bIsBegin == false)
        {
            return;
        }
        for (int i = 0; i < m_arrMessageList.Count; ++i)
        {
            _ProcessMsg(m_arrMessageList[i]);
        }

        m_arrMessageList.Clear();
    }

	public void AddMessage(Message kMsg)
	{
		m_arrMessageList.Add(kMsg);
	}

    private void _ProcessMsg(Message kMsg)
    {
		if (kMsg.m_eType == MessageType.MT_DialogClick)
        {
            StoryManager kStoryManager = ApplicationStatusManager.GetStatus<GameStatus>().GetLogicMain().GetModule("StoryManager") as StoryManager;
            Story curstory = kStoryManager.GetCurrentStory();
            if (curstory == null)
            {
                Debug.LogError("[MessageManager] : MessageType.MT_DialogClick Error,Story is null");
                return;
            }
            Action_Dialog kActionDialog = curstory.GetCurrentAction() as Action_Dialog;
            if (kActionDialog == null)
            {
                Debug.LogError("[MessageManager] : MessageType.MT_DialogClick Error,Story is null");
                return;
            }

            // 需要判断story的下一个是否仍旧是dialog类型
            kActionDialog.OnDialogEnd();
        }
    }
}