namespace YKMaze.Models
{
    using System;
    using System.Collections.Generic;

    public class Map
    {
        #region Singleton
        private static readonly Map instance = new Map();
        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        public static Map Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        private Map()
        {
        }
        #endregion Singleton

        #region 公開プロパティ
        private MapPoint currentPoint;
        /// <summary>
        /// 現在位置を取得します。
        /// </summary>
        public MapPoint CurrentPoint
        {
            get { return currentPoint; }
            private set
            {
                if (currentPoint != value)
                {
                    currentPoint = value;

                    // MapStatus を更新する
                    CurrentMapStatus = CurrentMap.GetMapStatus(currentPoint, CurrentDirection);

                    RaiseCurrentPointChanged();
                }
            }
        }

        private Direction currentDirection;
        /// <summary>
        /// 現在の進行方向を取得します。
        /// </summary>
        public Direction CurrentDirection
        {
            get { return currentDirection; }
            private set
            {
                if (currentDirection != value)
                {
                    currentDirection = value;
                    CurrentMapStatus = CurrentMap.GetMapStatus(CurrentPoint, currentDirection);

                    RaiseCurrentDirectionChanged();
                }
            }
        }

        /// <summary>
        /// 現在の迷路の状態を取得します。
        /// </summary>
        public MapStatus CurrentMapStatus { get; private set; }

        /// <summary>
        /// 現在のアイテムリストを取得します。
        /// </summary>
        public IReadOnlyList<KeyValuePair<MapPoint, ItemInfo>> CurrentItemList { get; private set; }

        private MapInfo currentMap;
        /// <summary>
        /// 現在の迷路を取得または設定します。
        /// </summary>
        public MapInfo CurrentMap
        {
            get { return currentMap; }
            set
            {
                if (currentMap != value)
                {
                    currentMap = value;
                    if (currentMap != null)
                    {
                        CurrentPoint = currentMap.StartPoint;
                        CurrentDirection = currentMap.StartDirection;
                        CurrentItemList = currentMap.ItemData;
                    }

                    RaiseCurrentMapChanged();
                }
            }
        }
        #endregion 公開プロパティ

        #region イベント定義
        /// <summary>
        /// CurrentPoint プロパティ値変更時に発生します。
        /// </summary>
        public event EventHandler<EventArgs> CurrentPointChanged;

        /// <summary>
        /// CurrentDirection プロパティ値変更時に発生します。
        /// </summary>
        public event EventHandler<EventArgs> CurrentDirectionChanged;

        /// <summary>
        /// CurrentMap プロパティ値変更時に発生します。
        /// </summary>
        public event EventHandler<EventArgs> CurrentMapChanged;

        /// <summary>
        /// CurrentPointChanged イベントを発行します。
        /// </summary>
        private void RaiseCurrentPointChanged()
        {
            var h = CurrentPointChanged;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        /// <summary>
        /// CurrentDirectionChanged イベントを発行します。
        /// </summary>
        private void RaiseCurrentDirectionChanged()
        {
            var h = CurrentDirectionChanged;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        /// <summary>
        /// CurrentMapChanged イベントを発行します。
        /// </summary>
        private void RaiseCurrentMapChanged()
        {
            var h = CurrentMapChanged;
            if (h != null)
                h(this, EventArgs.Empty);
        }
        #endregion イベント定義

        #region 公開メソッド
        /// <summary>
        /// 初めの迷路をロードします。
        /// </summary>
        public void SetFirstMap()
        {
            CurrentMap = MapItems.Stages[0];
        }
        /// <summary>
        /// 次の迷路をロードします。
        /// </summary>
        /// <returns>次の迷路がある場合に true を返します。</returns>
        public bool NextMap()
        {
            if (CurrentMap == null)
            {
                CurrentMap = MapItems.Stages[0];
                return true;
            }

            var isCurrent = false;
            foreach (var mapInfo in MapItems.Stages)
            {
                if (isCurrent)
                {
                    CurrentMap = mapInfo;
                    return true;
                }
                else
                {
                    if (CurrentMap.Name == mapInfo.Name)
                        isCurrent = true;
                }
            }

            return false;
        }

        /// <summary>
        /// ランダムな迷路をロードします。
        /// </summary>
        public void SetRandomMap(int width = -1, int height = -1)
        {
            CurrentMap = MapItems.RandomStage(width, height);
        }

        /// <summary>
        /// 迷路に対して操作をおこないます。
        /// </summary>
        /// <param name="operation">実行する操作を指定します。</param>
        public bool Operation(MapOperation operation)
        {
            switch (operation)
            {
                case MapOperation.GoForward:
                    return MoveCurrentPoint(CurrentDirection);

                case MapOperation.TurnLeft:
                case MapOperation.TurnRight:
                case MapOperation.TurnAround:
                    CurrentDirection = OperateDirection(CurrentDirection, operation);
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 進行方向に対する操作をおこないます。
        /// </summary>
        /// <param name="direction">現在の進行方向を指定します。</param>
        /// <param name="operation">進行方向に対する操作を指定します。</param>
        /// <returns>操作後の進行方向を返します。</returns>
        public Direction OperateDirection(Direction direction, MapOperation operation)
        {
            switch (operation)
            {
                case MapOperation.TurnLeft:
                    if (direction == Direction.Left)
                        return Direction.Down;
                    else if (direction == Direction.Up)
                        return Direction.Left;
                    else if (direction == Direction.Right)
                        return Direction.Up;
                    else
                        return Direction.Right;

                case MapOperation.TurnRight:
                    if (direction == Direction.Left)
                        return Direction.Up;
                    else if (direction == Direction.Up)
                        return Direction.Right;
                    else if (direction == Direction.Right)
                        return Direction.Down;
                    else
                        return Direction.Left;

                case MapOperation.TurnAround:
                    if (direction == Direction.Left)
                        return Direction.Right;
                    else if (direction == Direction.Up)
                        return Direction.Down;
                    else if (direction == Direction.Right)
                        return Direction.Left;
                    else
                        return Direction.Up;
            }

            return direction;
        }
        #endregion 公開メソッド

        #region private メソッド
        /// <summary>
        /// 現在位置を移動します。
        /// </summary>
        /// <param name="direction">進行方向を指定します。</param>
        private bool MoveCurrentPoint(Direction direction)
        {
            int dx;
            int dy;

            switch (direction)
            {
                case Direction.Left:
                    dx = -1;
                    dy = 0;
                    break;

                case Direction.Up:
                    dx = 0;
                    dy = -1;
                    break;

                case Direction.Right:
                    dx = 1;
                    dy = 0;
                    break;

                case Direction.Down:
                    dx = 0;
                    dy = 1;
                    break;

                default:
                    dx = 0;
                    dy = 0;
                    return false;
            }

            var pt = new MapPoint(CurrentPoint.X + dx, CurrentPoint.Y + dy);
            var result = CurrentMap.CheckPath(pt);
            if (result)
                CurrentPoint = pt;

            return result;
        }
        #endregion private メソッド
    }
}
