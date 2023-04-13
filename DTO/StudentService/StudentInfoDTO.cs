using System;

namespace Forum.DTO.StudentService
{
    /// <summary>
    /// 用于与前端交换的学生信息类
    /// </summary>
	public class StudentInfoDTO
    {
        public string term { get; set; } = "";

        public string grade { get; set; } = "";

        public string stuno { get; set; } = "";

        public string name { get; set; } = "";

        public string sex { get; set; } = "";

        public string avatar { get; set; } = "";

        public string class_fname { get; set; } = "";

        public string class_sname { get; set; } = "";

        public string level { get; set; } = "";

        public string cno { get; set; } = "";
    }
}

