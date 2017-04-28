/****************************** Module Header ******************************\
Module Name:  <Enum file>
Project:      CarRent
Author :    Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
namespace Codes
{
    #region UserDefine Enums
   

    public enum MailTemplatesDynamicFielsd
    {
        [Description("First Name")]
        FirstName = 1,
        [Description("Last Name")]
        LastName,
        [Description("Agent Id")]
        AgentID,
        [Description("Email Id")]
        EmailID,
        [Description("Company")]
        CompanyName,
        [Description("Membership")]
        CustomerClass,
        [Description("Address1")]
        Address,
        [Description("State")]
        State,
        [Description("City")]
        City,
        [Description("Zip")]
        Zip,
        [Description("Receiver EmailID")]
        ReceiverEmail,
        [Description("Receiver Name")]
        ReceiverName,
        [Description("Receiver Contact Number")]
        ReceiverContactNumber

    }

   
    public enum Status
    {
        Active,
        Inactive
    }

    public enum Direction
    {
        ASC,
        DESC
    }

    public enum UserType
    {

        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2
    }

    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female
    }

    public enum PaymentStatus
    {
        Pending,
        Rejected,
        Accepted
    }

    public enum Months
    {
        [Description("January")]
        Jan = 1,
        [Description("Februrary")]
        Feb,
        [Description("March")]
        Mar,
        [Description("April")]
        Apr,
        [Description("May")]
        May,
        [Description("Jun")]
        Jun,
        [Description("July")]
        Jul,
        [Description("Augaust")]
        Aug,
        [Description("September")]
        Sep,
        [Description("October")]
        Oct,
        [Description("November")]
        Nov,
        [Description("December")]
        Dec
    }

    public enum PageSize
    {
        [Description("10")]
        Ten = 10,
        [Description("25")]
        TwentyFive = 25,
        [Description("50")]
        Fifty = 50,
        [Description("100")]
        Hundred = 100
    }

    public enum Sort
    {
        Ascending,
        Descending
    }

    public enum MailReminderFrequency
    {
        [Description("Daily")]
        Daily = 1,
        [Description("Weekly")]
        Weekly,
        [Description("Monthly")]
        Monthly

    }

    public enum EnableDisable
    {
        [Description("Enable")]
        Enable = 1,
        [Description("Disable")]
        Disable = 0
    }

    public enum CardType
    {
        [Description("Visa")]
        Visa = 1,
        [Description("Discover")]
        Discover = 2,
        [Description("MasterCard")]
        MasterCard = 3,
        [Description("AMEX")]
        AMEX = 4,
        [Description("Solo")]
        Solo = 5,
        [Description("Switch")]
        Switch = 6,
        [Description("Maestro")]
        Maestro = 7
    }

    public enum CarType
    {
        [Description("Car")]
        Car = 1,
        [Description("Van")]
        Van = 2
    }

    public enum Depot
    {
        [Description("Chippenham/Bath")]
        ChippenhamBath = 1,
        [Description("Colwyn Bay")]
        ColwynBay = 2,
        [Description("Deeside")]
        Deeside = 3,
        [Description("Ely")]
        Ely = 4,
        [Description("Shrewsbury")]
        Shrewsbury = 5,
        [Description("Wrexham")]
        Wrexham = 6
    }

    public enum TimeSlots
    {
        [Description("07:00")]
        Seven = 1,
        [Description("08:00")]
        Eight = 2,
        [Description("09:00")]
        Nine = 3,
        [Description("10:00")]
        Ten = 4,
        [Description("11:00")]
        Eleven = 5,
        [Description("12:00")]
        Twelve = 6,
        [Description("13:00")]
        Thirteen = 7,
        [Description("14:00")]
        Fourteen = 8,
        [Description("15:00")]
        Fifteen = 9,
        [Description("16:00")]
        Sixteen = 10,
        [Description("17:00")]
        Seventeen = 11
    }

    public enum ContentType
    {
        [Description("Home")]
        Home = 1,
        [Description("About Us")]
        AboutUs = 2,
        [Description("Contact Us")]
        ContactUs = 3,
           [Description("Monthly Rental")]
         MonthlyRental = 4,
        [Description("Terms of Service")]
        TermsofService = 5,
        [Description("European Van Hire")]
        EuropeanVanHire = 6,
     
       
        



    }
    #endregion
}