using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Domain.Entities
{
    public partial class Speakers
    {
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
