using SQLite;

namespace myCIIEmployee
{
    public class LogedInUser
    {
        public LogedInUser()
        {
        }

        [PrimaryKey]
        public string EmployeeId { get; set; }

        public string Photograph { get; set; }
        public string Designation { get; set; }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }


        
        
            
    }
}
