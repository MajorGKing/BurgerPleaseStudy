using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#region DataModel
[Serializable]
public class GameSaveData
{
	// 소지금.
	public long Money = 0;

	// 플레이어 위치.
	public int RestaurantIndex;
	public Vector3 PlayerPosition;

	// 스테이지 별 상태.
	public List<RestaurantData> Restaurants;
}

[Serializable]
public class RestaurantData
{
	// 직원 수.
	public int WorkerCount;

	// 업그레이드.

	// 프랍들.
	public Define.ETutorialState TutorialState = Define.ETutorialState.None;
	public List<UnlockableStateData> UnlockableStates;
}

[Serializable]
public class UnlockableStateData
{
	public Define.EUnlockedState State = Define.EUnlockedState.Hidden;
	public long SpentMoney = 0;
    public long HasMoney = 0;
}
#endregion

public class SaveManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
