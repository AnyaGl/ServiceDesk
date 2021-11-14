namespace ServiceDesk.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department MainDepartment { get; set; }
    }
}
