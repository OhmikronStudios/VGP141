using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerL3
{
    public string m_name;
    public int m_currentLevel;
    public int m_score;
    

   
    public PlayerL3(string name, int level, int score)
    {
        m_name = name;
        m_currentLevel = level;
        m_score = score;
    }

    public override string ToString()
    {
        return "(" + m_name + "," + m_currentLevel + "," + m_score + ")";
    }

    public string getName()
    {
        return m_name;
    }
    public int getLevel()
    {
        return m_currentLevel;
    }public int getScore()
    {
        return m_score;
    }


}
