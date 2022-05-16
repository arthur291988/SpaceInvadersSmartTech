
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;
    [HideInInspector]
    private int HPOfPlayer;

    [HideInInspector]
    public float leftBorderForShip;
    [HideInInspector]
    public float rightBorderForShip;
    private Transform shipTransform;
    private float yPositionOfShip;
    private float speedOfShip;
    private float shotTimer;
    private float shotTime;
    [HideInInspector]
    public GameManager gameManager;

    private void OnEnable()
    {
        shipTransform = transform;
        yPositionOfShip = shipTransform.position.y;
        speedOfShip = 5;
        shotTime = 0.7f;
        shotTimer = 0.7f;
        HPOfPlayer = 5;
    }

    private void holdShipInFrames() {
        //для того чтобы корабль оставался в рамках экрана
        if (shipTransform.position.x < leftBorderForShip) shipTransform.position = new Vector2(leftBorderForShip, yPositionOfShip);
        if (shipTransform.position.x > rightBorderForShip) shipTransform.position = new Vector2(rightBorderForShip, yPositionOfShip);
    }

    private void disactivateThisShip()
    {
        ObjectPulledList = ObjectPuller.current.GetShipBurst();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = shipTransform.position;
        ObjectPulled.SetActive(true);
        gameManager.defeatFunction();
        gameObject.SetActive(false);
    }

    public void reduceTheLiveOfShip()
    {
        HPOfPlayer--;
        if (HPOfPlayer < 1) disactivateThisShip();
    }

    private void Update()
    {
        if (Input.GetKey("right"))
        {
            shipTransform.position = new Vector2(shipTransform.position.x+Time.deltaTime* speedOfShip, yPositionOfShip);
            holdShipInFrames();
        }

        if (Input.GetKey("left"))
        {
            shipTransform.position = new Vector2(shipTransform.position.x - Time.deltaTime * speedOfShip, yPositionOfShip);
            holdShipInFrames();
        }
        //выстрел
        if (Input.GetKey("space") && shotTimer >= shotTime) {
            ObjectPulledList = ObjectPuller.current.GetPlayerShot();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = shipTransform.position;
            ObjectPulled.SetActive(true);
            shotTimer = 0;
        }
        if (shotTimer < shotTime) shotTimer += Time.deltaTime;
    }
}
