namespace YKMaze.Models
{
    /// <summary>
    /// アイテムの情報
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// アイテム名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 値を取得または設定します。
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 使用時のメッセージを取得または設定します。
        /// </summary>
        public string PickupMessage { get; set; }

        public static ItemInfo Oniku = new ItemInfo()
        {
            Name = "お肉[20]",
            Value = 20,
            PickupMessage = "お肉[20] を拾って食べた。",
        };
    }
}
