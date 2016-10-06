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
        /// カレー粉かどうかを取得または設定します。
        /// </summary>
        public bool IsCurry { get; set; }

        /// <summary>
        /// 値を取得または設定します。
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 使用時のメッセージを取得または設定します。
        /// </summary>
        public string PickupMessage { get; set; }

        /// <summary>
        /// お肉[1]
        /// </summary>
        public static ItemInfo Oniku1 = new ItemInfo()
        {
            Name = "お肉[1]",
            Value = 1,
            PickupMessage = "お肉[1] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[10]
        /// </summary>
        public static ItemInfo Oniku10 = new ItemInfo()
        {
            Name = "お肉[10]",
            Value = 10,
            PickupMessage = "お肉[10] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[15]
        /// </summary>
        public static ItemInfo Oniku15 = new ItemInfo()
        {
            Name = "お肉[15]",
            Value = 15,
            PickupMessage = "お肉[15] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[20]
        /// </summary>
        public static ItemInfo Oniku20 = new ItemInfo()
        {
            Name = "お肉[20]",
            Value = 20,
            PickupMessage = "お肉[20] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[40]
        /// </summary>
        public static ItemInfo Oniku40 = new ItemInfo()
        {
            Name = "お肉[40]",
            Value = 40,
            PickupMessage = "お肉[40] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[50]
        /// </summary>
        public static ItemInfo Oniku50 = new ItemInfo()
        {
            Name = "お肉[50]",
            Value = 50,
            PickupMessage = "お肉[50] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// お肉[100]
        /// </summary>
        public static ItemInfo Oniku100 = new ItemInfo()
        {
            Name = "お肉[100]",
            Value = 100,
            PickupMessage = "お肉[100] を拾って食べた。",
            IsCurry = false,
        };

        /// <summary>
        /// カレー粉[1]
        /// </summary>
        public static ItemInfo Curry1 = new ItemInfo()
        {
            Name = "カレー粉[1]",
            Value = 1,
            PickupMessage = "カレー粉[1] を拾った。",
            IsCurry = true,
        };

        /// <summary>
        /// カレー粉[3]
        /// </summary>
        public static ItemInfo Curry3 = new ItemInfo()
        {
            Name = "カレー粉[3]",
            Value = 3,
            PickupMessage = "カレー粉[3] を拾った。",
            IsCurry = true,
        };

        /// <summary>
        /// 罠[10]
        /// </summary>
        public static ItemInfo Trap10 = new ItemInfo()
        {
            Name = "罠[10]",
            Value = -10,
            PickupMessage = "罠[10] にひっかかった！",
            IsCurry = false,
        };

        /// <summary>
        /// 罠[20]
        /// </summary>
        public static ItemInfo Trap20 = new ItemInfo()
        {
            Name = "罠[20]",
            Value = -20,
            PickupMessage = "罠[20] にひっかかった！",
            IsCurry = false,
        };

        /// <summary>
        /// 罠[50]
        /// </summary>
        public static ItemInfo Trap50 = new ItemInfo()
        {
            Name = "罠[50]",
            Value = -50,
            PickupMessage = "罠[50] にひっかかった！",
            IsCurry = false,
        };
    }
}
