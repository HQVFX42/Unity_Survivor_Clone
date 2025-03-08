# Unity_Survivor_Clone

## Addressable
- Resource
  ```csharp
    #region Addressable
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        // Check cashed resources
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;
        if (key.Contains(".sprite"))
        {
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";
        }

        // Load async resource
        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
    #endregion
  ```

## UI
### Before
- Find
  ```csharp
    private readonly string PopUpPurchasePrefabPath = "lobbyitem/popup_shop_purchase";
    private readonly string PopUpShortagePrefabPath = "lobbyitem/popup_shop_shortage";
    private readonly string PopUpShortageItemPrefabPath = "lobbyitem/shop_item/shortage_item";
    private readonly string PopUpPlatinumChestInfoPrefabPath = "lobbyitem/popup_shop_Sequipinfo";

    private readonly string CloseButtonPath = "panel/btn_x";
    private readonly string PopUpPurchaseButtonPath = "panel/content/bottom/btn_purchase";
    private readonly string SpecialPackDetailsButtonPath = "content/left/btn_details";
    private readonly string OpenOneButtonPath = "content/right/btn/btn_open_1";
    private readonly string OpenTenButtonPath = "content/right/btn/btn_open_10";
    private readonly string BoxNormalADButtonPath = "content/item_01/btn/btn_ad";
    private readonly string BoxNormalOpenButtonPath = "content/item_01/btn/btn_open_1";
    private readonly string BoxLuxuryADButtonPath = "content/item_02/btn/btn_ad";
    private readonly string BoxLuxuryOpenButtonPath = "content/item_02/btn/btn_open_1";

    private readonly Dictionary<EPDType, string> ShopItemPaths = new Dictionary<EPDType, string>()
    {
        { EPDType.STARTER, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/starters_pack"},
        { EPDType.PACKAGE, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/package" },
        { EPDType.DAILY, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/daily_shop" },
        { EPDType.SPECIAL, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/platinum_chest" },
        { EPDType.BOX, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/platinum_chest" },
        { EPDType.GEM, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/gem" },
        { EPDType.GOLD, "SafeArena/Viewport/Content/shop/bg/Scroll View/Viewport/Content/gold" },
    };

    private Button SpecialPackDetailsButton;
    private Button SpecialPackOneButton;
    private Button SpecialPackTenButton;
    private Button BoxNormalADButton;
    private Button BoxNormalOpenButton;
    private Button BoxLuxuryADButton;
    private Button BoxLuxuryOpenButton;
  ```
### After
- Bind
  ```csharp
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }

    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindText(Type type) { Bind<TMP_Text>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindToggle(Type type) { Bind<Toggle>(type); }
  ```
- Get
  ```csharp
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Toggle GetToggle(int idx) { return Get<Toggle>(idx); }
  ```
- Events
  ```csharp
    public static void BindEvent(GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Define.EUIEvent type = Define.EUIEvent.Click)
    {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.EUIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.EUIEvent.Preseed:
                evt.OnPressedHandler -= action;
                evt.OnPressedHandler += action;
                break;
            case Define.EUIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
            case Define.EUIEvent.PointerUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
            case Define.EUIEvent.Drag:
                evt.OnDragHandler -= dragAction;
                evt.OnDragHandler += dragAction;
                break;
            case Define.EUIEvent.BeginDrag:
                evt.OnBeginDragHandler -= dragAction;
                evt.OnBeginDragHandler += dragAction;
                break;
            case Define.EUIEvent.EndDrag:
                evt.OnEndDragHandler -= dragAction;
                evt.OnEndDragHandler += dragAction;
                break;
        }
    }
  ```
  
- Enum
  ```csharp
    enum GameObjects
    {
        ContentObject,
        ResultRewardScrollContentObject,
        ResultGoldObject,
        ResultKillObject,
    }
    
    enum Texts
    {
        GameResultPopupTitleText,
        ResultStageValueText,
        ResultSurvivalTimeText,
        ResultSurvivalTimeValueText,
        ResultKillValueText,
        ConfirmButtonText,
        ResultGoldValueText
    }
    
    enum Buttons
    {
        StatisticsButton,
        ConfirmButton,
    }
  ```
