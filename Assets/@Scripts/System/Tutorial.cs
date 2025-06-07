using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
	private MainCounterSystem _mainCounterSystem;

	private RestaurantData _data;

	private Define.ETutorialState _state
	{
		get { return _data.TutorialState; }
		set { _data.TutorialState = value; }
	}
    
    private UI_GameScene _uiGameScene;

    public void SetInfo(RestaurantData data)
	{
		_data = data;

		if (_state == Define.ETutorialState.None)
			_state = Define.ETutorialState.CreateFirstTable;

        _uiGameScene = Managers.UI.SceneUI as UI_GameScene;

		StartCoroutine(CoStartTutorial());
	}

    IEnumerator CoStartTutorial()
    {
        yield return new WaitForEndOfFrame();

        Counter counter = _mainCounterSystem.Counter;
        Grill grill = _mainCounterSystem.Grill;
		Table firstTable = _mainCounterSystem.Tables[0];
		Table secondTable = _mainCounterSystem.Tables[1];
		Office office = _mainCounterSystem.Office;
		TrashCan trashCan = _mainCounterSystem.TrashCan;

        counter.SetUnlockedState(Define.EUnlockedState.Hidden);
		grill.SetUnlockedState(Define.EUnlockedState.Hidden);
		firstTable.SetUnlockedState(Define.EUnlockedState.Hidden);
		secondTable.SetUnlockedState(Define.EUnlockedState.Hidden);
		office.SetUnlockedState(Define.EUnlockedState.Hidden);

        grill.StopSpawnBurger = true;

        if (_state == Define.ETutorialState.CreateFirstTable)
		{
			_uiGameScene.SetToastMessage("Create First Table");

			firstTable.SetUnlockedState(Define.EUnlockedState.ProcessingConstruction);
			yield return new WaitUntil(() => firstTable.IsUnlocked);
			_state = Define.ETutorialState.CreateBurgerMachine;
		}

        firstTable.SetUnlockedState(Define.EUnlockedState.Unlocked);

        if (_state == Define.ETutorialState.CreateBurgerMachine)
        {
            _uiGameScene.SetToastMessage("Create BurgerMachine");

            grill.SetUnlockedState(Define.EUnlockedState.ProcessingConstruction);
            yield return new WaitUntil(() => grill.IsUnlocked);
			_state = Define.ETutorialState.CreateCounter;
        }

        grill.SetUnlockedState(Define.EUnlockedState.Unlocked);

        if (_state == Define.ETutorialState.CreateCounter)
        {
            _uiGameScene.SetToastMessage("Create Counter");

            counter.SetUnlockedState(Define.EUnlockedState.ProcessingConstruction);
			yield return new WaitUntil(() => counter.IsUnlocked);
			_state = Define.ETutorialState.PickupBurger;
        }

        counter.SetUnlockedState(Define.EUnlockedState.Unlocked);
		grill.StopSpawnBurger = false;

        if (_state == Define.ETutorialState.PickupBurger)
        {
            _uiGameScene.SetToastMessage("Pickup Burger");

			yield return new WaitUntil(() => grill.CurrentWorker != null);
			_state = Define.ETutorialState.PutBurgerOnCounter;
        }

        if (_state == Define.ETutorialState.PutBurgerOnCounter)
        {
            _uiGameScene.SetToastMessage("Put Burger On Counter");

			yield return new WaitUntil(() => counter.CurrentBurgerWorker != null);
			_state = Define.ETutorialState.SellBurger;
        }

        if (_state == Define.ETutorialState.SellBurger)
        {
            _uiGameScene.SetToastMessage("Sell Burger");

            yield return new WaitUntil(() => firstTable.TableState == Define.ETableState.Reserved);
			_state = Define.ETutorialState.CleanTable;
        }

        if (_state == Define.ETutorialState.CleanTable)
        {
            _uiGameScene.SetToastMessage("");

            // 테이블 위 쓰레기 생성 대기.
			yield return new WaitUntil(() => firstTable.TableState == Define.ETableState.Dirty);

			_uiGameScene.SetToastMessage("Clean Table");

            // 테이블 위 쓰레기를 줍고.
			yield return new WaitUntil(() => firstTable.TableState != Define.ETableState.Dirty);

            // 쓰레기통에 버린다.
			yield return new WaitUntil(() => trashCan.CurrentWorker != null);
			_state = Define.ETutorialState.CreateSecondTable;
        }

        if (_state == Define.ETutorialState.CreateSecondTable)
        {
            _uiGameScene.SetToastMessage("Create Second Table");

            secondTable.SetUnlockedState(Define.EUnlockedState.ProcessingConstruction);			
			yield return new WaitUntil(() => secondTable.IsUnlocked);
			_state = Define.ETutorialState.CreateOffice;
        }

        secondTable.SetUnlockedState(Define.EUnlockedState.Unlocked);

        if (_state == Define.ETutorialState.CreateOffice)
        {
            _uiGameScene.SetToastMessage("Create Office");

            office.SetUnlockedState(Define.EUnlockedState.ProcessingConstruction);
			yield return new WaitUntil(() => office.IsUnlocked);
			_state = Define.ETutorialState.Done;
        }

        office.SetUnlockedState(Define.EUnlockedState.Unlocked);

		_uiGameScene.SetToastMessage("");

		yield return null;
    }
}
