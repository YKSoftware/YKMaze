namespace YKMaze.Models
{
    /// <summary>
    /// 迷路上の左上を原点として 0 始まりの座標
    /// </summary>
    public struct MapPoint
    {
        private static int id;
        private int hashCode;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="x">横軸座標を指定します。</param>
        /// <param name="y">縦軸座標を指定します。</param>
        public MapPoint(int x, int y)
        {
            this.x = x;
            this.y = y;

            var h = id++;
            hashCode = h;
        }

        private int x;
        /// <summary>
        /// 左端を原点として 0 始まりの横軸座標を取得または設定します。
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        /// <summary>
        /// 上端を原点として 0 始まりの縦軸座標を取得または設定します。
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列を返します。</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        /// <summary>
        /// 比較演算子 == をオーバーライドします。
        /// </summary>
        /// <param name="pt1">比較される値を指定します。</param>
        /// <param name="pt2">比較する値を指定します。</param>
        /// <returns>X および Y プロパティがそれぞれ等しい場合に true を返します。</returns>
        public static bool operator== (MapPoint pt1, MapPoint pt2)
        {
            return (pt1.X == pt2.X) && (pt1.Y == pt2.Y);
        }

        public static bool operator!= (MapPoint pt1, MapPoint pt2)
        {
            return (pt1.X != pt2.X) || (pt1.Y != pt2.Y);
        }

        public override bool Equals(object obj)
        {
            //return base.Equals(obj);
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            var pt = (MapPoint)obj;
            return this == pt;
        }

        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return this.hashCode;
        }
    }

    public static class MapPointExtensions
    {
        /// <summary>
        /// 進行方向に対してオフセットした座標を取得します。
        /// </summary>
        /// <param name="point">起点となる座標を指定します。</param>
        /// <param name="left">進行方向に対して左へのオフセットを指定します。</param>
        /// <param name="up">進行方向に対して正面へのオフセットを指定します。</param>
        /// <param name="direction">進行方向を指定します。</param>
        /// <returns>オフセットされた座標を返します。</returns>
        public static MapPoint Offset(this MapPoint point, int left, int up, Direction direction)
        {
            var x = point.X;
            var y = point.Y;

            switch (direction)
            {
                case Direction.Left:
                    x -= up;
                    y += left;
                    break;

                case Direction.Up:
                    x -= left;
                    y -= up;
                    break;

                case Direction.Right:
                    x += up;
                    y -= left;
                    break;

                case Direction.Down:
                    x += left;
                    y += up;
                    break;

                default:
                    break;
            }

            return new MapPoint(x, y);
        }
    }
}
