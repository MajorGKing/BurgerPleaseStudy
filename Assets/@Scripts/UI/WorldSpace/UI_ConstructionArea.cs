using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WorkerInteraction))]
public class UI_ConstructionArea : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    [SerializeField]
	TextMeshProUGUI _moneyText;

    public UnlockableBase Owner;
    public long TotalUpgradeMoney;
	public long MoneyRemaining => TotalUpgradeMoney - SpentMoney;

    public long SpentMoney
	{
		get {  return Owner.SpentMoney; }
		set { Owner.SpentMoney = value; }
	}

    public void RefreshUI()
	{
		_slider.value = SpentMoney / (float)TotalUpgradeMoney;
		_moneyText.text = Utils.GetMoneyText(MoneyRemaining);
	}
}
