
using System.Collections.Generic;
using UnityEngine;

public class LevelsData : MonoBehaviour
{
    //индексы позиций вражеских кораблей на сцене с уровнями кораблей, начиная с верхнего левого угла 

    public static Dictionary<Vector2, int> level1 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 5)] = 5,

        [new Vector2(2, 1)] = 4,
        [new Vector2(2, 9)] = 4,

        [new Vector2(3, 4)] = 3,
        [new Vector2(3, 5)] = 3,
        [new Vector2(3, 6)] = 3,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 6)] = 2,
        [new Vector2(4, 7)] = 2,

        [new Vector2(5, 4)] = 1,
        [new Vector2(5, 5)] = 1,
        [new Vector2(5, 6)] = 1,
    };
    public static Dictionary<Vector2, int> level2 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 9)] = 5,

        [new Vector2(2, 1)] = 4,
        [new Vector2(2, 5)] = 4,
        [new Vector2(2, 9)] = 4,

        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 3,
        [new Vector2(3, 6)] = 3,
        [new Vector2(3, 7)] = 3,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 5)] = 2,
        [new Vector2(4, 7)] = 2,

        [new Vector2(5, 1)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 5)] = 1,
        [new Vector2(5, 7)] = 1,
        [new Vector2(5, 9)] = 1,
    }; 
    public static Dictionary<Vector2, int> level3 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 5)] = 5,
        [new Vector2(1, 9)] = 5,

        [new Vector2(2, 1)] = 4,
        [new Vector2(2, 3)] = 4,
        [new Vector2(2, 5)] = 4,
        [new Vector2(2, 7)] = 4,
        [new Vector2(2, 9)] = 4,

        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 3,
        [new Vector2(3, 5)] = 3,
        [new Vector2(3, 6)] = 3,
        [new Vector2(3, 7)] = 3,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,
        [new Vector2(4, 5)] = 2,
        [new Vector2(4, 6)] = 2,
        [new Vector2(4, 7)] = 2,

        [new Vector2(5, 1)] = 1,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 1,
        [new Vector2(5, 5)] = 1,
        [new Vector2(5, 6)] = 1,
        [new Vector2(5, 7)] = 1,
        [new Vector2(5, 8)] = 1,
        [new Vector2(5, 9)] = 1,
    };
    //все уровни которые будут в игре (для масштабирования), вызов через кнопку которая имеет параметр int, который в свою очередь будет соотвествовать номеру уровня 
    public static Dictionary<int, Dictionary<Vector2, int>> allLevels = new Dictionary<int, Dictionary<Vector2, int>>
    {
        [1] = level1,
        [2] = level2,
        [3] = level3,
    };
}
