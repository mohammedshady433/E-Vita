using System;
using Xunit;
using E_Vita.Models;

public class ResetPassLogTests
{
    [Fact]
    public void CanCreateResetPassLog()
    {
        // Arrange
        var date = DateTime.Now;
        var newPass = "DPass123";
        var adminPass = "APass000";
        var userName = "doctor";
        var doctor = new Doctor { Doctor_ID = 1, Name = "Dr. sameh", User_Name = "doctor", Pass = "123" };
        var id = 100;

        // Act
        var resetPassLog = new Reset_Pass_Log
        {
            Date = date,
            New_Pass = newPass,
            Admin_Pass = adminPass,
            User_Name = userName,
            user_name = doctor,
            ID = id
        };

        // Assert
        Assert.Equal(date, resetPassLog.Date);
        Assert.Equal(newPass, resetPassLog.New_Pass);
        Assert.Equal(adminPass, resetPassLog.Admin_Pass);
        Assert.Equal(userName, resetPassLog.User_Name);
        Assert.Equal(doctor, resetPassLog.user_name);
        Assert.Equal(id, resetPassLog.ID);
    }
}
