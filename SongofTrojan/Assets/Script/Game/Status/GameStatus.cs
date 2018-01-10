/// <summary>
/// Game status.
/// 游戏正常运行流程,会进行LogicMain的启动与刷新
/// </summary>


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameStatus : IApplicationStatus
{
    protected UIWindowBase m_kCurMapScene = null;       // 普通地图界面
    protected UIWindowBase m_kBigMapScene = null;       // 大地图界面
    public    LogicMain    m_kLogicMain = null;         // 主逻辑

	public override void OnEnterStatus()
	{
        m_kLogicMain = new LogicMain();
		// 初始化Logic的配置
        m_kLogicMain.Init ();

        // 开始逻辑
        m_kLogicMain.Start ();
	}

	public override void OnUpdate()
	{
        m_kLogicMain.Update ();
	}


    /// <summary>
    /// 开启对话
    /// </summary>
    public void OnDialog(string content,int iroleid)
    {
        // 判断对话窗口是否开启
        DialogWindow uiDialog = UIManager.GetUI<DialogWindow>();
        if (uiDialog == null)
        {
            uiDialog = OpenUI<DialogWindow>();
        }
        if (uiDialog.IsShow() == false)
        {
            uiDialog.Show();
        }

        uiDialog.OnRefreshContent(content, iroleid);
    }

    /// <summary>
    /// 对话结束
    /// </summary>
    public void OnDialogEnd(bool isneeddel)
    {
        // 判断对话窗口是否开启
        DialogWindow uiDialog = UIManager.GetUI<DialogWindow>();
        if (uiDialog == null)
        {
            return;
        }
        if (uiDialog.IsShow() == true)
        {
            uiDialog.Hide();
        }
    }

    /// <summary>
    /// 进入地图
    /// </summary>
    public void ChangeMapScene(int iFromMapID,int iToMapID)
    {
        ApplicationManager.Instance.StartCoroutine(_ChangeSceneAnim(iFromMapID,iToMapID));
    }

    protected IEnumerator _ChangeSceneAnim(int iFromMapID,int iToMapID)
    {
        table.MapDefine kFromMap = TabtoyConfigManager.GetConfig().GetMapByID(iFromMapID);
        table.MapDefine kToMap = TabtoyConfigManager.GetConfig().GetMapByID(iToMapID);
        if (kToMap == null)
        {
            Debug.LogError("[Map] : Error,map is not exist");
            yield break;
        }

        // 首先播放场景结束动画
        if (kFromMap != null)
        {
            if (kFromMap.MapType == "大地图")
            {
                
            }
            else if (kFromMap.MapType == "城市")
            {
            }
        }

        // 然后播放场景加载动画
        if (kToMap.MapType == "大地图")
        {
            
        }
        else if (kToMap.MapType == "城市")
        {
            
        }
    }

    /// <summary>
    /// 保存游戏
    /// </summary>
    public void SaveGame(string savefilename)
    {
        FileStream kSaveStream = new FileStream(PathTool.GetAbsolutePath(ResLoadLocation.Persistent,"/" + savefilename),FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
        BinaryFormatter kFormater = new BinaryFormatter();
        kFormater.Serialize(kSaveStream, m_kLogicMain);
        kSaveStream.Close();
    }

    /// <summary>
    /// 重新加载游戏
    /// </summary>
    public void LoadGame(string loadfilename)
    {
        BinaryFormatter kFormater = new BinaryFormatter();
        FileStream kReadStream = new FileStream(PathTool.GetAbsolutePath(ResLoadLocation.Persistent,"/" + loadfilename), FileMode.Open , FileAccess.Read ,FileShare.Read );
        m_kLogicMain = kFormater.Deserialize(kReadStream) as LogicMain;
        kReadStream.Close();
    }


    public LogicMain GetLogicMain()
    {
        return m_kLogicMain;
    }
}