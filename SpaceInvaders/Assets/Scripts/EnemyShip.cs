using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [HideInInspector]
    public int levelOfEnemy;
    [HideInInspector]
    private int HPOfEnemy;
    [HideInInspector]
    public GameManager gameManager;

    public float leftBorderForShip;
    public float rightBorderForShip;
    public Transform shipTransform;
    private float yPositionOfShip;
    private float speedOfShip;
    private bool moveRight;
    public float shotTimeMin;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;


    // Start is called before the first frame update
    private void OnEnable()
    {
        shipTransform = transform;
        yPositionOfShip = shipTransform.position.y;
        speedOfShip = levelOfEnemy>3?3: levelOfEnemy; // скороcть врагов не более 3
        HPOfEnemy = levelOfEnemy;
        moveRight = true;
    }

    public void setTheBordersForTheShip() {
        leftBorderForShip = shipTransform.position.x - gameManager.ENEMY_SHIP_HORIZONTAL_MOVE_BORDERS;
        rightBorderForShip = shipTransform.position.x + gameManager.ENEMY_SHIP_HORIZONTAL_MOVE_BORDERS;
    }

    private void holdShipInFramesAndSwitchDirection()
    {
        //для того чтобы корабль оставался в рамках экрана
        if (shipTransform.position.x < leftBorderForShip)
        {
            shipTransform.position = new Vector2(leftBorderForShip, yPositionOfShip);
            moveRight = true;
        }
        if (shipTransform.position.x > rightBorderForShip)
        {
            shipTransform.position = new Vector2(rightBorderForShip, yPositionOfShip);
            moveRight = false;
        }
    }

    private void disactivateThisShip() {
        ObjectPulledList = ObjectPuller.current.GetShipBurst();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = shipTransform.position;
        ObjectPulled.SetActive(true);
        gameManager.reduceTheEnemyCount(levelOfEnemy);
        gameObject.SetActive(false);
    }

    public void reduceTheLiveOfShip() {
        HPOfEnemy--;
        if (HPOfEnemy < 1) disactivateThisShip();
    }

    public IEnumerator makeAShot() {
        yield return new WaitForSeconds(Random.Range(shotTimeMin, shotTimeMin+5)); 
        ObjectPulledList = ObjectPuller.current.GetEnemyShot();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = shipTransform.position;
        ObjectPulled.SetActive(true);
        StartCoroutine(makeAShot());
    }


    private void Update()
    {
        if (moveRight)
        {
            shipTransform.position = new Vector2(shipTransform.position.x + Time.deltaTime * speedOfShip, yPositionOfShip);
        }
        else
        {
            shipTransform.position = new Vector2(shipTransform.position.x - Time.deltaTime * speedOfShip, yPositionOfShip);
        }
        holdShipInFramesAndSwitchDirection();
    }
}
