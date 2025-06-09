using System.Collections;
using UnityEngine;

public class GameManager
{
	public Vector2 JoystickDir { get; set; } = Vector2.zero;
	public PlayerController Player;
	public Restaurant Restaurant;

	public long Money
	{
		get { return Managers.Save.SaveData.Money; }
		set
		{
			Managers.Save.SaveData.Money = value;
			Managers.Event.TriggerEvent(Define.EEventType.MoneyChanged);
		}
	}

	public void Init()
	{
		// Managers.Instance.StartCoroutine(CoInitialize());
	}

	public void StartSaveGame()
	{
		Managers.Instance.StartCoroutine(CoInitialize());
	}

	// 한 프레임 기다려서 모든 오브젝트 초기화 끝나고 실행.
	public IEnumerator CoInitialize()
	{
		yield return new WaitForEndOfFrame();

		Player = GameObject.FindAnyObjectByType<PlayerController>();
		Restaurant = GameObject.FindAnyObjectByType<Restaurant>();

		int index = Restaurant.StageNum;
		Restaurant.SetInfo(Managers.Save.SaveData.Restaurants[index]);

		Managers.Instance.StartCoroutine(CoSaveData());
	}

	IEnumerator CoSaveData()
	{
		while (true)
		{
			yield return new WaitForSeconds(10);

			Debug.Log("Try Save!");

			if (Managers.Scene.CurrentScene.SceneType != Define.EScene.DevScene || Managers.Scene.CurrentScene.SceneType != Define.EScene.GameScene)
				continue;

			Managers.Save.SaveData.RestaurantIndex = Restaurant.StageNum;
			Managers.Save.SaveData.PlayerPosition = Player.transform.position;

			Managers.Save.SaveGame();
		}
	}
}