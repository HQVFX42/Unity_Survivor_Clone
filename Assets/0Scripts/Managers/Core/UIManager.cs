using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    UI_Base _sceneUI;
    Stack<UI_Base> _uiStack = new Stack<UI_Base>();

    public T GetSceneUI<T>() where T : UI_Base
    {
        return _sceneUI as T;
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
        _uiStack.Push(ui);

        return ui;
    }

    public void ClosePopup(UI_Base popup)
    {
        if (_uiStack.Count == 0)
        {
            return;
        }

        UI_Base ui = _uiStack.Pop();
        Managers.Resource.Destroy(ui.gameObject);
    }
}
