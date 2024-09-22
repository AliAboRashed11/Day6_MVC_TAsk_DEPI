namespace Day2.viewsModel
{
    public class InstractorDeplistVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Imageurl { get; set; }
        public int DepartmentID { get; set; }
        public int CourseID { get; set; }
        public List <DepartmentVm> DepartmentVm { get; set; }
    }
}
