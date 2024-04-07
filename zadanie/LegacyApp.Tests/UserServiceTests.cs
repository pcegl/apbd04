namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Returns_False_When_First_Name_Is_Empty()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            null,
            "Kowalski",
            "kowalski@kowal.com",
            DateTime.Parse("2000-01-01"),
            1
        );
        
        // Assert
        
        //Assert.Equal(false, result);
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Throws_Exception_When_Client_Does_Not_Exist()
    {
        // Arrange
        var userService = new UserService();
        
        // Act

        Action action = () => userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowal.com",
            DateTime.Parse("2000-01-01"),
            100
        );
        
        // Assert
        
        Assert.Throws<ArgumentException>(action);
    }
    
    [Fact]
    public void AddUser_Returns_False_When_Email_Without_At_And_Dot()
    {
        // Arrange
        var userService = new UserService();
        
        // Act

        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalskikowalcom",
            DateTime.Parse("2000-01-01"),
            2
        );
        
        // Assert
        
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenAgeUnder21()
    {
        var userService = new UserService();
        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowal.com",
            DateTime.Parse("2020-01-01"),
            2
        );
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser__Returns_False_When_User_Has_CreditLimit_Below_500()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "Jan",
            "Kowalski",
            "jan@kowal.com",
            DateTime.Parse("1982-03-21"),
            1);
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Returns_True_When_User_Is_Important_Client()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "John",
            "Smith",
            "smith@gmail.pl",
            DateTime.Parse("1980-05-01"),
            3);
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Returns_True_When_User_Is_Very_Important_Client()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "Jan",
            "Malewski",
            "malewski@gmail.pl",
            DateTime.Parse("1990-10-25"),
            2);
    
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Returns_True_When_User_Is_NormalClient_And_CreditLimit_Is_Above_500()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "Jan",
            "Kwiatkowski",
            "kwiatkowski@wp.pl",
            DateTime.Parse("1990-10-25"),
            5);
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Returns_True_When_User_Has_CreditLimit_Above_500()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "John",
            "Doe",
            "doe@gmail.pl",
            DateTime.Parse("1982-03-21"),
            4);
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Returns_False_When_LastName_Is_Missing()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "John",
            "",
            "doe@gmail.com",
            DateTime.Parse("1982-03-21"),
            1);
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Returns_False_When_FirstName_Is_Missing()
    {
        var userService = new UserService();
        var addResult = userService.AddUser(
            "", 
            "Doe",
            "doe@gmail.com",
            DateTime.Parse("1982-03-21"),
            1);
        Assert.False(addResult);
    }
}