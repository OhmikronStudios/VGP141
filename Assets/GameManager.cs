using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] float botSpawnTimer = 2.0f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject botPrefab;

    public interface NotifySink
    { void OnEvent(); }

    LinkedList<NotifySink> alertList = new LinkedList<NotifySink>();
    LinkedList<GameObject> bots = new LinkedList<GameObject>();
   
    public void Register(NotifySink notification)
    { alertList.AddLast(notification); }

    public void Notify()
    {
        foreach (NotifySink note in alertList)
        { note.OnEvent(); }
    }

    public void NotifyDead(GameObject deadBot)
    { deadBot.SetActive(false); }

    IEnumerator BotSpawner()
    {
        foreach (GameObject bot in bots)
        {
            if (bot.activeSelf == false)
            {
                bot.GetComponent<PatrolBot>().Revive(spawnPoint.position);
                break;
            }
        }
        
        yield return new WaitForSeconds(botSpawnTimer);
        StartCoroutine(BotSpawner());

            //if (deadBots.Count != 0)
            //    {
            //        GameObject respawningBot = deadBots.Dequeue();
            //        respawningBot.SetActive(true);
            //        respawningBot.transform.position = spawnPoint.position;
            //        //aliveBots.Enqueue(respawningBot);
            //    }
    }


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newBot = (Instantiate(botPrefab));
            bots.AddFirst(newBot);
            newBot.SetActive(false);
        }

        StartCoroutine(BotSpawner());
    }





}
