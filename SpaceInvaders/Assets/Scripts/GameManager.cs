using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    public static float vertScreenSize;
    [HideInInspector]
    public float horisScreenSize;

    [HideInInspector]
    public Camera cameraOfGame;
    private const int VERTICAL_ENEMY_MAX_COUNT = 5;
    private const int HORIZONTAL_ENEMY_MAX_COUNT = 9;
    private const float PLAYER_SHIP_DISTANCE_FROM_BOTTOM = 0.5f;
    public float ENEMY_SHIP_HORIZONTAL_MOVE_BORDERS;

    [HideInInspector]
    public Dictionary<Vector2, Vector2> allPositionsForEnemiesWithIndexes; //first is index of position, second is coordinates

    public PlayerShip playerShip; 

    // Start is called before the first frame update
    void Start()
    {
        cameraOfGame = Camera.main;
        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        allPositionsForEnemiesWithIndexes = new Dictionary<Vector2, Vector2>();
        ENEMY_SHIP_HORIZONTAL_MOVE_BORDERS = horisScreenSize / 4;
        setAllEnemyPositions();
        setTheEnemies(1); //�� ��������� ����������� ������ ������� (������������ ����������� ��� ������������ �����������)
        activatePlayerShip();
    }


    //called only once while start of application
    private void setAllEnemyPositions()
    {
        float stepOfHorizontalPositions = horisScreenSize /2/ HORIZONTAL_ENEMY_MAX_COUNT; //���� �� 2 ��������� ������������ ��� �������������� �������� ������ � �����
        float stepOfVerticalPositions = vertScreenSize / 2 / VERTICAL_ENEMY_MAX_COUNT; //���� �������� ������ �� ��������� �������� ������ � ������ ��������� �������� ������� ����� �������� �� ��� 
        Vector2 positionCoordinates = Vector2.zero;

        for (int i = 0; i < VERTICAL_ENEMY_MAX_COUNT; i++)
        {
            for (int j = 0; j < HORIZONTAL_ENEMY_MAX_COUNT; j++)
            {
                //������� ����� ������� (������ � ������� �������)
                if (i == 0 && j == 0)
                {
                    positionCoordinates = new Vector2(stepOfHorizontalPositions * -4, vertScreenSize / 2 - stepOfVerticalPositions);
                }
                //������ ����� ������� ������ ������ ������� ������� ��������� ��������
                else if (j == 0)
                {
                    positionCoordinates = new Vector2(stepOfHorizontalPositions * -4, positionCoordinates.y - stepOfVerticalPositions);
                }
                else
                {
                    positionCoordinates = new Vector2(positionCoordinates.x + stepOfHorizontalPositions, positionCoordinates.y);
                }

                allPositionsForEnemiesWithIndexes.Add(new Vector2(i + 1, j + 1), positionCoordinates); //������� ��������� �.�. ����� � �������� � ������� LevelsData ���� � ������� � �� � ����
            }
        }
    }

    private void setTheEnemies(int level)
    {
        EnemyShip enenmy;
        int enenmyLevel;
        foreach (var coordinates in allPositionsForEnemiesWithIndexes)
        {
            if (LevelsData.allLevels[level].ContainsKey(coordinates.Key))
            {
                enenmyLevel = LevelsData.allLevels[level][coordinates.Key];
                ObjectPulledList = ObjectPuller.current.GetEnemyShipByLevel(enenmyLevel);
                ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
                ObjectPulled.transform.position = coordinates.Value;
                enenmy = ObjectPulled.GetComponent<EnemyShip>();
                enenmy.levelOfEnemy = enenmyLevel;
                enenmy.gameManager = this;
                enenmy.shotTimeMin = enenmyLevel*5;
                enenmy.setTheBordersForTheShip();
                ObjectPulled.SetActive(true);
                enenmy.StartCoroutine(enenmy.makeAShot());
            }
        }
    }
    private void activatePlayerShip()
    {
        //playerShip.gameManager = this;
        playerShip.transform.position = new Vector2(0, -vertScreenSize / 2 + playerShip.transform.localScale.y / 2 + PLAYER_SHIP_DISTANCE_FROM_BOTTOM);
        playerShip.leftBorderForShip = -horisScreenSize / 2  + playerShip.transform.localScale.x / 2;
        playerShip.rightBorderForShip = horisScreenSize / 2  - playerShip.transform.localScale.x / 2;
        playerShip.gameObject.SetActive(true);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
