using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class Administrations : BaseEntity<long>
    {
        public string Name { get; set; }
        public List<School> Schools { get; set; } = new List<School>();
        public List<AdministrationsVacation> Vacations { get; set; } = new List<AdministrationsVacation>();
    }
}