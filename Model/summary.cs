namespace Model
{
  public class summary
    {
        private int _ProductGroupID;
        private string _Month;
        private string _ProductGroupName;
        private int _TotalQuantity;
     
      public int ProductGroupID
        {
            get
            {
                return _ProductGroupID;
                
            }
            set
            {
                _ProductGroupID = value;
            }
        }
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
        public string ProductGroupName
        {
            get
            {
                return _ProductGroupName;

            }
            set
            {
                _ProductGroupName = value;
            }
        }
        public int TotalQuantity
        {
            get
            {
                return _TotalQuantity;

            }
            set
            {
                _TotalQuantity = value;
            }
        }
        
    }
}
