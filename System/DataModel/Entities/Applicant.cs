using System;
using System.Collections.Generic;

namespace DataContext;

public partial class Applicant
{
    public long Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Other { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string Linkedin { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<ApplicantPosition> ApplicantsPositions { get; set; } = new List<ApplicantPosition>();
}
