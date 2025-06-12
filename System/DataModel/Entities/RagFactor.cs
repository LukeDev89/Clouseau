using System;
using System.Collections.Generic;

namespace DataContext;

public partial class RagFactor
{
    public long Id { get; set; }

    public string Name { get { return _Name.Trim(); } set { _Name = value; } }
    private string _Name = "";
    
    public string Description { get { return _description.Trim(); } set { _description = value; } }
    private string _description = "";

    public string? Objetive { get { return string.IsNullOrEmpty(_objetive) ? "" : _objetive.Trim(); } set { _objetive = value!; } }
    private string? _objetive = "";

    public bool Active { get; set; }

    public virtual ICollection<RagStatus> RagStatuses { get; set; } = new List<RagStatus>();

    public virtual ICollection<RagTemplateFactor> RagTemplateFactors { get; set; } = new List<RagTemplateFactor>();
}
