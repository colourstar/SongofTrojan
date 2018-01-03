/// <summary>
/// Logic main.
/// 地图管理器
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapManager : IModuleBase
{
    public MapManager() : base("MapManager"){}

    public struct Map
    {
        public int          iMapID;
        public List<int>    arrChildMap;
    }

    protected Dictionary<string,Map>  m_dicMaps = new Dictionary<string,Map>();
    protected int                     m_iCurMapID = 0;

    public override void Init()
    {
        table.Config kConfig = TabtoyConfigManager.GetConfig();
        for (int i = 0; i < kConfig.Map.Count; ++i)
        {
            table.MapDefine kMapDefine = kConfig.Map[i];
            if (m_dicMaps.ContainsKey(kMapDefine.Name))
            {
                Debug.LogError("[Map] : Load Error,Map Repeated->" + kMapDefine.Name);
                continue;
            }
            Map kNewMap = new Map();
            kNewMap.iMapID = kMapDefine.ID;
            kNewMap.arrChildMap = new List<int>();
            string[] strSplitchildmap = kMapDefine.ChildMap.Split('|');
            for (int j = 0; j < strSplitchildmap.Length; ++j)
            {
                string kChildMapName = strSplitchildmap[j];
                table.MapDefine kChildMapDefine = kConfig.GetMapByName(kChildMapName);
                if (kChildMapDefine != null)
                {
                    kNewMap.arrChildMap.Add(kChildMapDefine.ID);
                }
            }

            m_dicMaps.Add(kMapDefine.Name, kNewMap);
        }
    }

    public override void Update()
    {
    }

    public void EnterMap(int iMapID)
    {
        if (iMapID == m_iCurMapID)
        {
            return;
        }

        ApplicationStatusManager.GetStatus<GameStatus>().ChangeMapScene(m_iCurMapID,iMapID);

        m_iCurMapID = iMapID;
    }


    public int GetCurMapID()
    {
        return m_iCurMapID;
    }
}