using System;
using UnityEngine;

public class SingletonMonoBehaviour<T>:MonoBehaviour where T : MonoBehaviour
{
    static T m_instance = null;
    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                Type t = typeof(T);
                m_instance = (T)FindObjectOfType(t);
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        //CheckIns();
    }

    bool CheckIns()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            return true;
        }
        else if (m_instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
