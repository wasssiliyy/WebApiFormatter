using WebApiDemoG.Dtos;
using WebApiDemoG.Entities;

namespace WebApiDemoG.Models
{
    public class StudentModel
    {
        public StudentModel() { }
        public StudentModel(List<string> Arr)
        {
            Name = Arr[0].Substring(Arr[0].IndexOf(":") + 1).Trim();
            Age = int.Parse(Arr[1].Substring(Arr[1].IndexOf(":") + 1).Trim());
            SeriaNo = Arr[2].Substring(Arr[2].IndexOf(":") + 1).Trim();
            Score = int.Parse(Arr[3].Substring(Arr[3].IndexOf(":") + 1).Trim());
        }
        public StudentModel(StudentDto studentdto)
        { 
            Id = studentdto.Id;
            Name = studentdto.Fullname;
            SeriaNo = studentdto.SeriaNo;
            Age = studentdto.Age;
            Score = studentdto.Score;
        }
        public StudentModel(Student student)
        {
            Id = student.Id;
            Name = student.Fullname;
            SeriaNo = student.SeriaNo;
            Age = student.Age;
            Score = student.Score;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeriaNo { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }


    }
}
