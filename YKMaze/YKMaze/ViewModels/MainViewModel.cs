namespace YKMaze.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows.Input;
    using YKMaze.Models;
    using YKToolkit.Bindings;

    public class MainViewModel : ViewModelBase
    {
        #region コンストラクタ
        public MainViewModel()
        {
            _mapInstance.CurrentPointChanged += OnMapStatusChanged;
            _mapInstance.CurrentDirectionChanged += OnMapStatusChanged;
            _mapInstance.CurrentMapChanged += OnCurrentMapChanged;

            ClearMessage();
        }
        #endregion コンストラクタ

        #region private フィールド
        /// <summary>
        /// 迷路情報のインスタンス
        /// </summary>
        private Map _mapInstance = Map.Instance;

        /// <summary>
        /// 通常プレイ用バイタリティ初期値
        /// </summary>
        private int _normalVitality = 100;

        /// <summary>
        /// エンドレスプレイ用バイタリティ初期値
        /// </summary>
        private int _endlessVitality = 1000;

        /// <summary>
        /// アイテムリスト
        /// </summary>
        private List<KeyValuePair<MapPoint, ItemInfo>> _itemData;

        /// <summary>
        /// エンドレスプレイモードかどうかを確認する
        /// </summary>
        private bool _isEndlessMode;

        /// <summary>
        /// エンドレスプレイモードのステージ数
        /// </summary>
        private int _endlessModeStageNumber;

        /// <summary>
        /// キー操作履歴
        /// </summary>
        private List<Key> _keyHistory;

        /// <summary>
        /// キー操作履歴実行用リスト
        /// </summary>
        private List<Key> _keyHistoryExec;

        /// <summary>
        /// 取得アイテム履歴
        /// </summary>
        private List<MapPoint> _itemHistory;
        #endregion private フィールド

        #region 公開プロパティ
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        public string Title
        {
            get { return ProductInfo.Instance.Title; }
        }

        #region メニュー
        private bool _isMenuEnabled = true;
        /// <summary>
        /// トップメニューが有効かどうかを取得します。
        /// </summary>
        public bool IsMenuEnabled
        {
            get { return _isMenuEnabled; }
            private set
            {
                if (SetProperty(ref _isMenuEnabled, value))
                {
                    if (_isMenuEnabled)
                    {
                        IsEndMenuEnabled = false;
                        IsGoalMenuEnabled = false;
                        IsViewHistoryEnabled = false;
                    }
                }
            }
        }

        private bool _isEndMenuEnabled;
        /// <summary>
        /// 終了メニューが有効かどうかを取得します。
        /// </summary>
        public bool IsEndMenuEnabled
        {
            get { return _isEndMenuEnabled; }
            private set
            {
                if (SetProperty(ref _isEndMenuEnabled, value))
                {
                    if (_isEndMenuEnabled)
                    {
                        IsMenuEnabled = false;
                        IsGoalMenuEnabled = false;
                        IsViewHistoryEnabled = false;
                    }
                }
            }
        }

        private bool _isGoalMenuEnabled;
        /// <summary>
        /// ゴールメニューが有効かどうかを取得します。
        /// </summary>
        public bool IsGoalMenuEnabled
        {
            get { return _isGoalMenuEnabled; }
            private set
            {
                if (SetProperty(ref _isGoalMenuEnabled, value))
                {
                    if (_isGoalMenuEnabled)
                    {
                        IsMenuEnabled = false;
                        IsEndMenuEnabled = false;
                    }
                }
            }
        }

        private bool _isViewHistoryEnabled;
        /// <summary>
        /// 軌跡表示が有効かどうかを取得します。
        /// </summary>
        public bool IsViewHistoryEnabled
        {
            get { return _isViewHistoryEnabled; }
            set { SetProperty(ref _isViewHistoryEnabled, value); }
        }
        #endregion メニュー

        /// <summary>
        /// KeyDown イベントハンドラに対するコールバックを取得します。
        /// </summary>
        public Action<Key, ModifierKeys> KeyDownCallback
        {
            get { return KeyBindOperation; }
        }

        #region 現在の状態
        /// <summary>
        /// 現在位置を取得します。
        /// </summary>
        public MapPoint CurrentPoint
        {
            get { return _mapInstance.CurrentPoint; }
        }

        /// <summary>
        /// 現在の進行方向を取得します。
        /// </summary>
        public Direction CurrentDirection
        {
            get { return _mapInstance.CurrentDirection; }
        }

        private MapStatus _currentMapStatus = MapStatus.Stop_None_Left_None_Right_None;
        /// <summary>
        /// 現在位置の周囲の状態を取得します。
        /// </summary>
        public MapStatus CurrentMapStatus
        {
            get { return _currentMapStatus; }
            private set { SetProperty(ref _currentMapStatus, value); }
        }

        public IReadOnlyList<IReadOnlyList<bool>> CurrentMapData
        {
            get { return _mapInstance.CurrentMap != null ? _mapInstance.CurrentMap.MapData : null; }
        }
        #endregion 現在の状態

        /// <summary>
        /// スタート地点を取得します。
        /// </summary>
        public MapPoint StartPoint
        {
            get { return _mapInstance.CurrentMap != null ? _mapInstance.CurrentMap.StartPoint : new MapPoint(); }
        }

        /// <summary>
        /// ゴール地点を取得します。
        /// </summary>
        public MapPoint GoalPoint
        {
            get { return _mapInstance.CurrentMap != null ? _mapInstance.CurrentMap.GoalPoint : new MapPoint(); }
        }

        /// <summary>
        /// 開始時の進行方向を取得します。
        /// </summary>
        public Direction StartDirection
        {
            get { return _mapInstance.CurrentMap != null ? _mapInstance.CurrentMap.StartDirection : Direction.Left; }
        }

        private MapPoint _currentHistoryPoint;
        /// <summary>
        /// 軌跡表示の現在位置を取得します。
        /// </summary>
        public MapPoint CurrentHistoryPoint
        {
            get { return _currentHistoryPoint; }
            private set { SetProperty(ref _currentHistoryPoint, value); }
        }

        private Direction _currentHistoryDirection;
        /// <summary>
        /// 軌跡表示の現在の進行方向を取得します。
        /// </summary>
        public Direction CurrentHistoryDirection
        {
            get { return _currentHistoryDirection; }
            private set { SetProperty(ref _currentHistoryDirection, value); }
        }

        private List<MapPoint> _itemHistoryExec;
        /// <summary>
        /// 取得アイテム履歴実行用リストを取得します。
        /// </summary>
        public List<MapPoint> ItemHistoryExec
        {
            get { return _itemHistoryExec; }
            private set { SetProperty(ref _itemHistoryExec, value); }
        }

        private ObservableCollection<string> _message;
        /// <summary>
        /// メッセージを取得します。
        /// </summary>
        public ObservableCollection<string> Message
        {
            get { return _message; }
            private set { SetProperty(ref _message, value); }
        }

        private int _vitality;
        /// <summary>
        /// 体力を取得します。
        /// </summary>
        public int Vitality
        {
            get { return _vitality; }
            private set { SetProperty(ref _vitality, value); }
        }

        private int _curry;
        /// <summary>
        /// カレー粉所持数を取得します。
        /// </summary>
        public int Curry
        {
            get { return _curry; }
            private set { SetProperty(ref _curry, value); }
        }
        #endregion 公開プロパティ

        #region 公開コマンド
        private DelegateCommand normalPlayCommand;
        /// <summary>
        /// 通常プレイ開始コマンドを取得します。
        /// </summary>
        public DelegateCommand NormalPlayCommand
        {
            get
            {
                return normalPlayCommand ?? (normalPlayCommand = new DelegateCommand(_ => StartNormalPlay()));
            }
        }

        private DelegateCommand endlessPlayCommand;
        /// <summary>
        /// エンドレスプレイ開始コマンドを取得します。
        /// </summary>
        public DelegateCommand EndlessPlayCommand
        {
            get
            {
                return endlessPlayCommand ?? (endlessPlayCommand = new DelegateCommand(_ => StartEndlessPlay()));
            }
        }

        private DelegateCommand gotoNextStageCommand;
        /// <summary>
        /// 次ステージ開始コマンドを取得します。
        /// </summary>
        public DelegateCommand GotoNextStageCommand
        {
            get
            {
                return gotoNextStageCommand ?? (gotoNextStageCommand = new DelegateCommand(_ => GotoNextStage()));
            }
        }

        private DelegateCommand viewHistoryCommand;
        /// <summary>
        /// 軌跡表示コマンドを取得します。
        /// </summary>
        public DelegateCommand ViewHistoryCommand
        {
            get
            {
                return viewHistoryCommand ?? (viewHistoryCommand = new DelegateCommand(_ => ViewHistory()));
            }
        }

        private DelegateCommand topMenuCommand;
        /// <summary>
        /// トップメニューに戻るコマンドを取得します。
        /// </summary>
        public DelegateCommand TopMenuCommand
        {
            get
            {
                return topMenuCommand ?? (topMenuCommand = new DelegateCommand(_ =>
                {
                    IsMenuEnabled = true;
                }));
            }
        }
        #endregion 公開コマンド

        #region プレイ開始
        /// <summary>
        /// 通常プレイを開始します。
        /// </summary>
        private void StartNormalPlay()
        {
            _isEndlessMode = false;
            Vitality = _normalVitality;
            _mapInstance.CurrentMap = null;
            GotoNextStage();
        }

        /// <summary>
        /// エンドレスプレイを開始します。
        /// </summary>
        private void StartEndlessPlay()
        {
            _endlessModeStageNumber = 0;
            _isEndlessMode = true;
            Vitality = _endlessVitality;
            GotoNextStage();
        }

        /// <summary>
        /// 次ステージを開始します。
        /// </summary>
        private void GotoNextStage()
        {
            if (_isEndlessMode)
            {
                _mapInstance.SetRandomMap();
                WriteLine("--- Random Stage{0} START ---", ++_endlessModeStageNumber);
            }
            else
            {
                _mapInstance.NextMap();
                WriteLine("--- {0} START ---", _mapInstance.CurrentMap.Name);
            }

            _keyHistory = new List<Key>();
            _itemHistory = new List<MapPoint>();
            CloseMenu();
        }
        #endregion プレイ開始

        #region キー操作
        /// <summary>
        /// キー操作をおこないます。
        /// </summary>
        /// <param name="key">操作されたキー</param>
        /// <param name="modifierKey">同時に押されたシステムキー</param>
        private void KeyBindOperation(Key key, ModifierKeys modifierKey)
        {
            if (IsMenuEnabled || IsEndMenuEnabled || IsGoalMenuEnabled)
                return;

            switch (key)
            {
                case Key.Left:
                    _keyHistory.Add(key);
                    _mapInstance.Operation(MapOperation.TurnLeft);
                    WriteLine("ひだり");
                    break;

                case Key.Up:
                    _keyHistory.Add(key);
                    if (_mapInstance.Operation(MapOperation.GoForward))
                        WriteLine("まっすぐ");
                    else
                        WriteLine("壁にぶつかった！");
                    Vitality--;

                    if (PreGoal == 0)
                    {
                        WriteLine("ゴール！");
                        // ゴールボーナス取得
                        if (_mapInstance.CurrentMap.GoalBonus != null)
                        {
                            WriteLine(_mapInstance.CurrentMap.GoalBonus.PickupMessage);
                            Vitality += _mapInstance.CurrentMap.GoalBonus.Value;
                        }
                    }
                    else
                    {
                        // ドロップアイテム取得
                        TryGetItem();

                        if (Vitality == 0)
                        {
                            WriteLine("体力が尽きてしまった。");
                            GameOver();
                        }
                    }
                    break;

                case Key.Right:
                    _keyHistory.Add(key);
                    _mapInstance.Operation(MapOperation.TurnRight);
                    WriteLine("みぎ");
                    break;

                case Key.Down:
                    _keyHistory.Add(key);
                    _mapInstance.Operation(MapOperation.TurnAround);
                    WriteLine("うしろ");
                    break;
            }
        }

        /// <summary>
        /// ドロップアイテム取得を試みる
        /// </summary>
        private void TryGetItem()
        {
            if (_itemData == null)
                return;

            KeyValuePair<MapPoint, ItemInfo>? item = null;
            foreach (var pair in _itemData)
            {
                if (pair.Key == CurrentPoint)
                {
                    var itemInfo = pair.Value;
                    WriteLine(itemInfo.PickupMessage);
                    if (itemInfo.IsCurry)
                    {
                        Curry += itemInfo.Value;
                    }
                    else
                    {
                        if (itemInfo.Value < 0)
                        {
                            if (Curry > 0)
                            {
                                WriteLine("カレー粉を落としてしまった。");
                                Curry--;
                            }
                            Vitality += itemInfo.Value;
                        }
                        else if (Curry > 0)
                        {
                            WriteLine("カレー粉でうまさ 3 倍！");
                            Curry--;
                            Vitality += 3 * itemInfo.Value;
                        }
                        else
                        {
                            Vitality += itemInfo.Value;
                        }
                    }

                    item = pair;
                    break;
                }
            }
            if (item != null)
            {
                _itemData.Remove(item.Value);
                // 履歴を残す
                _itemHistory.Add(item.Value.Key);
            }
        }
        #endregion キー操作

        #region イベントハンドラ
        /// <summary>
        /// Model 層の現在の迷路変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCurrentMapChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentMapData");
            RaisePropertyChanged("StartPoint");
            RaisePropertyChanged("GoalPoint");
            RaisePropertyChanged("StartDirection");

            UpdateCurrentStatus();

            // アイテムリスト更新
            if (_mapInstance.CurrentMap != null)
                _itemData = _mapInstance.CurrentMap.ItemData as List<KeyValuePair<MapPoint, ItemInfo>>;
        }

        /// <summary>
        /// Model 層の迷路の状態変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMapStatusChanged(object sender, EventArgs e)
        {
            UpdateCurrentStatus();
        }

        /// <summary>
        /// 各種状態を更新する
        /// </summary>
        private void UpdateCurrentStatus()
        {
            RaisePropertyChanged("CurrentPoint");
            RaisePropertyChanged("CurrentDirection");
            CurrentMapStatus = _mapInstance.CurrentMapStatus;

#if DEBUG
            RaisePropertyChanged("StartPoint");
            RaisePropertyChanged("GoalPoint");
#endif

            if (_mapInstance.CurrentMap == null)
                return;

            // ゴールまでの直線距離 (壁がある場合は NG とする)
            var pre = -1;
            if (GoalPoint == CurrentPoint)
            {
                pre = 0;
            }
            else if (GoalPoint == CurrentPoint.Offset(0, 1, CurrentDirection))
            {
                pre = 1;
            }
            else
            {
                // 一つ先が道の場合
                if (_mapInstance.CurrentMap.CheckPath(CurrentPoint.Offset(0, 1, CurrentDirection)))
                {
                    // 二つ先がゴールかどうか確認する
                    if (_mapInstance.CurrentMap.GoalPoint == CurrentPoint.Offset(0, 2, CurrentDirection))
                        pre = 2;
                }
            }
            PreGoal = pre;

            // ゴールまでの歩数
            LeftStep = System.Math.Abs(CurrentPoint.X - GoalPoint.X)
                     + System.Math.Abs(CurrentPoint.Y - GoalPoint.Y);

            // アイテムまでの距離 (壁がある場合は NG とする)
            pre = -1;
            var pt = CurrentPoint.Offset(0, 1, CurrentDirection);
            if (_mapInstance.CurrentMap.CheckPath(pt))
            {
                foreach (var info in _mapInstance.CurrentMap.ItemData)
                {
                    if (pt == info.Key)
                    {
                        pre = 1;
                        break;
                    }
                }
                if (pre < 0)
                {
                    pt = pt.Offset(0, 1, CurrentDirection);
                    foreach (var info in _mapInstance.CurrentMap.ItemData)
                    {
                        if (pt == info.Key)
                        {
                            pre = 2;
                            break;
                        }
                    }
                }
            }
            PreItem = pre;
        }
        #endregion イベントハンドラ

        #region ゴール表示
        private int preGoal = -1;
        /// <summary>
        /// ゴールまでの直線距離を取得します。
        /// </summary>
        public int PreGoal
        {
            get { return preGoal; }
            private set
            {
                if (SetProperty(ref preGoal, value))
                {
                    if (preGoal == 0)
                    {
                        // ゴール！
                        IsGoalMenuEnabled = true;
                    }
                }
            }
        }

        private int leftStep;
        /// <summary>
        /// ゴールまでの歩数を取得します。
        /// </summary>
        public int LeftStep
        {
            get { return leftStep; }
            private set { SetProperty(ref leftStep, value); }
        }
        #endregion ゴール表示

        #region アイテム表示
        private int preItem = -1;
        /// <summary>
        /// アイテムまでの距離を取得します。
        /// </summary>
        public int PreItem
        {
            get { return preItem; }
            private set { SetProperty(ref preItem, value); }
        }
        #endregion アイテム表示

        #region メッセージ関連
        private int _messageCount;

        /// <summary>
        /// メッセージを一行追加します。
        /// </summary>
        /// <param name="format">追加するメッセージのフォーマットを指定します。</param>
        /// <param name="args">フォーマットに対するオブジェクト</param>
        private void WriteLine(string format, params object[] args)
        {
            var str = string.Format(format, args);
            Message.Insert(0, string.Format("[{0}] {1}", _messageCount++, str));
            Message.RemoveAt(Message.Count - 1);
        }

        /// <summary>
        /// メッセージをクリアします。
        /// </summary>
        private void ClearMessage()
        {
            Message = new ObservableCollection<string>()
            {
                "", "", "", "", "", "", "", "", "", "",
            };

            _messageCount = 0;
        }
        #endregion メッセージ関連

        #region 軌跡表示
        private Timer _viewHistoryTimer;

        /// <summary>
        /// 軌跡表示を開始します。
        /// </summary>
        private async void ViewHistory()
        {
            CloseMenu();

            _keyHistoryExec = new List<Key>();
            foreach (var key in _keyHistory)
                _keyHistoryExec.Add(key);

            var list = new List<MapPoint>();
            foreach (var pt in _itemHistory)
                list.Add(pt);
            ItemHistoryExec = list;

            CurrentHistoryPoint = StartPoint;
            CurrentHistoryDirection = StartDirection;

            IsViewHistoryEnabled = true;

            // 非同期で軌跡表示をおこなう
            await ViewHistoryAsync();

        }

        private Task ViewHistoryAsync()
        {
            return Task.Run(() =>
            {
                _viewHistoryTimer = new Timer(400);
                _viewHistoryTimer.Elapsed += OnViewHistoryTimer;
                _viewHistoryTimer.Start();
            });
        }

        private void OnViewHistoryTimer(object sender, ElapsedEventArgs e)
        {
            if (_keyHistoryExec.Count == 0)
            {
                _viewHistoryTimer.Elapsed -= OnViewHistoryTimer;
                _viewHistoryTimer.Stop();
                _viewHistoryTimer.Dispose();
                _viewHistoryTimer = null;
                IsGoalMenuEnabled = true;
            }
            else
            {
                var key = _keyHistoryExec[0];
                _keyHistoryExec.RemoveAt(0);

                switch (key)
                {
                    case Key.Left:
                        CurrentHistoryDirection = _mapInstance.OperateDirection(CurrentHistoryDirection, MapOperation.TurnLeft);
                        break;

                    case Key.Up:
                        if (_mapInstance.CurrentMap.CheckPath(CurrentHistoryPoint.Offset(0, 1, CurrentHistoryDirection)))
                            CurrentHistoryPoint = CurrentHistoryPoint.Offset(0, 1, CurrentHistoryDirection);
                        break;

                    case Key.Right:
                        CurrentHistoryDirection = _mapInstance.OperateDirection(CurrentHistoryDirection, MapOperation.TurnRight);
                        break;

                    case Key.Down:
                        CurrentHistoryDirection = _mapInstance.OperateDirection(CurrentHistoryDirection, MapOperation.TurnAround);
                        break;
                }
            }
        }
        #endregion 軌跡表示

        #region その他 private メソッド
        /// <summary>
        /// ゲームオーバーにします。
        /// </summary>
        private void GameOver()
        {
            IsEndMenuEnabled = true;
        }

        /// <summary>
        /// メニューを閉じます。
        /// </summary>
        private void CloseMenu()
        {
            IsMenuEnabled = false;
            IsEndMenuEnabled = false;
            IsGoalMenuEnabled = false;
            IsViewHistoryEnabled = false;
        }
        #endregion その他 private メソッド
    }
}
