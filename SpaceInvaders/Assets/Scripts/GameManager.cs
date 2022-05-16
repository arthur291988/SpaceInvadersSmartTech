
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int enemyCount;
    private int score;

    [SerializeField]
    private Text winLoseText;
    [SerializeField]
    private GameObject WinLoseButton;
    private Image WinLoseButtonImage;

    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject gameInfoPanel;
    [SerializeField]
    private Text scoreText;

    private List<GameObject> allEnemies;


    [SerializeField]
    private GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        allEnemies = new List<GameObject>();
        cameraOfGame = Camera.main;
        vertScreenSize = cameraOfGame.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        allPositionsForEnemiesWithIndexes = new Dictionary<Vector2, Vector2>();
        WinLoseButtonImage = WinLoseButton.GetComponent<Image>();
        ENEMY_SHIP_HORIZONTAL_MOVE_BORDERS = horisScreenSize / 4;
        setAllEnemyPositions();
    }

    public void startTheGame(int level) {
        setTheEnemies(level);
        menuPanel.SetActive(false);
        quitButton.SetActive(false);
        activatePlayerShip();
        score=0;
        scoreText.text = score.ToString();
        gameInfoPanel.SetActive(true);
    }

    
    private void setAllEnemyPositions()
    {
        float stepOfHorizontalPositions = horisScreenSize / 2 / HORIZONTAL_ENEMY_MAX_COUNT; //деля на 2 оставляем пространство для маневрирования кораблям справа и слева
        float stepOfVerticalPositions = vertScreenSize / 2 / VERTICAL_ENEMY_MAX_COUNT; //одна половина экрана по вертикали остается игроку и вторая вражеским кораблям поэтому экран делиться на два 
        Vector2 positionCoordinates = Vector2.zero;

        for (int i = 0; i < VERTICAL_ENEMY_MAX_COUNT; i++)
        {
            for (int j = 0; j < HORIZONTAL_ENEMY_MAX_COUNT; j++)
            {
                //верхняя левая позиция (первая в словаре позиций)
                if (i == 0 && j == 0)
                {
                    positionCoordinates = new Vector2(stepOfHorizontalPositions * -4, vertScreenSize / 2 - stepOfVerticalPositions);
                }
                //первая левая позиция каждой строки таблицы позиций вражеских кораблей
                else if (j == 0)
                {
                    positionCoordinates = new Vector2(stepOfHorizontalPositions * -4, positionCoordinates.y - stepOfVerticalPositions);
                }
                else
                {
                    positionCoordinates = new Vector2(positionCoordinates.x + stepOfHorizontalPositions, positionCoordinates.y);
                }

                allPositionsForEnemiesWithIndexes.Add(new Vector2(i + 1, j + 1), positionCoordinates); //единицы добавляем т.к. ключи к позициям в скрипте LevelsData идут с единицы а не с нуля
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
                enenmy.shotTimeMin = enenmyLevel * 5;
                enenmy.setTheBordersForTheShip();
                allEnemies.Add(ObjectPulled);
                ObjectPulled.SetActive(true);
                enemyCount++;
                enenmy.StartCoroutine(enenmy.makeAShot());
            }
        }
    }
    private void activatePlayerShip()
    {
        playerShip.transform.position = new Vector2(0, -vertScreenSize / 2 + playerShip.transform.localScale.y / 2 + PLAYER_SHIP_DISTANCE_FROM_BOTTOM);
        playerShip.leftBorderForShip = -horisScreenSize / 2 + playerShip.transform.localScale.x / 2;
        playerShip.rightBorderForShip = horisScreenSize / 2 - playerShip.transform.localScale.x / 2;
        playerShip.gameManager = this;
        playerShip.gameObject.SetActive(true);
    }

    public void reduceTheEnemyCount(int level) {
        enemyCount--;
        score+=1*level;
        scoreText.text = score.ToString();
        if (enemyCount < 1 && !WinLoseButton.activeInHierarchy)
        {
            victoryFunction();
        }
    }

    public void defeatFunction()
    {
        WinLoseButtonImage.color = Color.red;
        winLoseText.text = "ПОРАЖЕНИЕ! Очки " + score.ToString();
        WinLoseButton.SetActive(true);
    }

    private void victoryFunction()
    {
        WinLoseButtonImage.color = Color.green;
        winLoseText.text = "ПОБЕДА! Очки "+ score.ToString();
        WinLoseButton.SetActive(true);
    }

    public void gotToMenu()
    {
        playerShip.gameObject.SetActive(false);
        foreach (GameObject go in allEnemies) go.SetActive(false);
        allEnemies.Clear();
        enemyCount = 0;
        WinLoseButton.SetActive(false);
        gameInfoPanel.SetActive(false);
        menuPanel.SetActive(true);
        quitButton.SetActive(true);
        score = 0;
    }

    public void quitTheGame() => Application.Quit();
}
