using Resources.Scripts.Entities;
using Resources.Scripts.ItemsData;
using Resources.Scripts.ItemsData.MagicScrolls;

namespace Resources.Scripts.Items.MagicScrolls
{
    public class MSFactory
    {
        public MagicScroll CreateMagicScroll(MSData data)
        {
            MagicScroll magicScroll = null;
            switch (data.type)
            {
                case MSType.FireBall:
                    magicScroll = new MSFireBall(data as MSFireBallData);
                    break;
            }

            return magicScroll;
        }
    }
}