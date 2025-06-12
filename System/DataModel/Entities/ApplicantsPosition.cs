using System;
using System.Collections.Generic;

namespace DataContext;

public partial class ApplicantsPosition
{
    public long Id { get; set; }

    public long ApplicantId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Applicant Applicant { get; set; } = null!;
}
