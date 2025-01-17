using DotnetAPI;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    { 
        _dapper =  new DataContextDapper(config);
    }

[HttpGet("TesteConnection")]
public DateTime TesteConnection()
{
    return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
}

[HttpGet("GetUsers")]

//public IEnumerable<User> GetUsers()

public IEnumerable<User> GetUsers()
{
    string sql = @"
    SELECT [UserId],
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active] 
    FROM TutorialAppSchema.Users";
    IEnumerable<User> users = _dapper.LoadData<User>(sql);
    return users;
    //return new string[]{"user1", "user2"};
}

[HttpGet("GetSingleUser/{userId}")]
//public IEnumerable<User> GetUsers()
public User GetSingleUser(int userId)
{
    string sql = @"
    SELECT [UserId],
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active] 
    FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();
    User user = _dapper.LoadDataSingle<User>(sql);
    return user;
}

[HttpPut("EditUser")]
public IActionResult EditUser(User user)
{
    string sql = @"
    UPDATE TutorialAppSchema.Users
        SET [FirstName]= '" + user.FirstName + 
        "', [LastName]= '"+ user.LastName +
        "', [Email] ='" + user.Email + 
        "', [Gender] = '" + user.Gender + 
        "', [Active] = '" + user.Active + 
             "' WHERE UserId = " + user.UserId;
    Console.WriteLine(sql);
    
    if (_dapper.ExecuteSql(sql))
    {
        return Ok();
    }

    throw new Exception("Failed to update User");
}

[HttpPost("AddUser")]
public IActionResult AddUser(UserAddDto user)
{
        string sql =  @"INSERT INTO TutorialAppSchema.Users(
        [FirstName],
        [LastName],
        [Email],
        [Gender],
        [Active]
    ) VALUES (" +
        "'" + user.FirstName + 
         "', '"+ user.LastName +
         "', '" + user.Email + 
         "', '" + user.Gender + 
         "', '" + user.Active +

    "')";
    
    Console.WriteLine(sql);
    
    if (_dapper.ExecuteSql(sql))
    {
        return Ok();
    }

    throw new Exception("Failed to Add User");
}

[HttpDelete("DeleteUser/{userId}")]
public IActionResult DeleteUser(int userId)
{

    string sql = @"
    DELETE FROM TutorialAppSchema.Users 
        WHERE UserId = " + userId.ToString();    

    Console.WriteLine(sql);
    
    if (_dapper.ExecuteSql(sql))
    {
        return Ok();
    }

    throw new Exception("Failed to Delete User");
}

}