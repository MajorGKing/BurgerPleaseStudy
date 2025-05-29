using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
		{
			Debug.Log($"TODO 로딩중 : {key} {count}/{totalCount}");

			if (count == totalCount)
			{

                Managers.Data.Init();
            }
		});
    }
}
