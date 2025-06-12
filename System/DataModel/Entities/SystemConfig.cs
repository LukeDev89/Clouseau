using System;
using System.Collections.Generic;

namespace DataContext;

public partial class SystemConfig
{
    public long Id { get; set; }

    public string Name { get => string.IsNullOrEmpty(_name) ? "" : _name.Trim(); set { _name = value; } }
    private string _name;

    public string? Description { get => string.IsNullOrEmpty(_description) ? "" : _description.Trim(); set { _description = value; } }
    private string? _description;

    public short DataType { get; set; }
    public string DataTypeName => ((DataModel.Enums.ValueType)DataType).ToString();

    public string DataValue { get => string.IsNullOrEmpty(_dataValue) ? "" : _dataValue.Trim(); set { _dataValue = value; } }
    private string _dataValue;

    public string? PreviousValue { get => string.IsNullOrEmpty(_previousValue) ? "" : _previousValue.Trim(); set { _previousValue = value; } }
    private string? _previousValue;
}
