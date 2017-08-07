namespace Model
{
   public class GroupWiseSalesSummary
    {
        private string _Month;
        private int _PumpQuantity;
        private int _KitchenQuantity;
        public string Month
        {
            get
            {
                return _Month;

            }
            set
            {
                _Month = value;
            }
        }
        public int PumpQuantity
        {
            get
            {
                return _PumpQuantity;

            }
            set
            {
                _PumpQuantity = value;
            }
        }
        public int KitchenQuantity
        {
            get
            {
                return _KitchenQuantity;

            }
            set
            {
                _KitchenQuantity = value;
            }
        }
    }
}
