using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Enums
{
    public enum Subjects
    {
        [Display(Name = "AE")]
        SoftwareDevelopment  = 10,
        ITS = 20
    }
}
