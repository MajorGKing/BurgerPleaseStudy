using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UpgradeEmployeePopupItem : UI_SubItem
{
    private enum Texts
    {
        CostText,
    }

    private enum Buttons
    {
        PurchaseButton,
    }

    UI_Popup _parent;

    Define.EUpgradeEmployeePopupItemType _type = Define.EUpgradeEmployeePopupItemType.None;

    long _money = 0;


    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindTexts(typeof(Texts));

        GetButton((int)Buttons.PurchaseButton).gameObject.BindEvent(OnClickPurchaseButton);
        RefreshUI();
    }

    public void SetInfo(Define.EUpgradeEmployeePopupItemType type, long money, UI_Popup parent = null)
    {
        _type = type;
        _money = money;
        _parent = parent;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if(GetText((int)Texts.CostText) == null)
            return;

        GetText((int)Texts.CostText).text = Utils.GetMoneyText(_money);
    }

    private void OnClickPurchaseButton(PointerEventData evt)
    {
        if (Managers.Game.Money < _money)
            return;

        Managers.Game.Money -= _money;

        switch (_type)
        {
            case Define.EUpgradeEmployeePopupItemType.Speed:
                {
                    // TODO
                }
                break;
            case Define.EUpgradeEmployeePopupItemType.Capacity:
                {
                    // TODO
                }
                break;
            case Define.EUpgradeEmployeePopupItemType.Hire:
                {
                    Managers.Event.TriggerEvent(Define.EEventType.HireWorker);

                    if (_parent == null)
                    {
                        Managers.UI.ClosePopupUI();
                    }
                    else
                    {
                        Managers.UI.ClosePopupUI(_parent);
                    }
                }
                break;
        }
    }
}