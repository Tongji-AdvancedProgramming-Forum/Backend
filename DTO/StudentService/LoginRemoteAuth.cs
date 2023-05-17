using System;
using System.Text.Json.Serialization;

namespace Forum.DTO.StudentService
{
    public class RemoteAuthStudentDTO
    {
        public string enable { get; set; } = "";
        public string fname { get; set; } = "";
        public string grade { get; set; } = "";
        public string lcno { get; set; } = "";
        public string level { get; set; } = "";
        public string sex { get; set; } = "";
        public string sname { get; set; } = "";
        public string sno { get; set; } = "";
        public string stu_name { get; set; } = "";
        public string tcno { get; set; } = "";
        public string term { get; set; } = "";
    }

    public class RemoteAuthDTO
    {
        public bool success { get; set; }
        public RemoteAuthStudentDTO? data { get; set; }
    }
}