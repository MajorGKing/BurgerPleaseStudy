using System.Linq;
using Data;
using TMPro;
using UnityEngine;
using static Define;

public class UI_GameScene : UI_Scene
{
    #region Enum
    enum Texts
    {
        MoneyCountText,
        ToastMessageText,
    }

    TMP_Text _moneyCountText;
    TMP_Text _toastMessageText;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        BindTexts(typeof(Texts));

        _moneyCountText = GetText((int)Texts.MoneyCountText);
        _toastMessageText = GetText((int)Texts.ToastMessageText);
    }

    public void SetInfo()
    {

    }

    private void OnEnable()
	{
		RefreshUI();
		Managers.Event.AddEvent(EEventType.MoneyChanged, RefreshUI);
	}

    private void OnDisable()
	{
		Managers.Event.RemoveEvent(EEventType.MoneyChanged, RefreshUI);
	}

	public void RefreshUI()
	{
		long money = Managers.Save.SaveData.Money;
		_moneyCountText.text = Utils.GetMoneyText(money);
	}

    public void SetToastMessage(string message)
	{
		_toastMessageText.text = message;
		_toastMessageText.enabled = (string.IsNullOrEmpty(message) == false);
	}
}