/// <summary>
/// Logic main.
/// 消息管理模块
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class MessageManager
{
    public enum MessageType
    {
        MT_Null = 0,
        MT_DialogClick = 1,
    }

    public struct Message
    {
        public MessageType eType;
        public Dictionary<string,string> mgparams;
    }

    public bool             m_isBegin = false;
    public List<Message>    m_MessageList = new List<Message>();

    public void AddMessage(Message kMsg)
    {
        m_MessageList.Add(kMsg);
    }

    public void Update()
    {
        if (m_isBegin == false)
        {
            return;
        }
        for (int i = 0; i < m_MessageList.Count; ++i)
        {
            _ProcessMsg(m_MessageList[i]);
        }

        m_MessageList.Clear();
    }

    private void _ProcessMsg(Message kMsg)
    {
        if (kMsg.eType == MessageType.MT_DialogClick)
        {
            Story curstory = LogicMain.m_StoryManager.GetCurrentStory();
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

            kActionDialog.OnDialogEnd();
        }
    }
}