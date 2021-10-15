using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SantaShop.ConsV2.Services
{
    public class PresentsService : BaseServiceClass
    {
        public PresentsService(IDbConnection dbConnection) : base(dbConnection)
        {
            BaseSelect = @"select * from SantaShop.Presents";
        }
    }
}
