﻿namespace YKMaze.Models
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
            if ((width <= 0) || (height <= 0))
            {
                var rnd = new System.Random();
                if (width <= 0)
                    width = rnd.Next(7, 21);
                if (height <= 0)
                    height = rnd.Next(7, 21);
            }

            var stage = new MapInfo()
            {
                Name = "Random Stage",
                Width = width,
                Height = height
            };
            var max_length = width > height ? width : height;
            if (max_length < 13)
                stage.Level = Difficulty.Easy;
            else if (max_length < 19)
                stage.Level = Difficulty.Normal;
            else if (max_length < 25)
                stage.Level = Difficulty.Hard;
            else if (max_length < 51)
                stage.Level = Difficulty.VeryHard;
            else
                stage.Level = Difficulty.Extream;

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

            #region stage1
            var stage1 = new MapInfo()
            {
                Name = "Stage1",
                Level = Difficulty.Easy,
                Width = 5,
                Height = 5,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 4), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Trap10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(1, 0), ItemInfo.Curry3),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage1);
            #endregion stage1

            #region stage2
            var stage2 = new MapInfo()
            {
                Name = "Stage2",
                Level = Difficulty.Easy,
                Width = 5,
                Height = 5,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 4),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(2, 0),
            };
            stages.Add(stage2);
            #endregion stage2

            #region stage3
            var stage3 = new MapInfo()
            {
                Name = "Stage3",
                Level = Difficulty.Easy,
                Width = 5,
                Height = 5,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 4), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(0, 4),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage3);
            #endregion stage3

            #region stage4
            var stage4 = new MapInfo()
            {
                Name = "Stage4",
                Level = Difficulty.Easy,
                Width = 7,
                Height = 7,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 2), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage4);
            #endregion stage4

            #region stage5
            var stage5 = new MapInfo()
            {
                Name = "Stage5",
                Level = Difficulty.Easy,
                Width = 7,
                Height = 7,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 6), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 6), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage5);
            #endregion stage5

            #region stage6
            var stage6 = new MapInfo()
            {
                Name = "Stage6",
                Level = Difficulty.Easy,
                Width = 7,
                Height = 7,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 6),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(4, 4),
            };
            stages.Add(stage6);
            #endregion stage6

            #region stage7
            var stage7 = new MapInfo()
            {
                Name = "Stage7",
                Level = Difficulty.Easy,
                Width = 9,
                Height = 9,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Oniku50),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(2, 0), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(0, 6),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage7);
            #endregion stage7

            #region stage8
            var stage8 = new MapInfo()
            {
                Name = "Stage8",
                Level = Difficulty.Easy,
                Width = 9,
                Height = 9,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 8), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(4, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(6, 6),
            };
            stages.Add(stage8);
            #endregion stage8

            #region stage9
            var stage9 = new MapInfo()
            {
                Name = "Stage9",
                Level = Difficulty.Easy,
                Width = 9,
                Height = 9,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 3), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 0),
            };
            stages.Add(stage9);
            #endregion stage9

            #region stage10
            var stage10 = new MapInfo()
            {
                Name = "Stage10",
                Level = Difficulty.Easy,
                Width = 9,
                Height = 9,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(4, 6),
            };
            stages.Add(stage10);
            #endregion stage10

            #region stage11
            var stage11 = new MapInfo()
            {
                Name = "Stage11",
                Level = Difficulty.Easy,
                Width = 9,
                Height = 9,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 2),
            };
            stages.Add(stage11);
            #endregion stage11

            #region stage12
            var stage12 = new MapInfo()
            {
                Name = "Stage12",
                Level = Difficulty.Easy,
                Width = 11,
                Height = 11,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 10), ItemInfo.Oniku40),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 0), ItemInfo.Oniku40),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 10), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(0, 0),
            };
            stages.Add(stage12);
            #endregion stage12

            #region stage13
            var stage13 = new MapInfo()
            {
                Name = "Stage13",
                Level = Difficulty.Easy,
                Width = 11,
                Height = 11,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 10), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(4, 2),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(8, 2),
            };
            stages.Add(stage13);
            #endregion stage13

            #region stage14
            var stage14 = new MapInfo()
            {
                Name = "Stage14",
                Level = Difficulty.Easy,
                Width = 11,
                Height = 11,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 2), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 6), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(8, 0),
            };
            stages.Add(stage14);
            #endregion stage14

            #region stage15
            var stage15 = new MapInfo()
            {
                Name = "Stage15",
                Level = Difficulty.Easy,
                Width = 11,
                Height = 11,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 10),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(10, 8),
            };
            stages.Add(stage15);
            #endregion stage15

            #region stage16
            var stage16 = new MapInfo()
            {
                Name = "Stage16",
                Level = Difficulty.Easy,
                Width = 11,
                Height = 11,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 8),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(10, 6),
            };
            stages.Add(stage16);
            #endregion stage16

            #region stage17
            var stage17 = new MapInfo()
            {
                Name = "Stage17",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Oniku40),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 12),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(0, 14),
            };
            stages.Add(stage17);
            #endregion stage17

            #region stage18
            var stage18 = new MapInfo()
            {
                Name = "Stage18",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 14), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(6, 2),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(10, 2),
            };
            stages.Add(stage18);
            #endregion stage18

            #region stage19
            var stage19 = new MapInfo()
            {
                Name = "Stage19",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 0), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(4, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(14, 14),
            };
            stages.Add(stage19);
            #endregion stage19

            #region stage20
            var stage20 = new MapInfo()
            {
                Name = "Stage20",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 2), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(12, 4),
            };
            stages.Add(stage20);
            #endregion stage20

            #region stage21
            var stage21 = new MapInfo()
            {
                Name = "Stage21",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 4), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(6, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(12, 8),
            };
            stages.Add(stage21);
            #endregion stage21

            #region stage22
            var stage22 = new MapInfo()
            {
                Name = "Stage22",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 2), ItemInfo.Oniku40),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 14), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(6, 0),
            };
            stages.Add(stage22);
            #endregion stage22

            #region stage23
            var stage23 = new MapInfo()
            {
                Name = "Stage23",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 10), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 6), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(14, 0),
            };
            stages.Add(stage23);
            #endregion stage23

            #region stage24
            var stage24 = new MapInfo()
            {
                Name = "Stage24",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 6), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 4),
            };
            stages.Add(stage24);
            #endregion stage24

            #region stage25
            var stage25 = new MapInfo()
            {
                Name = "Stage25",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 12), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 12), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(2, 6),
            };
            stages.Add(stage25);
            #endregion stage25

            #region stage26
            var stage26 = new MapInfo()
            {
                Name = "Stage26",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 12), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 2),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 0),
            };
            stages.Add(stage26);
            #endregion stage26

            #region stage27
            var stage27 = new MapInfo()
            {
                Name = "Stage27",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Oniku40),
                },
                StartPoint = new MapPoint(6, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(0, 12),
            };
            stages.Add(stage27);
            #endregion stage27

            #region stage28
            var stage28 = new MapInfo()
            {
                Name = "Stage28",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 4), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 14),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(0, 0),
            };
            stages.Add(stage28);
            #endregion stage28

            #region stage29
            var stage29 = new MapInfo()
            {
                Name = "Stage29",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 2), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 2), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 2), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 2), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(10, 0),
            };
            stages.Add(stage29);
            #endregion stage29

            #region stage30
            var stage30 = new MapInfo()
            {
                Name = "Stage30",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(2, 6), ItemInfo.Oniku100),
                },
                StartPoint = new MapPoint(2, 2),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 0),
            };
            stages.Add(stage30);
            #endregion stage30

            #region stage31
            var stage31 = new MapInfo()
            {
                Name = "Stage31",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 0), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 10), ItemInfo.Oniku50),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 12), ItemInfo.Oniku50),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Oniku50),
                },
                StartPoint = new MapPoint(2, 2),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(12, 8),
            };
            stages.Add(stage31);
            #endregion stage31

            #region stage32
            var stage32 = new MapInfo()
            {
                Name = "Stage32",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 8),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(0, 0),
            };
            stages.Add(stage32);
            #endregion stage32

            #region stage33
            var stage33 = new MapInfo()
            {
                Name = "Stage33",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 10), ItemInfo.Oniku100),
                },
                StartPoint = new MapPoint(6, 6),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(12, 2),
            };
            stages.Add(stage33);
            #endregion stage33

            #region stage34
            var stage34 = new MapInfo()
            {
                Name = "Stage34",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 0), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(6, 6),
            };
            stages.Add(stage34);
            #endregion stage34

            #region stage35
            var stage35 = new MapInfo()
            {
                Name = "Stage35",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 8), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 6), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 8), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 10), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 12), ItemInfo.Oniku10),
                },
                StartPoint = new MapPoint(6, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(14, 0),
            };
            stages.Add(stage35);
            #endregion stage35

            #region stage36
            var stage36 = new MapInfo()
            {
                Name = "Stage36",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 7), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 9), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 11), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 14), ItemInfo.Oniku10),
                },
                StartPoint = new MapPoint(2, 4),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(10, 8),
            };
            stages.Add(stage36);
            #endregion stage36

            #region stage37
            var stage37 = new MapInfo()
            {
                Name = "Stage37",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(2, 0), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 2), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 10), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 14), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 14),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(14, 4),
            };
            stages.Add(stage37);
            #endregion stage37

            #region stage38
            var stage38 = new MapInfo()
            {
                Name = "Stage38",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 10), ItemInfo.Oniku10),
                },
                StartPoint = new MapPoint(4, 6),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 6),
            };
            stages.Add(stage38);
            #endregion stage38

            #region stage39
            var stage39 = new MapInfo()
            {
                Name = "Stage39",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 14), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Curry1),
                },
                StartPoint = new MapPoint(0, 14),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(10, 2),
            };
            stages.Add(stage39);
            #endregion stage39

            #region stage40
            var stage40 = new MapInfo()
            {
                Name = "Stage40",
                Level = Difficulty.Normal,
                Width = 15,
                Height = 15,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Curry3),
                },
                StartPoint = new MapPoint(0, 6),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(12, 6),
            };
            stages.Add(stage40);
            #endregion stage40

            #region stage41
            var stage41 = new MapInfo()
            {
                Name = "Stage41",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 0), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 4),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(12, 0),
            };
            stages.Add(stage41);
            #endregion stage41

            #region stage42
            var stage42 = new MapInfo()
            {
                Name = "Stage42",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 4), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 8), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Oniku100),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(4, 8),
            };
            stages.Add(stage42);
            #endregion stage42

            #region stage43
            var stage43 = new MapInfo()
            {
                Name = "Stage43",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 6), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 4), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 2), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 0), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 0), ItemInfo.Trap50),
                },
                StartPoint = new MapPoint(6, 10),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(12, 6),
            };
            stages.Add(stage43);
            #endregion stage43

            #region stage44
            var stage44 = new MapInfo()
            {
                Name = "Stage44",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 0), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 0), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 0), ItemInfo.Oniku1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 12), ItemInfo.Oniku10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 8), ItemInfo.Trap10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 16), ItemInfo.Oniku10),
                },
                StartPoint = new MapPoint(6, 2),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(8, 4),
            };
            stages.Add(stage44);
            #endregion stage44

            #region stage45
            var stage45 = new MapInfo()
            {
                Name = "Stage45",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(6, 4), ItemInfo.Oniku50),
                },
                StartPoint = new MapPoint(6, 8),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(14, 6),
            };
            stages.Add(stage45);
            #endregion stage45

            #region stage46
            var stage46 = new MapInfo()
            {
                Name = "Stage46",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 16), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 6), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(8, 0),
            };
            stages.Add(stage46);
            #endregion stage46

            #region stage47
            var stage47 = new MapInfo()
            {
                Name = "Stage47",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 0), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 2), ItemInfo.Curry1),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 10), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 8),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(14, 4),
            };
            stages.Add(stage47);
            #endregion stage47

            #region stage48
            var stage48 = new MapInfo()
            {
                Name = "Stage48",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(12, 14), ItemInfo.Trap20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 10), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 10), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 0), ItemInfo.Oniku100),
                },
                StartPoint = new MapPoint(6, 4),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(10, 10),
            };
            stages.Add(stage48);
            #endregion stage48

            #region stage49
            var stage49 = new MapInfo()
            {
                Name = "Stage49",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(8, 2), ItemInfo.Trap10),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 4), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(10, 8), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(16, 8),
            };
            stages.Add(stage49);
            #endregion stage49

            #region stage50
            var stage50 = new MapInfo()
            {
                Name = "Stage50",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(16, 0), ItemInfo.Curry3),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(14, 14), ItemInfo.Oniku20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 12), ItemInfo.Trap20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 14), ItemInfo.Trap20),
                    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(4, 16), ItemInfo.Trap20),
                },
                StartPoint = new MapPoint(8, 14),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(6, 0),
            };
            stages.Add(stage50);
            #endregion stage50

            #region stage51
            var stage51 = new MapInfo()
            {
                Name = "Stage51",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 12),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(14, 14),
            };
            stages.Add(stage51);
            #endregion stage51

            #region stage52
            var stage52 = new MapInfo()
            {
                Name = "Stage52",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(10, 10),
            };
            stages.Add(stage52);
            #endregion stage52

            #region stage53
            var stage53 = new MapInfo()
            {
                Name = "Stage53",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(16, 6),
            };
            stages.Add(stage53);
            #endregion stage53

            #region stage54
            var stage54 = new MapInfo()
            {
                Name = "Stage54",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(4, 0),
            };
            stages.Add(stage54);
            #endregion stage54

            #region stage55
            var stage55 = new MapInfo()
            {
                Name = "Stage55",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(16, 2),
            };
            stages.Add(stage55);
            #endregion stage55

            #region stage56
            var stage56 = new MapInfo()
            {
                Name = "Stage56",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(10, 8),
            };
            stages.Add(stage56);
            #endregion stage56

            #region stage57
            var stage57 = new MapInfo()
            {
                Name = "Stage57",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 16),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(2, 2),
            };
            stages.Add(stage57);
            #endregion stage57

            #region stage58
            var stage58 = new MapInfo()
            {
                Name = "Stage58",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 8),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(16, 10),
            };
            stages.Add(stage58);
            #endregion stage58

            #region stage59
            var stage59 = new MapInfo()
            {
                Name = "Stage59",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(14, 14),
            };
            stages.Add(stage59);
            #endregion stage59

            #region stage60
            var stage60 = new MapInfo()
            {
                Name = "Stage60",
                Level = Difficulty.Normal,
                Width = 17,
                Height = 17,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(16, 10),
            };
            stages.Add(stage60);
            #endregion stage60

            #region stage61
            var stage61 = new MapInfo()
            {
                Name = "Stage61",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(20, 4),
            };
            stages.Add(stage61);
            #endregion stage61

            #region stage62
            var stage62 = new MapInfo()
            {
                Name = "Stage62",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(0, 18),
            };
            stages.Add(stage62);
            #endregion stage62

            #region stage63
            var stage63 = new MapInfo()
            {
                Name = "Stage63",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 2),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(10, 2),
            };
            stages.Add(stage63);
            #endregion stage63

            #region stage64
            var stage64 = new MapInfo()
            {
                Name = "Stage64",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(20, 8),
            };
            stages.Add(stage64);
            #endregion stage64

            #region stage65
            var stage65 = new MapInfo()
            {
                Name = "Stage65",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(20, 6),
            };
            stages.Add(stage65);
            #endregion stage65

            #region stage66
            var stage66 = new MapInfo()
            {
                Name = "Stage66",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 20),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(20, 0),
            };
            stages.Add(stage66);
            #endregion stage66

            #region stage67
            var stage67 = new MapInfo()
            {
                Name = "Stage67",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(20, 14),
            };
            stages.Add(stage67);
            #endregion stage67

            #region stage68
            var stage68 = new MapInfo()
            {
                Name = "Stage68",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(18, 16),
            };
            stages.Add(stage68);
            #endregion stage68

            #region stage69
            var stage69 = new MapInfo()
            {
                Name = "Stage69",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(16, 0),
            };
            stages.Add(stage69);
            #endregion stage69

            #region stage70
            var stage70 = new MapInfo()
            {
                Name = "Stage70",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 18),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(16, 2),
            };
            stages.Add(stage70);
            #endregion stage70

            #region stage71
            var stage71 = new MapInfo()
            {
                Name = "Stage71",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 14),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(20, 0),
            };
            stages.Add(stage71);
            #endregion stage71

            #region stage72
            var stage72 = new MapInfo()
            {
                Name = "Stage72",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(0, 14),
            };
            stages.Add(stage72);
            #endregion stage72

            #region stage73
            var stage73 = new MapInfo()
            {
                Name = "Stage73",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(14, 0),
            };
            stages.Add(stage73);
            #endregion stage73

            #region stage74
            var stage74 = new MapInfo()
            {
                Name = "Stage74",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(20, 0),
            };
            stages.Add(stage74);
            #endregion stage74

            #region stage75
            var stage75 = new MapInfo()
            {
                Name = "Stage75",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(18, 2),
            };
            stages.Add(stage75);
            #endregion stage75

            #region stage76
            var stage76 = new MapInfo()
            {
                Name = "Stage76",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(18, 16),
            };
            stages.Add(stage76);
            #endregion stage76

            #region stage77
            var stage77 = new MapInfo()
            {
                Name = "Stage77",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(10, 8),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(0, 0),
            };
            stages.Add(stage77);
            #endregion stage77

            #region stage78
            var stage78 = new MapInfo()
            {
                Name = "Stage78",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 4),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(20, 0),
            };
            stages.Add(stage78);
            #endregion stage78

            #region stage79
            var stage79 = new MapInfo()
            {
                Name = "Stage79",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(20, 8),
            };
            stages.Add(stage79);
            #endregion stage79

            #region stage80
            var stage80 = new MapInfo()
            {
                Name = "Stage80",
                Level = Difficulty.Hard,
                Width = 21,
                Height = 21,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 4),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(16, 6),
            };
            stages.Add(stage80);
            #endregion stage80

            #region stage81
            var stage81 = new MapInfo()
            {
                Name = "Stage81",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(12, 2),
            };
            stages.Add(stage81);
            #endregion stage81

            #region stage82
            var stage82 = new MapInfo()
            {
                Name = "Stage82",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 8),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(22, 0),
            };
            stages.Add(stage82);
            #endregion stage82

            #region stage83
            var stage83 = new MapInfo()
            {
                Name = "Stage83",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(26, 22),
            };
            stages.Add(stage83);
            #endregion stage83

            #region stage84
            var stage84 = new MapInfo()
            {
                Name = "Stage84",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 10),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(10, 0),
            };
            stages.Add(stage84);
            #endregion stage84

            #region stage85
            var stage85 = new MapInfo()
            {
                Name = "Stage85",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(16, 20),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(0, 0),
            };
            stages.Add(stage85);
            #endregion stage85

            #region stage86
            var stage86 = new MapInfo()
            {
                Name = "Stage86",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(14, 16),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(26, 0),
            };
            stages.Add(stage86);
            #endregion stage86

            #region stage87
            var stage87 = new MapInfo()
            {
                Name = "Stage87",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(16, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(18, 0),
            };
            stages.Add(stage87);
            #endregion stage87

            #region stage88
            var stage88 = new MapInfo()
            {
                Name = "Stage88",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(12, 0),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(26, 4),
            };
            stages.Add(stage88);
            #endregion stage88

            #region stage89
            var stage89 = new MapInfo()
            {
                Name = "Stage89",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 4),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(26, 2),
            };
            stages.Add(stage89);
            #endregion stage89

            #region stage90
            var stage90 = new MapInfo()
            {
                Name = "Stage90",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false,  true,  true,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(18, 6),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(18, 4),
            };
            stages.Add(stage90);
            #endregion stage90

            #region stage91
            var stage91 = new MapInfo()
            {
                Name = "Stage91",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 18),
                StartDirection = Direction.Right,
                GoalPoint = new MapPoint(12, 2),
            };
            stages.Add(stage91);
            #endregion stage91

            #region stage92
            var stage92 = new MapInfo()
            {
                Name = "Stage92",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(26, 10),
            };
            stages.Add(stage92);
            #endregion stage92

            #region stage93
            var stage93 = new MapInfo()
            {
                Name = "Stage93",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(4, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(18, 12),
            };
            stages.Add(stage93);
            #endregion stage93

            #region stage94
            var stage94 = new MapInfo()
            {
                Name = "Stage94",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(12, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(24, 6),
            };
            stages.Add(stage94);
            #endregion stage94

            #region stage95
            var stage95 = new MapInfo()
            {
                Name = "Stage95",
                Level = Difficulty.VeryHard,
                Width = 27,
                Height = 27,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 20),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(10, 0),
            };
            stages.Add(stage95);
            #endregion stage95

            #region stage96
            var stage96 = new MapInfo()
            {
                Name = "Stage96",
                Level = Difficulty.VeryHard,
                Width = 29,
                Height = 29,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(2, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(28, 4),
            };
            stages.Add(stage96);
            #endregion stage96

            #region stage97
            var stage97 = new MapInfo()
            {
                Name = "Stage97",
                Level = Difficulty.VeryHard,
                Width = 29,
                Height = 29,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(22, 0),
                StartDirection = Direction.Left,
                GoalPoint = new MapPoint(28, 12),
            };
            stages.Add(stage97);
            #endregion stage97

            #region stage98
            var stage98 = new MapInfo()
            {
                Name = "Stage98",
                Level = Difficulty.VeryHard,
                Width = 29,
                Height = 29,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(8, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(28, 0),
            };
            stages.Add(stage98);
            #endregion stage98

            #region stage99
            var stage99 = new MapInfo()
            {
                Name = "Stage99",
                Level = Difficulty.VeryHard,
                Width = 29,
                Height = 29,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(6, 2),
                StartDirection = Direction.Up,
                GoalPoint = new MapPoint(28, 10),
            };
            stages.Add(stage99);
            #endregion stage99

            #region stage100
            var stage100 = new MapInfo()
            {
                Name = "Stage100",
                Level = Difficulty.Extream,
                Width = 51,
                Height = 51,
                MapData = new List<IReadOnlyList<bool>>()
                {
                    new List<bool>() {  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true },
                    new List<bool>() { false, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false, false, false,  true, false, false },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false, false, false,  true, false,  true, false, false, false, false, false, false, false, false, false, false, false,  true, false, false, false, false, false,  true, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true },
                    new List<bool>() {  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false, false, false,  true, false, false, false,  true, false, false, false, false, false,  true, false,  true, false, false, false, false, false, false, false,  true, false,  true, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true, false,  true, false,  true,  true,  true, false,  true, false,  true },
                    new List<bool>() { false, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false, false, false,  true, false, false, false, false, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true,  true,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true,  true,  true },
                    new List<bool>() {  true, false,  true, false, false, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false,  true, false,  true, false, false, false,  true, false,  true, false,  true, false, false, false, false, false,  true, false, false, false,  true, false,  true, false,  true, false,  true, false, false, false, false, false,  true },
                    new List<bool>() {  true, false,  true,  true,  true,  true,  true, false,  true, false,  true, false,  true,  true,  true,  true,  true,  true,  true, false,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true, false,  true, false,  true,  true,  true,  true,  true, false,  true,  true,  true,  true,  true },
                },
                ItemData = new List<KeyValuePair<MapPoint, ItemInfo>>()
                {
                    //    new KeyValuePair<MapPoint, ItemInfo>(new MapPoint(0, 1), ItemInfo.Oniku20),
                },
                StartPoint = new MapPoint(0, 0),
                StartDirection = Direction.Down,
                GoalPoint = new MapPoint(47, 18),
            };
            stages.Add(stage100);
            #endregion stage100
        }
        #endregion コンストラクタ

        #region ランダム迷路生成ヘルパ
        /// <summary>
        /// 壁延ばし法によるランダムな迷路を生成します。
        /// </summary>
        /// <param name="width">迷路の幅を奇数で指定します。</param>
        /// <param name="height">迷路の高さを奇数で指定します。</param>
        /// <returns>迷路データを返します。</returns>
        private static IReadOnlyList<IReadOnlyList<bool>> GenerateRandomMap(int width, int height)
        {
            if (width % 2 == 0)
                width++;
            if (height % 2 == 0)
                height++;

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
            var mapData = new List<IReadOnlyList<bool>>();
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
        private static void SetRandomStartAndGoalPoint(IReadOnlyList<IReadOnlyList<bool>> mapData, out MapPoint startPoint, out MapPoint goalPoint)
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
            List<MapPoint> list;

            // スタート座標をランダムに決定する
            list = new List<MapPoint>();
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (mapData[j][i])
                        list.Add(new MapPoint(i, j));
                }
            }
            do
            {
                do
                {
                    if (list.Count == 0)
                    {
                        pt = new MapPoint(0, 0);
                        break;
                    }
                    var i = rnd.Next(0, list.Count - 1);
                    pt = list[i];
                    list.Remove(pt);
                } while (!(pt.X <= width / 2));
                if (list.Count == 0)
                    break;
            } while (!CheckFukuroKoji(mapData, pt));
            startPoint = pt;

            // ゴール座標をランダムに決定する
            list = new List<MapPoint>();
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (mapData[j][i])
                        list.Add(new MapPoint(i, j));
                }
            }
            list.Remove(startPoint);
            do
            {
                do
                {
                    if (list.Count == 0)
                    {
                        pt = new MapPoint(height - 1, width - 1);
                        break;
                    }
                    var i = rnd.Next(0, list.Count - 1);
                    pt = list[i];
                    list.Remove(pt);
                } while (!(pt.X >= width / 2 + 1));
                if (list.Count == 0)
                    break;
            } while (!CheckFukuroKoji(mapData, pt));
            goalPoint = pt;
        }

        /// <summary>
        /// 袋小路かどうかを確認します。
        /// </summary>
        /// <param name="mapData">迷路データを指定します。</param>
        /// <param name="point">確認する座標を指定します。</param>
        /// <returns></returns>
        private static bool CheckFukuroKoji(IReadOnlyList<IReadOnlyList<bool>> mapData, MapPoint point)
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
