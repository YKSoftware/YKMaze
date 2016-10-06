namespace YKMaze.Models
{
    using System;
    using System.Collections.Generic;
    using YKToolkit.Bindings;

    public class Map : NotificationObject
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

        private MapPoint currentPoint;
        /// <summary>
        /// 現在位置を取得します。
        /// </summary>
        public MapPoint CurrentPoint
        {
            get { return currentPoint; }
            private set
            {
                if (SetProperty(ref currentPoint, value))
                {
                    // MapStatus を更新する
                    CurrentMapStatus = CurrentMap.GetMapStatus(currentPoint, CurrentDirection);
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
                if (SetProperty(ref currentDirection, value))
                {
                    CurrentMapStatus = CurrentMap.GetMapStatus(CurrentPoint, currentDirection);
                }
            }
        }

        private MapStatus currentMapStatus;
        /// <summary>
        /// 現在の迷路の状態を取得します。
        /// </summary>
        public MapStatus CurrentMapStatus
        {
            get { return currentMapStatus; }
            private set { SetProperty(ref currentMapStatus, value); }
        }

        /// <summary>
        /// 現在のアイテムリストを取得します。
        /// </summary>
        public IList<KeyValuePair<MapPoint, ItemInfo>> CurrentItemList { get; private set; }

        private MapInfo currentMap;
        /// <summary>
        /// 現在の迷路を取得または設定します。
        /// </summary>
        public MapInfo CurrentMap
        {
            get { return currentMap; }
            set
            {
                currentMap = value;
                CurrentPoint = currentMap.StartPoint;
                CurrentDirection = currentMap.StartDirection;
                CurrentItemList = currentMap.ItemData;
            }
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
                    if (CurrentDirection == Direction.Left)
                        CurrentDirection = Direction.Down;
                    else if (CurrentDirection == Direction.Up)
                        CurrentDirection = Direction.Left;
                    else if (CurrentDirection == Direction.Right)
                        CurrentDirection = Direction.Up;
                    else
                        CurrentDirection = Direction.Right;
                    return true;

                case MapOperation.TurnRight:
                    if (CurrentDirection == Direction.Left)
                        CurrentDirection = Direction.Up;
                    else if (CurrentDirection == Direction.Up)
                        CurrentDirection = Direction.Right;
                    else if (CurrentDirection == Direction.Right)
                        CurrentDirection = Direction.Down;
                    else
                        CurrentDirection = Direction.Left;
                    return true;

                case MapOperation.TurnAround:
                    if (CurrentDirection == Direction.Left)
                        CurrentDirection = Direction.Right;
                    else if (CurrentDirection == Direction.Up)
                        CurrentDirection = Direction.Down;
                    else if (CurrentDirection == Direction.Right)
                        CurrentDirection = Direction.Left;
                    else
                        CurrentDirection = Direction.Up;
                    return true;

                default:
                    return false;
            }
        }

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
    }
}
