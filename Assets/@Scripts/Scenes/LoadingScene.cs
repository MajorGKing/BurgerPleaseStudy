using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    protected override void Awake()
    {        
        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
		{
			Debug.Log($"TODO 로딩중 : {key} {count}/{totalCount}");

			if (count == totalCount)
			{
                Managers.Data.Init();

                LoadingSceneWorks();

                base.Awake();
            }
		});
	}

	protected override void Start()
    {
        base.Start();
    }


    public override void Clear()
    {
        
    }

    private void LoadingSceneWorks()
    {
        SceneType = Define.EScene.TitleScene;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;  
        GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
        GraphicsSettings.transparencySortAxis = new Vector3(0.0f, 1.0f, 0.0f);

        Managers.Scene.LoadScene(Define.EScene.DevScene);
    }
}
