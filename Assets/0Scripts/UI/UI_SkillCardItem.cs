using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        #region Object Bind
        gameObject.BindEvent(OnClicked);
        #endregion

        return true;
    }

    public void SetInfo()
    {

    }

    public void OnClicked()
    {

    }
}
