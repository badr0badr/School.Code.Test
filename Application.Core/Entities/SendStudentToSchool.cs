﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class SendStudentToSchool : BaseEntity<long>
    {
        public long SchoolSenderId { get; set; }
        public long SchoolReceiverId { get; set; }
        public long StudentId { get; set; }
        public bool CanBeSend { get; set; }
        public Student Student { get; set; }
        public School SchoolSender { get; set; }
        public School SchoolReceiver { get; set; }
    }
}