using System;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastLogin { get; set; }
}