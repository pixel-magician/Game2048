using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    int[,] _data;
    CubeItem[,] _cubes;

    [SerializeField]
    int _degree = 4;
    [SerializeField]
    GameObject _itemPrefab;
    [SerializeField]
    Transform _container;
    static Dictionary<int, Color> _colorSetting = null;

    public static Dictionary<int, Color> ColorSetting { get => _colorSetting; set => _colorSetting = value; }

    private void Start()
    {
        Init();
        Create();
    }
    private void Update()
    {
        HandleInput();
    }

    public void Create()
    {
        for (int i = 0; i < _degree; i++)
        {
            for (int j = 0; j < _degree; j++)
            {
                GameObject g = Instantiate(_itemPrefab, _container);
                _cubes[i, j] = g.GetComponent<CubeItem>();
                _cubes[i, j].Refresh(_data[i, j]);
            }
        }
    }

    public void Init()
    {
        //初始化棋盘数据，全部赋值为0
        _data = new int[_degree, _degree];
        _cubes = new CubeItem[_degree, _degree];
        for (int i = 0; i < _degree; i++)
        {
            for (int j = 0; j < _degree; j++)
            {
                _data[i, j] = 0;
            }
        }
        //随机选两个格子赋值为2，注：两组随机数不能相同
        Vector2Int one = new Vector2Int(Random.Range(0, _degree), Random.Range(0, _degree));
        Vector2Int two = new Vector2Int(Random.Range(0, _degree), Random.Range(0, _degree));
        while (one == two)
        {
            two = new Vector2Int(Random.Range(0, _degree), Random.Range(0, _degree));
        }
        //_data[one.x, one.y] = 2;
        //_data[two.x, two.y] = 2;

        _data[2, 0] = 2;
        _data[1, 0] = 4;
        //_data[0, 3] = 2;


        //设定颜色值
        if (_colorSetting == null)
        {
            _colorSetting = new Dictionary<int, Color>();
            _colorSetting.Add(0, Color.white);
            _colorSetting.Add(2, Color.yellow);
            _colorSetting.Add(4, Color.blue);
            _colorSetting.Add(8, Color.green);
            _colorSetting.Add(16, Color.cyan);
            _colorSetting.Add(32, Color.red);
            _colorSetting.Add(64, Color.gray);
            _colorSetting.Add(128, Color.gray);
            _colorSetting.Add(256, Color.gray);
            _colorSetting.Add(512, Color.gray);
            _colorSetting.Add(1024, Color.gray);
            _colorSetting.Add(2048, Color.gray);
        }
    }


    public void HandleInput()
    {
        if (Input.anyKeyDown)
        {
            float f = Input.GetAxis("Horizontal");
            if (f < 0) HandleLeft();
            else HandleRight();
        }
        if (Input.anyKeyDown)
        {
            float f = Input.GetAxis("Vertical");
            if (f < 0) HandleLeft();
            else HandleRight();
        }
        return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HandleLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleDown();
        }
    }



    void HandleLeft()
    {
        for (int r = 0; r < _degree; r++)
        {
            for (int c = 1; c < _degree; c++)
            {
                MoveLeft(r, c);
            }
        }
        Refresh();
    }
    void HandleRight()
    {
        for (int r = 0; r < _degree; r++)
        {
            for (int c = _degree - 2; c >= 0; c--)
            {
                MoveRight(r, c);
            }
        }
        Refresh();
    }

    void HandleUp()
    {
        for (int c = 0; c < _degree; c++)
        {
            for (int r = 1; r < _degree; r++)
            {
                MoveUp(r, c);
            }
        }
        Refresh();
    }
    void HandleDown()
    {
        for (int c = 0; c < _degree; c++)
        {
            for (int r = _degree - 2; r >= 0; r--)
            {
                MoveDown(r, c);
            }
        }
        Refresh();
    }

    void MoveLeft(int x, int y)
    {
        if (_data[x, y] == 0 || y == 0) return;//当前坐标的格子 如果在最边缘或者值为0，直接退出
        int index = y - 1;//需要做比较的格子的横向坐标
        //循环，将需要比较的格子往前推
        while (index >= 0)
        {
            //每次循环都做判断，遇到非空的格子，做相应处理
            if (_data[x, index] != 0)
            {
                if (_data[x, y] == _data[x, index])//两个格子数值相等，做叠加操作。
                {
                    _data[x, index] += _data[x, y];
                    _data[x, y] = 0;
                }
                else if (y - index > 1)//判断两个格子间隔大于1执行后续逻辑，因为相邻的两个格子不需要移动
                {
                    _data[x, index + 1] = _data[x, y];//两个格子不相等，移动到这个格子旁边。
                    _data[x, y] = 0;
                }
                break;
            }
            //没有遇到非空的格子，判断循环是否到达棋盘边缘，到达边缘要做交换（移动到这个位置）操作
            else if (index == 0)
            {
                _data[x, index] = _data[x, y];
                _data[x, y] = 0;
                break;
            }
            index--;
        }
    }

    void MoveRight(int x, int y)
    {
        if (_data[x, y] == 0 || y == _degree - 1) return;//当前坐标的格子 如果在最边缘或者值为0，直接退出
        int index = y + 1;//需要做比较的格子的横向坐标
        //循环，将需要比较的格子往后推
        while (index < _degree)
        {
            //每次循环都做判断，遇到非空的格子，做相应处理
            if (_data[x, index] != 0)
            {
                if (_data[x, y] == _data[x, index])//两个格子数值相等，做叠加操作。
                {
                    _data[x, index] += _data[x, y];
                    _data[x, y] = 0;
                }
                else if (index - y > 1)//判断两个格子间隔大于1执行后续逻辑，因为相邻的两个格子不需要移动
                {
                    _data[x, index - 1] = _data[x, y];//两个格子不相等，移动到这个格子旁边。
                    _data[x, y] = 0;
                }
                break;
            }
            //没有遇到非空的格子，判断循环是否到达棋盘边缘，到达边缘要做交换（移动到这个位置）操作
            else if (index == _degree - 1)
            {
                _data[x, index] = _data[x, y];
                _data[x, y] = 0;
                break;
            }
            index++;
        }
    }

    void MoveUp(int x, int y)
    {
        if (_data[x, y] == 0 || x == 0) return;//当前坐标的格子 如果在最边缘或者值为0，直接退出
        int index = x - 1;//需要做比较的格子的横向坐标
        //循环，将需要比较的格子往前推
        while (index >= 0)
        {
            //每次循环都做判断，遇到非空的格子，做相应处理
            if (_data[index, y] != 0)
            {
                if (_data[x, y] == _data[index, y])//两个格子数值相等，做叠加操作。
                {
                    _data[index, y] += _data[x, y];
                    _data[x, y] = 0;
                }
                else if (x - index > 1)//判断两个格子间隔大于1执行后续逻辑，因为相邻的两个格子不需要移动
                {
                    _data[index + 1, y] = _data[x, y];//两个格子不相等，移动到这个格子旁边。
                    _data[x, y] = 0;
                }
                break;
            }
            //没有遇到非空的格子，判断循环是否到达棋盘边缘，到达边缘要做交换（移动到这个位置）操作
            else if (index == 0)
            {
                _data[index, y] = _data[x, y];
                _data[x, y] = 0;
                break;
            }
            index--;
        }
    }

    void MoveDown(int x, int y)
    {
        if (_data[x, y] == 0 || x == _degree - 1) return;//当前坐标的格子 如果在最边缘或者值为0，直接退出
        int index = x + 1;//需要做比较的格子的横向坐标
        //循环，将需要比较的格子往后推
        while (index < _degree)
        {
            //每次循环都做判断，遇到非空的格子，做相应处理
            if (_data[index, y] != 0)
            {
                if (_data[x, y] == _data[index, y])//两个格子数值相等，做叠加操作。
                {
                    _data[index, y] += _data[x, y];
                    _data[x, y] = 0;
                }
                else if (index - x > 1)//判断两个格子间隔大于1执行后续逻辑，因为相邻的两个格子不需要移动
                {
                    _data[index - 1, y] = _data[x, y];//两个格子不相等，移动到这个格子旁边。
                    _data[x, y] = 0;
                }
                break;
            }
            //没有遇到非空的格子，判断循环是否到达棋盘边缘，到达边缘要做交换（移动到这个位置）操作
            else if (index == _degree - 1)
            {
                _data[index, y] = _data[x, y];
                _data[x, y] = 0;
                break;
            }
            index++;
        }
    }



    void Refresh()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                _cubes[r, c].Refresh(_data[r, c]);
            }
        }
    }


}
