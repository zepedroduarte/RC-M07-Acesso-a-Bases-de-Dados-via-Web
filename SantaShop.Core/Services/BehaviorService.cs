using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SantaShop.Core.Services
{
    public class BehaviorService : BaseServiceClass
    {
        public BehaviorService() 
        {
            BaseSelect = "select * from SantaShop.Behaviors";
        }
    }
}
