﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class SendStudentToSchoolSpecification : BaseSpecification<SendStudentToSchool, long>
    {
        public SendStudentToSchoolSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public SendStudentToSchoolSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public SendStudentToSchoolSpecification(SendStudentToSchoolParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.StudentId.HasValue || Params.StudentId == p.StudentId)
            && (!Params.SchoolSenderId.HasValue || Params.SchoolSenderId == p.SchoolSenderId)
            && (!Params.SchoolReceiverId.HasValue || Params.SchoolReceiverId == p.SchoolReceiverId)
            && (!Params.CanBeSend.HasValue || Params.CanBeSend == p.CanBeSend)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.SchoolSender);
            Includes.Add(p => p.SchoolReceiver);
        }
    }
}