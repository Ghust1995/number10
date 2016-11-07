using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ImportType
{
    Int,
    Float,
    Char,
    String
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ImportData : System.Attribute
{
    private string _container;
    private string _data;
    private ImportType _type;

    public ImportData(string container, string data, ImportType type)
    {
        this._container = container;
        this._data = data;
        this._type = type;
    }
}
