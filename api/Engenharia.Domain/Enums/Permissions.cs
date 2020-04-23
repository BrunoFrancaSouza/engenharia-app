﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Engenharia.Domain.Auth
{
    public enum Permissions : int
    {
        NotSet = 0, //error condition

        [Display(GroupName = "UserAdmin", Name = "Read users", Description = "Can list User")]
        UserRead = 1,
        //This is an example of grouping multiple actions under one permission
        [Display(GroupName = "UserAdmin", Name = "Alter user", Description = "Can do anything to the User")]
        UserChange = 2,

        //Here is an example of very detailed control over something
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        StockRead = 10,
        [Display(GroupName = "Stock", Name = "Add new", Description = "Can add a new stock item")]
        StockAddNew = 13,
        [Display(GroupName = "Stock", Name = "Remove", Description = "Can read sales data")]
        StockRemove = 14,

        [Display(GroupName = "Sales", Name = "Read", Description = "Can delete a stock item")]
        SalesRead = 20,
        [Display(GroupName = "Sales", Name = "Sell", Description = "Can sell items from stock")]
        SalesSell = 21,
        [Display(GroupName = "Sales", Name = "Return", Description = "Can return an item to stock")]
        SalesReturn = 22,

        [Display(GroupName = "Employees", Name = "Read", Description = "Can read company employees")]
        EmployeeRead = 30,

       

        [Display(GroupName = "UserAdmin", Name = "Read Roles", Description = "Can list Role")]
        RoleRead = 50,
        [Display(GroupName = "UserAdmin", Name = "Change Role", Description = "Can create, update or delete a Role")]
        RoleChange = 51,

        [Display(GroupName = "CacheTest", Name = "Cache1", Description = "Base permission to update permission test")]
        Cache1 = 60,
        [Display(GroupName = "CacheTest", Name = "Cache2", Description = "Permission to toggle for update permission test")]
        Cache2 = 61,

        [Display(GroupName = "Impersonation", Name = "Impersonate - straight", Description = "Impersonate user using their permissions")]
        Impersonate = 70,
        [Display(GroupName = "Impersonation", Name = "Impersonate - enhanced", Description = "Impersonate user using current permissions")]
        ImpersonateKeepOwnPermissions = 71,

        //This is an example of what to do with permission you don't used anymore.
        //You don't want its number to be reused as it could cause problems 
        //Just mark it as obsolete and the PermissionDisplay code won't show it
        [Obsolete]
        [Display(GroupName = "Old", Name = "Not used", Description = "example of old permission")]
        OldPermissionNotUsed = 100,

        //This is an example of a permission linked to a optional (paid for?) feature
        //The code that turns roles to permissions can
        //remove this permission if the user isn't allowed to access this feature
        //[LinkedToModule(PaidForModules.Feature1)]
        //[Display(GroupName = "Features", Name = "Feature1", Description = "Can access feature1")]
        //Feature1Access = 1000,
        //[LinkedToModule(PaidForModules.Feature2)]
        //[Display(GroupName = "Features", Name = "Feature2", Description = "Can access feature2")]
        //Feature2Access = 1001,

        //[Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        //AccessAll = Int16.MaxValue,
    }
}