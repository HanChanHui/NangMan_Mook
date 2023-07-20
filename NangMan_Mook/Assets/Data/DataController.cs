using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터형태로 변환
// 3. 불러온 데이터를 사용


public class DataController : MonoBehaviour
{

    // 싱글톤으로 선언
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;

    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    // 게임 데이터 파일이름 설정
    public string GameDataFileName = "StarfishData.json";

    // "원하는 이름(영문).json"
    public GamaData _gameData;
    public GamaData gameData
    {
        get
        {
            // 게임이 시작되면 자동으로 실행되도록
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    // 저장된 게임 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 저장된 게임이 있다면
        if(File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GamaData>(FromJsonData);
        }

        // 저장된 게임이 없다면
        else
        {
            print("새로운 파일 생성");
            _gameData = new GamaData();
        }
    }

    // 게임 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        // 올바른 저장인지 확인 (자유롭게 변형)
        print("저장완료");
        print("2는 " + gameData.isClear2);
        print("3는 " + gameData.isClear3);
        print("4는 " + gameData.isClear4);
        print("5는 " + gameData.isClear5);
    }

    // 게임을 종료하면 자동저장되도록
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void SaveUnlock(int chapterNumber)
    {
        switch (chapterNumber)
        {
            case 2:
                _gameData.isClear2 = true;
                _gameData.isClear2Count++;
                SaveGameData();
                break;
            case 3:
                _gameData.isClear3 = true;
                _gameData.isClear3Count++;
                SaveGameData();
                break;
            case 4:
                _gameData.isClear4 = true;
                _gameData.isClear4Count++;
                SaveGameData();
                break;
            case 5:
                _gameData.isClear5 = true;
                _gameData.isClear5Count++;
                SaveGameData();
                break;
        }
    }

    public void SaveReset()
    {
        _gameData.Pos = "0/0";
        _gameData.isClear2 = false;
        _gameData.isClear3 = false;
        _gameData.isClear4 = false;
        _gameData.isClear2Count = 0;
        _gameData.isClear3Count = 0;
        _gameData.isClear4Count = 0;
        SaveGameData();
    }
}
