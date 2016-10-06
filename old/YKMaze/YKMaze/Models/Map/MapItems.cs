namespace YKMaze.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// 固定またはランダムな迷路情報を持つクラス
    /// </summary>
    public class MapItems
    {
        #region public static プロパティ
        private static readonly IList<MapInfo> stages;
        /// <summary>
        /// ステージリストを取得します。
        /// </summary>
        public static IList<MapInfo> Stages
        {
            get { return stages; }
        }
        #endregion public static プロパティ

        #region public static メソッド
        /// <summary>
        /// ランダムな迷路を取得します。
        /// </summary>
        /// <param name="width">迷路の幅を奇数で指定します。</param>
        /// <param name="height">迷路の高さを奇数で指定します。</param>
        /// <returns>ランダムな迷路情報を返します。</returns>
        public static MapInfo RandomStage(int width, int height)
        {
            var stage = new MapInfo()
            {
                Name = "Random Stage",
                Width = width,
                Height = height
            };

            stage.MapData = GenerateRandomMap(stage.Width, stage.Height);
            MapPoint pt1;
            MapPoint pt2;
            SetRandomStartAndGoalPoint(stage.MapData, out pt1, out pt2);
            stage.StartPoint = pt1;
            stage.GoalPoint = pt2;
            var dir = Direction.Left;
            pt1 = stage.StartPoint.Offset(0, 1, dir);
            if ((pt1.X < 0) || (pt1.X == width) || (pt1.Y < 0) || (pt1.Y == height) || !stage.MapData[pt1.Y][pt1.X])
            {
                dir = Direction.Up;
                pt1 = stage.StartPoint.Offset(0, 1, dir);
                if ((pt1.X < 0) || (pt1.X == width) || (pt1.Y < 0) || (pt1.Y == height) || !stage.MapData[pt1.Y][pt1.X])
                {
                    dir = Direction.Right;
                    pt1 = stage.StartPoint.Offset(0, 1, dir);
                    if ((pt1.X < 0) || (pt1.X == width) || (pt1.Y < 0) || (pt1.Y == height) || !stage.MapData[pt1.Y][pt1.X])
                    {
                        dir = Direction.Down;
                    }
                }
            }
            stage.StartDirection = dir;

            return stage;
        }
        #endregion public static メソッド

        #region コンストラクタ
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static MapItems()
        {
            stages = new List<MapInfo>();

            var stage1 = new MapInfo()
            {
                Name = "Stage1",
                Level = Difficulty.Easy,
                Width = 8,
                Height = 8,
                MapData = new List<IList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false },
                    new List<bool>() { false, false,  true, false,  true, false, false, false },
                    new List<bool>() { false, false,  true,  true,  true, false, false, false },
                    new List<bool>() { false, false,  true, false, false, false, false, false },
                    new List<bool>() { false, false,  true, false, false, false, false, false },
                    new List<bool>() { false, false,  true, false, false, false, false, false },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 2), ItemInfo.Oniku),
                },
                StartPoint = new MapPoint(2, 7),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(7, 0),
            };
            stages.Add(stage1);

            var stage2 = new MapInfo()
            {
                Name = "Stage2",
                Level = Difficulty.Easy,
                Width = 8,
                Height = 8,
                MapData = new List<IList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false },
                    new List<bool>() { false, false,  true, false,  true, false,  true,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true, false, false },
                    new List<bool>() {  true, false,  true, false, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 2), ItemInfo.Oniku),
                },
                StartPoint = new MapPoint(0, 7),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(7, 0),
            };
            stages.Add(stage2);
        }
        #endregion コンストラクタ

        #region ランダムな迷路
        /// <summary>
        /// ランダムな迷路を生成します。
        /// </summary>
        /// <param name="width">迷路の幅を奇数で指定します。</param>
        /// <param name="height">迷路の高さを奇数で指定します。</param>
        /// <returns>迷路データを返します。</returns>
        public static IList<IList<bool>> GenerateRandomMap(int width, int height)
        {
            if (width % 2 == 0)
                return null;
            if (height % 2 == 0)
                return null;

            var mapDataCore = new bool[width, height];
            var points = new List<MapPoint>();

            // すべてを道とした迷路データを生成する
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    mapDataCore[j, i] = true;

                    // 奇数座標を保持する
                    if ((i % 2 != 0) && (j % 2 != 0))
                        points.Add(new MapPoint(j, i));
                }
            }

            // 候補となる奇数座標がなくなるまで続ける
            var rnd = new System.Random();
            while (points.Count > 0)
            {

                // スタート地点をランダムに決定する
                MapPoint? pt0;
                while (true)
                {
                    if (points.Count > 0)
                    {
                        pt0 = points[rnd.Next(0, points.Count - 1)];
                        if (mapDataCore[pt0.Value.X, pt0.Value.Y])
                            break;

                        // 既に壁となっている座標は除外する
                        points.Remove(pt0.Value);
                    }
                    else
                    {
                        pt0 = null;
                        break;
                    }
                }
                if (pt0 == null)
                    break;

                // 今から作る壁の座標リスト
                var walls = new List<MapPoint>();
                var stopGenerate = false;
                var pt = (MapPoint)pt0;
                MapPoint nextPoint;
                while (!stopGenerate)
                {
                    var dir = rnd.Next(0, 5);   // 0:左/1:上/2:上/3:右/4:下/5:下
                    Direction direction = Direction.Left;

                    // 進行方向 2 マス先の座標を取得する
                    // 上端/下端の横並びがまっすぐの通路になりやすいので
                    // 上/下になる確率を左/右より若干高くしている
                    switch (dir)
                    {
                        case 0: direction = Direction.Left; break;
                        case 1: direction = Direction.Up; break;
                        case 2: direction = Direction.Up; break;
                        case 3: direction = Direction.Right; break;
                        case 4: direction = Direction.Down; break;
                        case 5: direction = Direction.Down; break;
                        default:
                            break;
                    }
                    nextPoint = pt.Offset(0, 2, direction);

                    // 進行方向 2 マス先が自分かどうか確認する
                    if (walls.Contains(nextPoint))
                    {
                        // 自分の場合は壁作り失敗でやり直し
                        walls.Clear();
                        break;
                    }

                    // 現在位置から進行方向 2 マスを壁として登録する
                    walls.Add(pt);
                    var temp = pt.Offset(0, 1, direction);
                    if ((temp.X >= 0) && (temp.X < width) && (temp.Y >= 0) && (temp.Y < height))
                    {
                        walls.Add(temp);
                        temp = pt.Offset(0, 2, direction);
                        if ((temp.X >= 0) && (temp.X < width) && (temp.Y >= 0) && (temp.Y < height))
                        {
                            walls.Add(temp);
                            if (!mapDataCore[temp.X, temp.Y])
                                break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                    // 現在位置を移動する
                    if (!stopGenerate)
                        pt = nextPoint;
                }   // end of while (!stopGenerate)

                if (walls.Count > 0)
                {
                    foreach (var wall in walls)
                    {
                        mapDataCore[wall.X, wall.Y] = false;
                    }

                    points.Remove(pt0.Value);
                }
            }   // end of while (points.Count > 0)

            #region 内部変数を戻り値の型に変換する
            var mapData = new List<IList<bool>>();
            for (var i = 0; i < height; i++)
            {
                var row = new List<bool>();
                for (var j = 0; j < width; j++)
                {
                    row.Add(mapDataCore[j, i]);
                }
                mapData.Add(row);
            }
            #endregion 内部変数を戻り値の型に変換する

            return mapData;
        }

        /// <summary>
        /// スタート地点およびゴール地点をランダムに決定します。
        /// </summary>
        /// <param name="mapData">迷路データを指定します。</param>
        /// <param name="startPoint">決定されるスタート地点を出力します。</param>
        /// <param name="goalPoint">決定されるゴール地点を出力します。</param>
        public static void SetRandomStartAndGoalPoint(IList<IList<bool>> mapData, out MapPoint startPoint, out MapPoint goalPoint)
        {
            if (mapData == null)
            {
                startPoint = new MapPoint(0, 0);
                goalPoint = new MapPoint(0, 0);
                return;
            }
            var height = mapData.Count;
            if (height == 0)
            {
                startPoint = new MapPoint(0, 0);
                goalPoint = new MapPoint(0, 0);
                return;
            }
            var width = mapData[0].Count;
            if (width == 0)
            {
                startPoint = new MapPoint(0, 0);
                goalPoint = new MapPoint(0, 0);
                return;
            }

            MapPoint pt;
            var rnd = new System.Random();
            List<MapPoint> list = new List<MapPoint>();
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    list.Add(new MapPoint(i, j));
                }
            }

            // スタート座標をランダムに決定する
            do
            {
                do
                {
                    var i = rnd.Next(0, list.Count - 1);
                    pt = list[i];
                } while (!(pt.X <= width / 2));
                list.Remove(pt);
            } while (!CheckFukuroKoji(mapData, pt));
            startPoint = pt;

            // ゴール座標をランダムに決定する
            do
            {
                do
                {
                    var i = rnd.Next(0, list.Count - 1);
                    pt = list[i];
                } while (!(pt.X >= width / 2 + 1));
                list.Remove(pt);
            } while (!CheckFukuroKoji(mapData, pt));
            goalPoint = pt;
        }

        /// <summary>
        /// 袋小路かどうかを確認します。
        /// </summary>
        /// <param name="mapData">迷路データを指定します。</param>
        /// <param name="point">確認する座標を指定します。</param>
        /// <returns></returns>
        private static bool CheckFukuroKoji(IList<IList<bool>> mapData, MapPoint point)
        {
            var width = mapData[0].Count;
            var height = mapData.Count;
            var walls = 0;
            MapPoint pt;

            pt = point.Offset(0, 1, Direction.Left);
            walls += pt.X < 0 ? 1 :
                (mapData[pt.Y][pt.X] ? 0 : 1);
            pt = point.Offset(0, 1, Direction.Up);
            walls += pt.Y < 0 ? 1 :
                (mapData[pt.Y][pt.X] ? 0 : 1);
            pt = point.Offset(0, 1, Direction.Right);
            walls += pt.X >= width ? 1 :
                (mapData[pt.Y][pt.X] ? 0 : 1);
            pt = point.Offset(0, 1, Direction.Down);
            walls += pt.Y >= height ? 1 :
                (mapData[pt.Y][pt.X] ? 0 : 1);

            return walls == 3;
        }
        #endregion ランダムな迷路
    }
}
