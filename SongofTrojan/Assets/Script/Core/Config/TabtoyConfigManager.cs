using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FrameWork;
using System.Text;
using System;
using LuaInterface;

/// <summary>
/// excel配置管理,读取config.json文件
/// </summary>
public static class TabtoyConfigManager
{
	static Dictionary<string,object> s_excelConfig = new Dictionary<string,object>();
	public const string c_directoryName = "Config";
	public const string c_expandName    = "json";

	public static bool GetIsExistConfig(string ConfigName)
	{
		string dataJson = "";

		#if UNITY_EDITOR
		if(!Application.isPlaying)
		{
			dataJson = ResourceIOTool.ReadStringByResource(
				PathTool.GetRelativelyPath(c_directoryName,
					ConfigName,
					c_expandName));
		}
		else
		{
			dataJson = ResourceManager.ReadTextFile(ConfigName);
		}

		#else
		dataJson = ResourceManager.ReadTextFile(ConfigName);
		#endif

		if (dataJson == "")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public static object GetData(string ConfigName)
	{
		if (s_excelConfig.ContainsKey(ConfigName))
		{
			return s_excelConfig[ConfigName];
		}

		string dataJson = "";

		#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			dataJson = ResourceIOTool.ReadStringByResource(
				PathTool.GetRelativelyPath(c_directoryName,
					ConfigName,
					c_expandName));
		}
		else
		{
			dataJson = ResourceManager.ReadTextFile(ConfigName);
		}
		#else
		dataJson = ResourceManager.ReadTextFile(ConfigName);
		#endif

		if (dataJson == "")
		{
			throw new Exception("ConfigManager GetData not find " + ConfigName);
		}
		else
		{
			Dictionary<string,object> datas = new Dictionary<string,object>();
			if (!string.IsNullOrEmpty(dataJson))
			{
				Dictionary<string, object> listData = Json.Deserialize(dataJson) as Dictionary<string, object>;
				if (listData == null)
				{
					return datas;
				}

				foreach (string key in listData.Keys)
				{
					datas.Add (key, (listData [key]));
				}
			}

			s_excelConfig.Add(ConfigName, datas);
			return datas;
		}
	}
	
}

