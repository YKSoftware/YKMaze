namespace YKMaze.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 迷路の基本情報
    /// </summary>
    public class MapInfo
    {
        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 難易度を取得または設定します。
        /// </summary>
        public Difficulty Level { get; set; }

        #region 迷路データ
        /// <summary>
        /// 横マス数を取得または設定します。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 縦マス数を取得または設定します。
        /// </summary>
        public int Height { get; set; }

        private IList<IList<bool>> mapData;
        /// <summary>
        /// 迷路データを取得または設定します。
        /// 設定する場合は Width および Height プロパティを先に設定して下さい。
        /// </summary>
        public IList<IList<bool>> MapData
        {
            get { return mapData; }
            set
            {
                while (value.Count < Height)
                    value.Add(Enumerable.Range(0, Width).Select(_ => false).ToList());
                while (value.Count > Height)
                    value.RemoveAt(value.Count - 1);

                foreach (var list in value)
                {
                    while (list.Count < Width)
                        list.Add(false);
                    while (list.Count > Width)
                        list.RemoveAt(list.Count - 1);
                }

                mapData = value;
            }
        }

        private IList<KeyValuePair<MapPoint, ItemInfo>> itemData = new List<KeyValuePair<MapPoint, ItemInfo>>();
        /// <summary>
        /// アイテムデータを取得または設定します。
        /// </summary>
        public IList<KeyValuePair<MapPoint, ItemInfo>> ItemData
        {
            get { return itemData; }
            set { itemData = value; }
        }

        /// <summary>
        /// スタート地点を取得または設定します。
        /// </summary>
        public MapPoint StartPoint { get; set; }

        /// <summary>
        /// スタート時の方向を取得または設定します。
        /// </summary>
        public Direction StartDirection { get; set; }

        /// <summary>
        /// ゴール地点を取得または設定します。
        /// </summary>
        public MapPoint GoalPoint { get; set; }
        #endregion 迷路データ

        /// <summary>
        /// 指定座標が通れるかどうかを確認します。
        /// </summary>
        /// <param name="point">座標を指定します。</param>
        /// <returns></returns>
        public bool CheckPath(MapPoint point)
        {
            return CheckPath(point.X, point.Y);
        }

        /// <summary>
        /// 指定座標が通れるかどうかを確認します。
        /// </summary>
        /// <param name="x">横軸座標を指定します。</param>
        /// <param name="y">縦軸座標を指定します。</param>
        /// <returns>通れる場合は true を返します。</returns>
        public bool CheckPath(int x, int y)
        {
            if (MapData == null)
                return false;
            if (y < 0)
                return false;
            if (x < 0)
                return false;

            if (y < MapData.Count)
            {
                if (MapData[y] == null)
                    return false;

                if (x < MapData[y].Count)
                {
                    return MapData[y][x];
                }
            }

            return false;
        }

        /// <summary>
        /// 位置と向きから迷路の状態を取得します。
        /// </summary>
        /// <param name="point">調べたい位置を指定します。</param>
        /// <param name="direction">調べたい向きを指定します。</param>
        /// <returns>指定された位置と向きにおける迷路の状態を返します。</returns>
        public MapStatus GetMapStatus(MapPoint point, Direction direction)
        {
            if (MapData == null)
                return MapStatus.None;

            var left = -1;
            var right = -1;

            // 現在の位置から見て
            // 左の確認
            var left0 = CheckPath(point.Offset(1, 0, direction));
            // 右の確認
            var right0 = CheckPath(point.Offset(-1, 0, direction));
            // 目の前の確認
            var stop0 = CheckPath(point.Offset(0, 1, direction));
            if (!stop0)
            {
                // 行き止まりだったらここで確定
                return GetStatus(0, left0 ? 0 : -1, right0 ? 0 : -1);
            }

            // 一歩進んだ位置から見て
            // 左の確認
            var left1 = CheckPath(point.Offset(1, 1, direction));
            // 右の確認
            var right1 = CheckPath(point.Offset(-1, 1, direction));
            // 目の前の確認
            var stop1 = CheckPath(point.Offset(0, 2, direction));
            if (!stop1)
            {
                // 行き止まりだったらここで確定

                if (left0)
                    left = 0;
                else if (left1)
                    left = 1;
                if (right0)
                    right = 0;
                else if (right1)
                    right = 1;

                return GetStatus(1, left, right);
            }

            // 二歩進んだ位置から見て
            // 左の確認
            var left2 = CheckPath(point.Offset(1, 2, direction));
            // 右の確認
            var right2 = CheckPath(point.Offset(-1, 2, direction));
            // 目の前の確認
            var stop2 = CheckPath(point.Offset(0, 3, direction));

            if (left0 && left2)
                left = 3;
            else if (left0)
                left = 0;
            else if (left1)
                left = 1;
            else if (left2)
                left = 2;
            if (right0 && right2)
                right = 3;
            else if (right0)
                right = 0;
            else if (right1)
                right = 1;
            else if (right2)
                right = 2;

            return GetStatus(stop2 ? -1 : 2, left, right);
        }

        /// <summary>
        /// 迷路の状態を返す
        /// </summary>
        /// <param name="stop">-1:行き止まりなし/0:目の前行き止まり/1:行き止まり手前1/2:行き止まり手前2</param>
        /// <param name="left">-1:左角なし/0:左角/1:左角手前1/2:左角手前2</param>
        /// <param name="right">-1:右角なし/0:右角/1:右角手前1/2:右角手前2</param>
        /// <returns></returns>
        private MapStatus GetStatus(int stop, int left, int right)
        {
            if (stop < 0)
            {
                // stop_none
                if (left < 0)
                {
                    // left_none
                    if (right < 0)
                    {
                        return MapStatus.Stop_None_Left_None_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_None_Left_None_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_None_Left_None_Right_Pre1;
                    }
                    else
                    {
                        return MapStatus.Stop_None_Left_None_Right_Pre2;
                    }
                }
                else if (left == 0)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_None_Left_Pre0_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_None_Left_Pre0_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_None_Left_Pre0_Right_Pre1;
                    }
                    else
                    {
                        return MapStatus.Stop_None_Left_Pre0_Right_Pre2;
                    }
                }
                else if (left == 1)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_None_Left_Pre1_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_None_Left_Pre1_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_None_Left_Pre1_Right_Pre1;
                    }
                    else
                    {
                        return MapStatus.Stop_None_Left_Pre1_Right_Pre2;
                    }
                }
                else
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_None_Left_Pre2_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_None_Left_Pre2_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_None_Left_Pre2_Right_Pre1;
                    }
                    else
                    {
                        return MapStatus.Stop_None_Left_Pre2_Right_Pre2;
                    }
                }
            }
            else if (stop == 0)
            {
                if (left < 0)
                {
                    // left_none
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre0_Left_None_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre0_Left_None_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        throw new System.ArgumentException();
                    }
                    else
                    {
                        throw new System.ArgumentException();
                    }
                }
                else if (left == 0)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre0_Left_Pre0_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre0_Left_Pre0_Right_Pre0;
                    }
                    else
                    {
                        throw new System.ArgumentException();
                    }
                }
                else
                {
                    throw new System.ArgumentException();
                }
            }
            else if (stop == 1)
            {
                if (left < 0)
                {
                    // left_none
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre1_Left_None_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre1_Left_None_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre1_Left_None_Right_Pre1;
                    }
                    else
                    {
                        throw new System.ArgumentException();
                    }
                }
                else if (left == 0)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre0_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre0_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre0_Right_Pre1;
                    }
                    else
                    {
                        throw new System.ArgumentException();
                    }
                }
                else if (left == 1)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre1_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre1_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre1_Left_Pre1_Right_Pre1;
                    }
                    else
                    {
                        throw new System.ArgumentException();
                    }
                }
                else
                {
                    throw new System.ArgumentException();
                }
            }
            else // stop == 2
            {
                if (left < 0)
                {
                    // left_none
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_Pre1;
                    }
                    else if (right == 2)
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_Pre2;
                    }
                    else
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_Pre0_Pre2;
                    }
                }
                else if (left == 0)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Right_Pre1;
                    }
                    else if (right == 2)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Right_Pre2;
                    }
                    else
                    {
                        return MapStatus.Stop_Pre2_Left_None_Right_Pre0_Pre2;
                    }
                }
                else if (left == 1)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre1_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre1_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre1_Right_Pre1;
                    }
                    else if (right == 2)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre1_Right_Pre2;
                    }
                    else
                    {
                        return MapStatus.Stop_Pre2_Left_Pre1_Right_Pre0_Pre2;
                    }
                }
                else if (left == 2)
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre2_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre2_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre2_Right_Pre1;
                    }
                    else if (right == 2)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre2_Right_Pre2;
                    }
                    else
                    {
                        return MapStatus.Stop_Pre2_Left_Pre2_Right_Pre0_Pre2;
                    }
                }
                else
                {
                    if (right < 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_None;
                    }
                    else if (right == 0)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre0;
                    }
                    else if (right == 1)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre1;
                    }
                    else if (right == 2)
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre2;
                    }
                    else
                    {
                        return MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre0_Pre2;
                    }
                }
            }
        }
    }
}
