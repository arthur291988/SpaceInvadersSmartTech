
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{

    public static ObjectPuller current;

    private const int pullOfObjects9 = 9;
    private const int pullOfObjects7 = 7;
    private const int pullOfObjects5 = 5;
    private const int pullOfObjects3 = 3;
    private bool willGrow;

    [SerializeField]
    private GameObject enemyShipLevel1;
    [SerializeField]
    private GameObject enemyShipLevel2;
    [SerializeField]
    private GameObject enemyShipLevel3;
    [SerializeField]
    private GameObject enemyShipLevel4;
    [SerializeField]
    private GameObject enemyShipLevel5;

    [SerializeField]
    private GameObject playerShot;
    [SerializeField]
    private GameObject enemyShot;

    [SerializeField]
    private GameObject shipBurst;
    [SerializeField]
    private GameObject bulletBurst;

    private List<GameObject> enemyShipLevel1Pull;
    private List<GameObject> enemyShipLevel2Pull;
    private List<GameObject> enemyShipLevel3Pull;
    private List<GameObject> enemyShipLevel4Pull;
    private List<GameObject> enemyShipLevel5Pull;
    private List<GameObject> playerShotPull;
    private List<GameObject> enemyShotPull;
    private List<GameObject> shipBurstPull;
    private List<GameObject> bulletBurstPull;

    private void Awake()
    {
        willGrow = true;
        current = this;
    }

    private void OnEnable()
    {
        enemyShipLevel1Pull = new List<GameObject>();
        enemyShipLevel2Pull = new List<GameObject>();
        enemyShipLevel3Pull = new List<GameObject>();
        enemyShipLevel4Pull = new List<GameObject>();
        enemyShipLevel5Pull = new List<GameObject>();
        playerShotPull = new List<GameObject>();
        enemyShotPull = new List<GameObject>();
        shipBurstPull = new List<GameObject>();
        bulletBurstPull = new List<GameObject>();

        for (int i = 0; i < pullOfObjects9; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyShipLevel1);
            obj.SetActive(false);
            enemyShipLevel1Pull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(enemyShot);
            obj1.SetActive(false);
            enemyShotPull.Add(obj1);
        }
        for (int i = 0; i < pullOfObjects7; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyShipLevel2);
            obj.SetActive(false);
            enemyShipLevel2Pull.Add(obj);

        }
        for (int i = 0; i < pullOfObjects5; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyShipLevel3);
            obj.SetActive(false);
            enemyShipLevel3Pull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(enemyShipLevel4);
            obj1.SetActive(false);
            enemyShipLevel4Pull.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(playerShot);
            obj2.SetActive(false);
            playerShotPull.Add(obj2);
        }
        for (int i = 0; i < pullOfObjects3; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyShipLevel5);
            obj.SetActive(false);
            enemyShipLevel5Pull.Add(obj);


            GameObject obj2 = (GameObject)Instantiate(bulletBurst);
            obj2.SetActive(false);
            bulletBurstPull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(shipBurst);
            obj3.SetActive(false);
            shipBurstPull.Add(obj3);
        }
    }
    public List<GameObject> GetEnemyShipByLevel(int level)
    {
        if (level==1) return enemyShipLevel1Pull;
        else if (level == 2) return enemyShipLevel2Pull;
        else if(level == 3) return enemyShipLevel3Pull;
        else if(level == 4) return enemyShipLevel4Pull;
        else return enemyShipLevel5Pull;
    }

    public List<GameObject> GetPlayerShot()
    {
        return playerShotPull;
    }

    public List<GameObject> GetEnemyShot()
    {
        return enemyShotPull;
    }

    public List<GameObject> GetShipBurst()
    {
        return shipBurstPull;
    }

    public List<GameObject> GetBulletBurst()
    {
        return bulletBurstPull;
    }

    

    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects
    public GameObject GetGameObjectFromPull(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return (GameObject)GOLists[i];
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(GOLists[0]);
            obj.SetActive(false);
            GOLists.Add(obj);
            return obj;
        }
        return null;
    }

}
