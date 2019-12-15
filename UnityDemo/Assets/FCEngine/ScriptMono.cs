﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScriptMono : MonoBehaviour
{
    public string ScripClassName; // 对应脚本中的类名
    protected long m_nScriptInsPtr; // 脚本对象指针(一个64位整数)

    void  Start()
    {
        // 目前这个示例中，由于DLL初始化比较晚，就不在这里执行了
        m_nScriptInsPtr = 0;

        // 这时脚本还没有加载好，暂时就不在这里执行了
        //CreateScript();
        FCScriptLoader.InitCall(CreateScript); // 如果没有初始化发了脚本，就延后执行
    }
    void CreateScript()
    {
        if (0 != m_nScriptInsPtr)
            return;
        // 创建一个脚本
        if (string.IsNullOrEmpty(ScripClassName))
            m_nScriptInsPtr = 0;
        else
            m_nScriptInsPtr = FCLibHelper.fc_instance(ScripClassName);

        // 必要的话，调用下脚本中的Start函数
        if (m_nScriptInsPtr != 0)
        {
            // 假设存在transform变量
            SetScriptValue("transform", transform);

            FCLibHelper.fc_call(m_nScriptInsPtr, "Start");
        }

        OnCreateScript();
    }
    protected virtual void OnCreateScript()
    {

    }
    void OnDestroy()
    {
        if(m_nScriptInsPtr != 0)
        {
            FCLibHelper.fc_call(m_nScriptInsPtr, "OnDestroy"); // 实际上，脚本一般是不需要OnDestroy事件的，只需要释放脚本就可以了
            FCLibHelper.fc_relese_ins(m_nScriptInsPtr); // 释放脚本对象，如果脚本对象有析构函数，就会自动调用析构函数
            m_nScriptInsPtr = 0;
        }
    }
    public void OnReloadResource()
    {
        m_nScriptInsPtr = 0;
    }
    public void OnButtonClicked(string szName)
    {
        CreateScript(); // 延迟执行吧

        if (m_nScriptInsPtr != 0)
        {
            FCDll.PushCallParam(szName);  // 传点击的按钮的参数
            FCLibHelper.fc_call(m_nScriptInsPtr, "OnButtonClicked");
        }
    }
    public long GetScriptPtr()
    {
        CreateScript(); // 延迟执行吧
        return m_nScriptInsPtr;
    }
    // 功能：设置脚本中的变量
    public void SetScriptValue(string szName, bool value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_bool(ptr, value);
        }
    }
    public void SetScriptValue(string szName, byte value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_byte(ptr, value);
        }
    }
    public void SetScriptValue(string szName, short value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_short(ptr, value);
        }
    }
    public void SetScriptValue(string szName, ushort value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_ushort(ptr, value);
        }
    }
    public void SetScriptValue(string szName, int value)
    {
        if(m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_int(ptr, value);
        }
    }
    public void SetScriptValue(string szName, uint value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_uint(ptr, value);
        }
    }
    public void SetScriptValue(string szName, float value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_float(ptr, value);
        }
    }
    public void SetScriptValue(string szName, double value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_double(ptr, value);
        }
    }
    public void SetScriptValue(string szName, string value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_string(ptr, value);
        }
    }
    public void SetScriptValue(string szName, System.Object value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            long obj_ptr = FCGetObj.PushObj(value);
            FCLibHelper.fc_set_value_wrap_objptr(ptr, obj_ptr);
        }
    }
    public void SetScriptValue(string szName, UnityEngine.Object value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            long obj_ptr = FCGetObj.PushObj(value);
            FCLibHelper.fc_set_value_wrap_objptr(ptr, obj_ptr);
        }
    }
    public void SetScriptValue(string szName, Vector2 value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_vector2(ptr, ref value);
        }
    }
    public void SetScriptValue(string szName, Vector3 value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_vector3(ptr, ref value);
        }
    }
    public void SetScriptValue(string szName, Vector4 value)
    {
        if (m_nScriptInsPtr != 0)
        {
            long ptr = FCLibHelper.fc_get_class_value(m_nScriptInsPtr, szName);
            FCLibHelper.fc_set_value_vector4(ptr, ref value);
        }
    }
}
