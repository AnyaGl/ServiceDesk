﻿namespace ServiceDesk.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Department Department { get; set; }
    }
}
