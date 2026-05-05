﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class Student : BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public long ClassId { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Status { get; set; }
        public string MargeType { get; set; }
        public long Code { get; set; }
        public int PlaceNumber { get; set; }
        public int HallNumber { get; set; }
        public int PlaceInHall { get; set; }
        public int ExamTemplte { get; set; }
        public Classes Class { get; set; }
        public List<StudentScoreData> Scores { get; set; } = new List<StudentScoreData>();
        public List<StudentAbsentData> Absents { get; set; } = new List<StudentAbsentData>();
        public List<StudentAbsentCases> AbsentCases { get; set; } = new List<StudentAbsentCases>();
        public List<StudentExamScoreData> Exams { get; set; } = new List<StudentExamScoreData>();
    }
}