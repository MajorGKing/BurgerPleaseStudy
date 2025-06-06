using System;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Define
{
    public const char MAP_TOOL_WALL = '0';
    public const char MAP_TOOL_NONE = '1';

    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene,
    }

    public enum ESound
    {
        Bgm,
        SubBgm,
        Effect,
        Max,
    }

    public enum ETouchEvent
    {
        PointerUp,
        PointerDown,
        Click,
        Pressed,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum ELanguage
    {
        Korean,
        English,
        French,
        SimplifiedChinese,
        TraditionalChinese,
        Japanese,
    }

    public enum ELayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Dummy1 = 3,
        Water = 4,
        UI = 5,
        Hero = 6,
        Monster = 7,
        Boss = 8,
        //
        Env = 11,
        Obstacle = 12,
        //
        Projectile = 20,
    }

    public const float GRILL_SPAWN_BURGER_INTERVAL = 0.5f;
    public const int GRILL_MAX_BURGER_COUNT = 20;

    public const float CONSTRUCTION_UPGRADE_INTERVAL = 0.01f;
    public const float MONEY_SPAWN_INTERVAL = 0.1f;
    public const float TRASH_SPAWN_INTERVAL = 0.1f;
    public const float GUEST_SPAWN_INTERVAL = 1f;
    public const int GUEST_MAX_ORDER_BURGER_COUNT = 2;

    public static Vector3 WORKER_SPAWN_POS = new Vector3(0, 0, 0);
    public static Vector3 GUEST_LEAVE_POS = new Vector3(0, 0, 0);

    public static int IDLE = Animator.StringToHash("Idle");
    public static int MOVE = Animator.StringToHash("Move");
    public static int SERVING_IDLE = Animator.StringToHash("ServingIdle");
    public static int SERVING_MOVE = Animator.StringToHash("ServingMove");
    public static int EATING = Animator.StringToHash("Eating");

    public enum EEventType
	{
		MoneyChanged,
		HireWorker,
		UnlockProp,

		MaxCount
	}

    public enum EAnimState
    {
        None,
        Idle,
        Move,
        Eating,
    }

    public enum EObjectType
    {
        None,
        Trash,
        Burger,
        Money,
    }

    public enum EGuestState
    {
        None,
        Queuing,
        Serving,
        Eating,
        Leaving,
    }

    public enum ETableState
    {
        None,
        Reserved,
        Eating,
        Dirty,
    }

    public enum EUnlockedState
    {
        Hidden,
        ProcessingConstruction,
        Unlocked
    }

    public enum ETutorialState
    {
        None,
        // CreateDoor
        CreateFirstTable,
        CreateBurgerMachine,
        CreateCounter,
        PickupBurger,
        PutBurgerOnCounter,
        SellBurger,
        CleanTable,
        CreateSecondTable,
        // OpenDistrictArea
        CreateOffice,

        Done,
    }

    public enum EMainCounterJob
	{
		MoveBurger,
		CounterCashier,
		CleanTable,

		MaxCount,
	}

    #region PrefabName
    public const string BURGEROBJECT = "Burger";
    public const string MONEYOBJECT = "Money";
    public const string TRASHOBJECT = "Trash";
    public const string TRASHCANOBJECT = "TrashCan";
    public const string GUEST = "Guest";
    public const string WORKER = "Worker";

    #endregion

    public enum EUpgradeEmployeePopupItemType
    {
        None,
        Speed,
        Capacity,
        Hire
    }
}