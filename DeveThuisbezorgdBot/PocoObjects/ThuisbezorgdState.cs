using System.Collections.Generic;

namespace DeveThuisbezorgdBot.PocoObjects
{
    public class ThuisbezorgdState
    {
        public List<UserFoodSmikkeltje> FoodSmikkelaars { get; set; } = new List<UserFoodSmikkeltje>();
        public long Initiator { get; set; }
    }
}
