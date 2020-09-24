using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeItem : MonoBehaviour
{

    [SerializeField]
    Text _title;
    [SerializeField]
    Image _bg;

    



    public void Refresh(int value = 0)
    {
        _bg.color = Chessboard.ColorSetting[value];
        if (value == 0)
        {
            _title.text = "";
            return;
        }
        _title.text = value.ToString();
    }
}
