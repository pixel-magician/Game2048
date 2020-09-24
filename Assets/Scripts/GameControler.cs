using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    int[,] _data;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }





    public void Init()
    {
        _data = new int[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                _data[i, j] = 0;
            }
        }
        Vector2Int one = new Vector2Int(Random.Range(0, 4), Random.Range(0, 4));
        Vector2Int two = new Vector2Int(Random.Range(0, 4), Random.Range(0, 4));
        while (one == two)
        {
            two = new Vector2Int(Random.Range(0, 4), Random.Range(0, 4));
        }
        //_data[one.x, one.y] = 2;
        //_data[two.x, two.y] = 2;

        _data[0, 0] = 2;
        _data[0, 2] = 2;
    }
}
