namespace YKMaze.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using YKMaze.Models;
    using YKToolkit.Bindings;

    public class MainViewModel : ViewModelBase
    {
        #region 公開プロパティ
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        public string Title
        {
            get { return ProductInfo.Instance.Title; }
        }

        private ObservableCollection<string> message = new ObservableCollection<string>();
        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        public ObservableCollection<string> Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private int vitality = 100;
        /// <summary>
        /// 体力を取得します。
        /// </summary>
        public int Vitality
        {
            get { return vitality; }
            private set { SetProperty(ref vitality, value); }
        }

        /// <summary>
        /// 現在位置を取得します。
        /// </summary>
        public MapPoint CurrentPoint
        {
            get { return Map.Instance.CurrentPoint; }
        }

        /// <summary>
        /// 現在の進行方向を取得します。
        /// </summary>
        public Direction CurrentDirection
        {
            get { return Map.Instance.CurrentDirection; }
        }

        /// <summary>
        /// 現在の迷路の状態を取得します。
        /// </summary>
        public MapStatus CurrentMapStatus
        {
            get { return Map.Instance.CurrentMapStatus; }
        }

        /// <summary>
        /// ステージ名を取得します。
        /// </summary>
        public string StageName
        {
            get { return Map.Instance.CurrentMap.Name; }
        }

        /// <summary>
        /// KeyDown イベントハンドラに対するコールバックを取得します。
        /// </summary>
        public Action<Key, ModifierKeys> KeyDownCallback
        {
            get { return KeyBindOperation; }
        }
        #endregion 公開プロパティ

        #region 公開コマンド
        private DelegateCommand nextStageCommand;
        /// <summary>
        /// 次ステージコマンドを取得します。
        /// </summary>
        public DelegateCommand NextStageCommand
        {
            get
            {
                return nextStageCommand ?? (nextStageCommand = new DelegateCommand(_ => GotoNextStage()));
            }
        }

        private DelegateCommand randomStageCommand;
        /// <summary>
        /// ランダムステージコマンドを取得します。
        /// </summary>
        public DelegateCommand RandomStageCommand
        {
            get
            {
                return randomStageCommand ?? (randomStageCommand = new DelegateCommand(_ => GotoRandomStage()));
            }
        }
        #endregion 公開コマンド

        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public MainViewModel()
        {
            Map.Instance.PropertyChanged += OnMapInstancePropertyChanged;

            GotoNextStage();
        }
        #endregion コンストラクタ

        #region private メソッド
        /// <summary>
        /// model 層の Map.Instance のプロパティ値変更イベントが発生したときに処理します。
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMapInstancePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("CurrentDirection");
            RaisePropertyChanged("CurrentMapStatus");

            StartPoint = Map.Instance.CurrentMap.StartPoint;
            GoalPoint = Map.Instance.CurrentMap.GoalPoint;
        }

        /// <summary>
        /// 次ステージへ進みます。
        /// </summary>
        private void GotoNextStage()
        {
            _isGoal = false;
            Message.Clear();
            if (Map.Instance.NextMap())
            {
                Message.Add("--- " + Map.Instance.CurrentMap.Name + " START ---");
            }
            else
            {
                Map.Instance.CurrentMap = MapItems.Stages[0];
                Message.Add("--- " + Map.Instance.CurrentMap.Name + " START ---");
            }

            DebugData = Map.Instance.CurrentMap.MapData;
        }

        /// <summary>
        /// ランダムステージへ進みます。
        /// </summary>
        private void GotoRandomStage()
        {
            _isGoal = false;
            Map.Instance.CurrentMap = MapItems.RandomStage(7, 7);
            Message.Add("--- " + Map.Instance.CurrentMap.Name + " START ---");

            DebugData = Map.Instance.CurrentMap.MapData;
        }

        /// <summary>
        /// キー操作をおこないます。
        /// </summary>
        /// <param name="key">操作されたキー</param>
        /// <param name="modifierKey">同時に押されたシステムキー</param>
        private void KeyBindOperation(Key key, ModifierKeys modifierKey)
        {
            if (Vitality <= 0)
                return;

            if (_isGoal)
            {
                return;
            }

            switch (key)
            {
                case Key.Left:
                    if (Map.Instance.Operation(MapOperation.TurnLeft))
                        WriteLine("左を向いた。");
                    break;
                case Key.Up:
                    var isMoved = Map.Instance.Operation(MapOperation.GoForward);
                    if (isMoved)
                    {
                        WriteLine("前に進んだ。");
                        if (Map.Instance.CurrentPoint == Map.Instance.CurrentMap.GoalPoint)
                        {
                            WriteLine("ゴール！");
                            _isGoal = true;
                            return;
                        }
                    }
                    else
                    {
                        WriteLine("壁にぶつかった。");
                    }

                    // 体力処理
                    Vitality--;
                    if (Vitality == 0)
                    {
                        WriteLine("体力が尽きてしまった。");
                        WriteLine("--- GAME OVER ---");
                        return;
                    }

                    // ドロップアイテム処理
                    KeyValuePair<MapPoint, ItemInfo> info = new KeyValuePair<MapPoint, ItemInfo>();
                    foreach (var itemInfo in Map.Instance.CurrentItemList)
                    {
                        if (itemInfo.Key == CurrentPoint)
                        {
                            info = itemInfo;
                            var item = info.Value;
                            WriteLine(item.PickupMessage);
                            if (item.Value > 0)
                                WriteLine("体力が " + item.Value.ToString() + " 回復した。");
                            else
                                WriteLine("体力が " + System.Math.Abs(item.Value).ToString() + " 減った。");
                            Vitality += item.Value;
                            break;
                        }
                    }
                    if (info.Value != null)
                        Map.Instance.CurrentItemList.Remove(info);
                    break;

                case Key.Right:
                    if (Map.Instance.Operation(MapOperation.TurnRight))
                        WriteLine("右を向いた。");
                    break;
                case Key.Down:
                    if (Map.Instance.Operation(MapOperation.TurnAround))
                        WriteLine("後ろを向いた。");
                    break;
            }
        }

        /// <summary>
        /// メッセージに一行追加します。
        /// </summary>
        /// <param name="message">追加するメッセージを指定します。</param>
        private void WriteLine(string message)
        {
            Message.Insert(0, message);
            if (Message.Count > 10)
                Message.RemoveAt(Message.Count - 1);
        }
        #endregion private メソッド

        #region private フィールド
        /// <summary>
        /// ゴールしたかどうかを保持する
        /// </summary>
        private bool _isGoal;
        #endregion private フィールド

        private IList<IList<bool>> debugData;
        public IList<IList<bool>> DebugData
        {
            get { return debugData; }
            set { SetProperty(ref debugData, value); }
        }

        private MapPoint startPoint;
        public MapPoint StartPoint
        {
            get { return startPoint; }
            set { SetProperty(ref startPoint, value); }
        }

        private MapPoint goalPoint;
        public MapPoint GoalPoint
        {
            get { return goalPoint; }
            set { SetProperty(ref goalPoint, value); }
        }
    }
}
