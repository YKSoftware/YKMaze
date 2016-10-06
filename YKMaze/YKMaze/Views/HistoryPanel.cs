namespace YKMaze.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using YKMaze.Models;

    public class HistoryPanel : FrameworkElement
    {
        /// <summary>
        /// MapData 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MapDataProperty = DependencyProperty.Register("MapData", typeof(IReadOnlyList<IReadOnlyList<bool>>), typeof(HistoryPanel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// ItemData 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ItemDataProperty = DependencyProperty.Register("ItemData", typeof(List<MapPoint>), typeof(HistoryPanel), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// StartPoint 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(MapPoint), typeof(HistoryPanel), new FrameworkPropertyMetadata(new MapPoint(), FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// GoalPoint 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty GoalPointProperty = DependencyProperty.Register("GoalPoint", typeof(MapPoint), typeof(HistoryPanel), new FrameworkPropertyMetadata(new MapPoint(), FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// CurrentPoint 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CurrentPointProperty = DependencyProperty.Register("CurrentPoint", typeof(MapPoint), typeof(HistoryPanel), new FrameworkPropertyMetadata(new MapPoint(), FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// CurrentDirection 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CurrentDirectionProperty = DependencyProperty.Register("CurrentDirection", typeof(Direction), typeof(HistoryPanel), new FrameworkPropertyMetadata(Direction.Up, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 迷路データを取得または設定します。
        /// </summary>
        public IReadOnlyList<IReadOnlyList<bool>> MapData
        {
            get { return (IReadOnlyList<IReadOnlyList<bool>>)GetValue(MapDataProperty); }
            set { SetValue(MapDataProperty, value); }
        }

        /// <summary>
        /// アイテムデータを取得または設定します。
        /// </summary>
        public List<MapPoint> ItemData
        {
            get { return (List<MapPoint>)GetValue(ItemDataProperty); }
            set { SetValue(ItemDataProperty, value); }
        }

        /// <summary>
        /// スタート地点を取得または設定します。
        /// </summary>
        public MapPoint StartPoint
        {
            get { return (MapPoint)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        /// <summary>
        /// ゴール地点を取得または設定します。
        /// </summary>
        public MapPoint GoalPoint
        {
            get { return (MapPoint)GetValue(GoalPointProperty); }
            set { SetValue(GoalPointProperty, value); }
        }

        /// <summary>
        /// 現在位置を取得または設定します。
        /// </summary>
        public MapPoint CurrentPoint
        {
            get { return (MapPoint)GetValue(CurrentPointProperty); }
            set { SetValue(CurrentPointProperty, value); }
        }

        /// <summary>
        /// 進行方向を取得または設定します。
        /// </summary>
        public Direction CurrentDirection
        {
            get { return (Direction)GetValue(CurrentDirectionProperty); }
            set { SetValue(CurrentDirectionProperty, value); }
        }

        /// <summary>
        /// 描画可能領域のサイズ
        /// </summary>
        private Size _availableSize;

        /// <summary>
        /// 一コマ分の領域のサイズ
        /// </summary>
        private Size _unitSize;

        /// <summary>
        /// 横マス数
        /// </summary>
        private int _num_x;

        /// <summary>
        /// 縦マス数
        /// </summary>
        private int _num_y;

        /// <summary>
        /// 描画に必要な領域のサイズを計測します。
        /// </summary>
        /// <param name="availableSize">描画可能領域のサイズ</param>
        /// <returns>描画に必要な領域のサイズを返します。</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            // デザインモードの場合は計測しない
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _availableSize = availableSize;

                if (MapData != null)
                {
                    _num_x = MapData[0].Count;
                    _num_y = MapData.Count;
                    var width = _num_x > _num_y ? _availableSize.Width / _num_x : _availableSize.Width / _num_y;
                    _unitSize = new Size(width, width);
                }
            }

            return new Size(_num_x * _unitSize.Width, _num_y * _unitSize.Height);
            //return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// 描画処理をおこないます。
        /// </summary>
        /// <param name="dc">描画先の DrawingContext</param>
        protected override void OnRender(DrawingContext dc)
        {
            // デザインモードの場合は描画せずに終了する
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;

            if (MapData == null)
                return;

            var w = Window.GetWindow(this);

            var pathBrush = new SolidColorBrush(Colors.Wheat);
            var wallBrush = w.Background.Clone();
            var startBrush = new SolidColorBrush(Colors.LightBlue);
            var goalBrush = new SolidColorBrush(Colors.Plum);
            var itemBrush = new SolidColorBrush(Colors.Green);
            if (pathBrush.CanFreeze)
                pathBrush.Freeze();
            if (wallBrush.CanFreeze)
                wallBrush.Freeze();
            if (startBrush.CanFreeze)
                startBrush.Freeze();
            if (goalBrush.CanFreeze)
                goalBrush.Freeze();
            if (itemBrush.CanFreeze)
                itemBrush.Freeze();

            var pen = new Pen(new SolidColorBrush(Colors.LightGray), 1.0);
            pen.Freeze();

            Point pt;

            if (ItemData != null)
            {
                MapPoint? map_pt = null;
                foreach (var pt_item in ItemData)
                {
                    if (pt_item == CurrentPoint)
                        map_pt = pt_item;
                }
                if (map_pt != null)
                    ItemData.Remove(map_pt.Value);
            }

            // 行ごとの処理
            for (var i = 0; i < _num_y; i++)
            {
                pt = new Point(0, i * _unitSize.Height);
                // 列ごとの処理
                for (var j = 0; j < _num_x; j++)
                {
                    var brush = MapData[i][j] ? pathBrush : wallBrush;
                    if (StartPoint == new MapPoint(j, i))
                        brush = startBrush;
                    else if (GoalPoint == new MapPoint(j, i))
                        brush = goalBrush;
                    else
                    {
                        if (ItemData != null)
                        {
                            foreach (var pt_item in ItemData)
                            {
                                if (pt_item == new MapPoint(j, i))
                                    brush = itemBrush;
                            }
                        }
                    }
                    dc.DrawRectangle(brush, pen, new Rect(pt, _unitSize));
                    pt.Offset(_unitSize.Width, 0);
                }
            }

            // 回転
            var angle = 0;
            switch (CurrentDirection)
            {
                case Direction.Left:
                    angle = 90;
                    break;

                case Direction.Up:
                    angle = 180;
                    break;

                case Direction.Right:
                    angle = -90;
                    break;

                case Direction.Down:
                    angle = 0;
                    break;
            }

            // 平行移動
            pt = new Point(CurrentPoint.X * _unitSize.Width, CurrentPoint.Y * _unitSize.Height);
            var pathGeometry = new PathGeometry();
            var segments = new LineSegment[]
            {
                new LineSegment(new Point(5.0 * _unitSize.Width / 8.0, _unitSize.Height / 5.0), true),
                new LineSegment(new Point(5.0 * _unitSize.Width / 8.0, 2.4 * _unitSize.Height / 5.0), true),
                new LineSegment(new Point(7.0 * _unitSize.Width / 8.0, 2.4 * _unitSize.Height / 5.0), true),
                new LineSegment(new Point(_unitSize.Width / 2.0, 5.0 * _unitSize.Height / 6.0), true),
                new LineSegment(new Point(_unitSize.Width / 8.0, 2.4 * _unitSize.Height / 5.0), true),
                new LineSegment(new Point(3.0 * _unitSize.Width / 8.0, 2.4 * _unitSize.Height / 5.0), true),
            };
            var pathfigure = new PathFigure(new Point(3.0 * _unitSize.Width / 8.0, _unitSize.Height / 5.0), segments, true);
            pathGeometry.Figures.Add(pathfigure);
            
            var tg = new TransformGroup();
            var rotate = new RotateTransform(angle, _unitSize.Width / 2.0, _unitSize.Height / 2.0);
            tg.Children.Add(rotate);
            var translate = new TranslateTransform(CurrentPoint.X * _unitSize.Width, CurrentPoint.Y * _unitSize.Height);
            tg.Children.Add(translate);
            pathGeometry.Transform = tg;
            if (pathGeometry.CanFreeze)
                pathGeometry.Freeze();
            dc.DrawGeometry(wallBrush, pen, pathGeometry);

            //base.OnRender(dc);
        }
    }
}
