/// <summary>
/// Logic main.
/// 角色管理器
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CharacterManager : IModuleBase
{
    public Dictionary<int,Character> m_dicCharacters = new Dictionary<int, Character>();

    public CharacterManager() : base("CharacterManager"){}

    public override void Init()
    {
        table.Config kConfig = TabtoyConfigManager.GetConfig();
        for (int i = 0; i < kConfig.Role.Count; ++i)
        {
            var kRole = kConfig.Role[i];
            _CreateCharacter(kRole.ID);
        }
    }

    public override void Update()
    {
        foreach (var kCharacter in m_dicCharacters)
        {
            kCharacter.Value.Update();
        }
    }

    protected void _CreateCharacter(int iTypeid)
    {
        Character kCharacter = new Character();
        kCharacter.Init(iTypeid);
        m_dicCharacters.Add(iTypeid, kCharacter);
    }

    public Character GetCharacter(int iTypeid)
    {
        if (m_dicCharacters.ContainsKey(iTypeid))
        {
            return m_dicCharacters[iTypeid];
        }
        return null;
    }
}