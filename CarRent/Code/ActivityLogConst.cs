/****************************** Module Header ******************************\
Module Name:  <Enum class>
Project:      CarRent
Author :     UmarFarooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codes
{
    /// <summary>
    /// Activity logs enums
    /// </summary>
    public enum ActivityLogModules
    {
        Plans=1,
        Users,
        Campaigns,
        Tasks,
        Financials,
        Reports,
        Roles,
        Files,
        Profile,
        Membership
    }

    /// <summary>
    /// Log description enum
    /// </summary>
    public enum ActivityLogDescription
    {
        #region "Plan Actions description"
        PlanView = 1,
        PlanCreate,
        PlanEdit,
        PlanDelete,
        #endregion

        /// <summary>
        /// Commission action description
        /// </summary>
        #region "commision action description"
        CommisionCreate,
        #endregion

        /// <summary>
        /// User activity Description
        /// </summary>
        #region "User Actions description"
        UserLogin,
        UserLogOut,
        UserView,
        UserCreate,
        UserEdit,
        UserChangePassword,
        UserDelete,
        UserProfileEdit,
        UserActiveInActive,
        #endregion
        
        /// <summary>
        /// User Role description
        /// </summary>
        #region "Role Actions Description"
        RoleCreate,
        UpdateRole,
        #endregion
        /// <summary>
        /// Membership description enum
        /// </summary>
        #region Membership Action Description
        MembershipNewAgentCreated,
        MembershipNewCustomerCreated,
        #endregion

        /// <summary>
        ///Task enum 
        /// </summary>
        #region Task Actions Description
        TaskCreate,
        TaskLimitSet,
        TaskStatusChange,
        ReminderMailsend,
        #endregion

        /// <summary>
        /// Customer Enum
        /// </summary>
        #region memberAgent Actions Description
        CustomerDelete,
        #endregion
    }
}