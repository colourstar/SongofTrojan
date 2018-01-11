/// <summary>
/// AttrDefine
/// 属性定义
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Attr
{
    protected float    m_fBaseValue = 0.0f;
    protected float    m_fBasePercent = 1.0f;
    protected float    m_fFixValue = 0.0f;
    protected float    m_fCurValue = 0.0f;
    protected float    m_fMaxValue = 0.0f;

    public void     ModifyAttr(float fBaseValue = 0.0f,float fPercent = 0.0f,float fFixValue = 0.0f,bool bModifyCurValue = true)
    {
        m_fBaseValue += fBaseValue;
        m_fBasePercent += fPercent;
        m_fFixValue += fFixValue;
        m_fMaxValue = m_fBaseValue * m_fBasePercent + m_fFixValue;
        if (bModifyCurValue || m_fCurValue > m_fMaxValue)
        {
            m_fCurValue = m_fMaxValue;
        }
    }

    public float    GetCurValue()
    {
        return m_fCurValue;
    }
}

