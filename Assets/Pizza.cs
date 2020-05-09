using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza 
{
    string m_type;
    float m_bakingTime;

  
    public Pizza (string type, float bakingTime) 
    {
        m_type = type;
        m_bakingTime = bakingTime;
    }
    public float getBakingTime()
    {
        return m_bakingTime;
    }
    public string getType()
    {
        return m_type;
    }

    

}
