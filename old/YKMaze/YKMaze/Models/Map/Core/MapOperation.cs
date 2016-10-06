namespace YKMaze.Models
{
    /// <summary>
    /// 迷路上での操作
    /// </summary>
    public enum MapOperation
    {
        /// <summary>
        /// 前進する
        /// </summary>
        GoForward,

        /// <summary>
        /// 左を向く
        /// </summary>
        TurnLeft,

        /// <summary>
        /// 右を向く
        /// </summary>
        TurnRight,

        /// <summary>
        /// 後ろを向く
        /// </summary>
        TurnAround,
    }
}
