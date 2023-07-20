using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화
public class GamaData
{
    public string Pos = "0/0";

    // 각 지역의 잠금여부
    public bool isClear2;
    public bool isClear3;
    public bool isClear4;
    public bool isClear5;

    public int isClear2Count = 0;
    public int isClear3Count = 0;
    public int isClear4Count = 0;
    public int isClear5Count = 0;

}
