using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

public class UIManager
{
    UI_Base _sceneUI;
    Stack<UI_Base> _popupStack = new Stack<UI_Base>();

    public T GetSceneUI<T>() where T : UI_Base
    {
        return _sceneUI as T;
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null, bool pooling = true) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{name}", parent, pooling);
        go.transform.SetParent(parent);
        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>() where T : UI_Base
    {
        if (_sceneUI != null)
        {
            return GetSceneUI<T>();
        }

        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling: true).GetOrAddComponent<T>();
        _sceneUI = ui;

        return ui;
    }

    public T ShowPopup<T>() where T : UI_Base
    {
        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling: true).GetOrAddComponent<T>();
        _popupStack.Push(ui);

        return ui;
    }

    public void ClosePopup(UI_Base popup)
    {
        if (_popupStack.Count == 0)
        {
            return;
        }

        UI_Base ui = _popupStack.Pop();
        Managers.Resource.Destroy(ui.gameObject);
    }

    public void RefreshTimeScale()
    {
        if (SceneManager.GetActiveScene().name != Define.EScene.GameScene.ToString())
        {
            Time.timeScale = 1;
            return;
        }

        if (_popupStack.Count > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        //DOTween.timeScale = 1;
        //OnTimeScaleChanged?.Invoke((int)Time.timeScale);
    }
}
