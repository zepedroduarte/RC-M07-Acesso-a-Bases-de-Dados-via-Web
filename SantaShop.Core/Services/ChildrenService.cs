﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SantaShop.Core.Services
{
    public class ChildrenService : BaseServiceClass
    {
        public ChildrenService()
        {
            BaseSelect = @"
                            select	
                                [ChildID]
                                ,[ChildName]
                                ,[YearOfBirth]
                                ,behave.[BehaviorID]
                                ,[BehaviorDescription]
                                ,[IsEligibleForPresent]
                                ,present.[PresentID]
                                ,[PresentName]
                            from
                                SantaShop.Children as child
                            inner join
                                SantaShop.Behaviors as behave on behave.BehaviorID = child.BehaviorID
                            inner join 
                                SantaShop.Presents as present on present.PresentID = child.PresentID";
        }
    }
}
