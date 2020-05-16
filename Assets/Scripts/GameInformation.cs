using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameInformation : MonoBehaviour
{
    Hashtable playerInfo = new Hashtable();

    //Input and Output Fields
    public InputField playerSearch;  
    public Text playerOutput;  
    public Text levelOutput;  
    public Text scoreOutput;
    public Text tableNamesOutput;
   

    // Start is called before the first frame update
    void Start()
    {
        //Setting the input UI to allow for searching
        playerSearch.onValueChanged.AddListener(delegate { checkForPlayer(); });
        
        //clearing the initial output boxes
        playerOutput.text = "Player Not Found";
        levelOutput.text = "";
        scoreOutput.text = "";
        
        //Populating the Hashtable
        AddPlayer("Alice", 7, 11200);
        AddPlayer("Bruce", 2, 500);
        AddPlayer("Charlie", 3, 800);
        AddPlayer("Danny", 4, 1000);
        AddPlayer("Ethan", 5, 10000);
        AddPlayer("Fran", 2, 550);
        AddPlayer("George", 10, 100000);

        //dynamically displaying the names in the Hashtable on sceneload
        //tableNames.text = String.Join(", ", playerInfo.Keys);
        string tableNames = "";
        int count = 0;
        foreach (string name in playerInfo.Keys)
        {
            if (count++ != 0)
            { tableNames +=", ";  }
            tableNames += name;
        }
        tableNamesOutput.text = tableNames;
    }

    // Update is called once per frame
    void Update()
    {
        //TESTING PURPOSE
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    printTable();
        //    if (playerInfo.Contains("Sally"))
        //    { Debug.Log("Found" + playerInfo["Sally"].ToString()); }
        //    else
        //    { Debug.Log("Player Not Found"); }
        //}
    }

    void AddPlayer(string name, int level, int score)
    {
        playerInfo.Add(name, new PlayerL3(name, level, score));
    }

    //TESTING PURPOSE
    //void printTable()
    //{
    //    foreach (var p in playerInfo.Values)
    //    {
    //        Debug.Log(p.ToString());
    //    }
    //}

    public void checkForPlayer()
    {
        var playerName = playerSearch.text;
        if (playerInfo.Contains(playerName))
        {
            PlayerL3 info = (PlayerL3)playerInfo[playerName];
            playerOutput.text = info.getName();
            levelOutput.text = info.getLevel().ToString();
            scoreOutput.text = info.getScore().ToString();
            //Debug.Log("Found" + playerInfo[playerSearch.text].ToString());
        }
        else
        {
            playerOutput.text = "Player Not Found";
            levelOutput.text = "";
            scoreOutput.text = "";
        }
    }
    


}
