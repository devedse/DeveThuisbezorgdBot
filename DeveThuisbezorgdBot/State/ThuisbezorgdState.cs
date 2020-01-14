using System.Collections.Generic;

namespace DeveThuisbezorgdBot.State
{
    public class ThuisbezorgdState
    {
        public List<UserFoodSmikkeltje> FoodSmikkelaars { get; set; } = new List<UserFoodSmikkeltje>();
        public long Initiator { get; set; }
    }
}
