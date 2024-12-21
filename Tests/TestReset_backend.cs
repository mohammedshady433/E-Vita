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
        var newPass = "newPassword123";
        var adminPass = "adminPassword123";
        var userName = "doctor"; // Ensure this is a string
        var doctor = new Doctor
        {
            Doctor_ID = 1,
            Name = "Dr. Sameh",
            Speciality = "beauty",
            User_Name = "doctor",
            Pass = "password"
        };
        var doc_ID = doctor.Doctor_ID; // Use the doctor ID as an int
        var ID = 100;

        // Act
        var resetPassLog = new Reset_Pass_Log
        {
            Date = date,
            New_Pass = newPass,
            Admin_Pass = adminPass,
            User_Name = userName, // Assign the correct string value
            Doc_ID = docId, // Assign the correct int value
            doc_id = doctor, // Reference the Doctor object
            ID = id
        };

        // Assert
        Assert.Equal(date, resetPassLog.Date);
        Assert.Equal(newPass, resetPassLog.New_Pass);
        Assert.Equal(adminPass, resetPassLog.Admin_Pass);
        Assert.Equal(userName, resetPassLog.User_Name);
        Assert.Equal(docId, resetPassLog.Doc_ID);
        Assert.Equal(doctor, resetPassLog.doc_id);
        Assert.Equal(id, resetPassLog.ID);
    }
}
