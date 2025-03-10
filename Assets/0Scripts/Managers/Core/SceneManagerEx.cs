using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.EScene type, Transform parents = null)
    {
        switch (CurrentScene.SceneType)
        {
            case Define.EScene.TitleScene:
                Managers.Clear();
                SceneManager.LoadScene(GetSceneName(type));
                break;
            case Define.EScene.GameScene:
                SceneChangeAnimation_In anim = Managers.Resource.Instantiate("SceneChangeAnimation_In").GetOrAddComponent<SceneChangeAnimation_In>();
                anim.transform.SetParent(parents);

                Time.timeScale = 1;

                anim.SetInfo(type, () =>
                {
                    Managers.Resource.Destroy(Managers.UI.SceneUI.gameObject);
                    Managers.Clear();
                    SceneManager.LoadScene(GetSceneName(type));
                });
                break;
            case Define.EScene.LobbyScene:
                SceneChangeAnimation_In anim2 = Managers.Resource.Instantiate("SceneChangeAnimation_In").GetOrAddComponent<SceneChangeAnimation_In>();
                anim2.transform.SetParent(parents);

                Time.timeScale = 1;

                anim2.SetInfo(type, () =>
                {
                    Managers.Resource.Destroy(Managers.UI.SceneUI.gameObject);
                    Managers.Clear();
                    SceneManager.LoadScene(GetSceneName(type));
                });

                break;
        }

    }

    string GetSceneName(Define.EScene type)
    {
        string name = System.Enum.GetName(typeof(Define.EScene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
