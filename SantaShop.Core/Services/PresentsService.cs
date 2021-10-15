using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SantaShop.Core.Services
{
    public class PresentsService : BaseServiceClass
    {
        public PresentsService()
        {
            BaseSelect = @"select * from SantaShop.Presents";
        }
    }
}
