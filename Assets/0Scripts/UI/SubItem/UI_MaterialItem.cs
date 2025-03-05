using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MaterialItem : UI_Base
{
    #region UI 기능 리스트
    // 정보 갱신
    // MaterialItemImage : 재료 아이템 아이콘
    // ItemCountValueText : 재료 아이콘 개수
    #endregion

    #region Enum

    enum GameObjects
    {
        GetEffectObject,
    }
    enum Buttons
    {
        MaterialInfoButton,
    }

    enum Texts
    {
        ItemCountValueText
    }

    enum Images
    {
        MaterialItemImage,
        MaterialItemBackgroundImage,
    }

    #endregion

    Data.MaterialData _materialData;
    Transform _makeSubItemParents;
    ScrollRect _scrollRect;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));


        GetObject((int)GameObjects.GetEffectObject).SetActive(false);

        GetButton((int)Buttons.MaterialInfoButton).gameObject.BindEvent(null, OnDrag, Define.EUIEvent.Drag);
        GetButton((int)Buttons.MaterialInfoButton).gameObject.BindEvent(null, OnBeginDrag, Define.EUIEvent.BeginDrag);
        GetButton((int)Buttons.MaterialInfoButton).gameObject.BindEvent(null, OnEndDrag, Define.EUIEvent.EndDrag);
        gameObject.BindEvent(OnClickMaterialInfoButton);
        GetButton((int)Buttons.MaterialInfoButton).gameObject.BindEvent(OnClickMaterialInfoButton);

        return true;
    }

    public void SetInfo(string spriteName, int count)
    {
        transform.localScale = Vector3.one;
        GetImage((int)Images.MaterialItemImage).sprite = Managers.Resource.Load<Sprite>(spriteName);
        GetImage((int)Images.MaterialItemBackgroundImage).color = EquipmentUIColors.Epic;
        GetText((int)Texts.ItemCountValueText).text = $"{count}";
        GetObject((int)GameObjects.GetEffectObject).SetActive(true);

    }

    ///<summary> tooltip </summary>
    void OnClickMaterialInfoButton()
    {
        //Managers.Sound.PlayButtonClick();
        //UI_ToolTipItem item = Managers.UI.MakeSubItem<UI_ToolTipItem>(_makeSubItemParents);
        //item.transform.localScale = Vector3.one;
        //RectTransform targetPos = this.gameObject.GetComponent<RectTransform>();
        //RectTransform parentsCanvas = _makeSubItemParents.gameObject.GetComponent<RectTransform>();
        //item.SetInfo(_materialData, targetPos, parentsCanvas);
        //item.transform.SetAsLastSibling();
    }

    #region Scroll
    public void OnDrag(BaseEventData baseEventData)
    {
        if (_scrollRect == null)
        {
            return;
        }
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        _scrollRect.OnDrag(pointerEventData);
    }

    public void OnBeginDrag(BaseEventData baseEventData)
    {
        if (_scrollRect == null)
        {
            return;
        }
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        _scrollRect.OnBeginDrag(pointerEventData);
    }

    public void OnEndDrag(BaseEventData baseEventData)
    {
        if (_scrollRect == null)
        {
            return;
        }
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        _scrollRect.OnEndDrag(pointerEventData);
    }
    #endregion
}
