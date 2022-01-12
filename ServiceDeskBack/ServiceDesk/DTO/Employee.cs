namespace ServiceDesk.DTO.Employee
{
    public class Authorization
    {
        public string login { get; set; }
        public string password { get; set; }
    }
    public class AuthResult
    {
        public int errorCode { get; set; }
        public string guid { get; set; }
    }

    public class Department
    {
        public string guid { get; set; }
        public string name { get; set; }
    }

    public class Employee
    {
        public string guid { get; set; }
        public string name { get; set; }
        public Department department { get; set; }
        public string photoPath { get; set; }
    }
}
