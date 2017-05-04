using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormLib.Core;

namespace WinFormLib.Controls
{
    public interface IDataControl<T> : IDataControlFomatter<T>, IDataControl
    {
        T Value { get; }
        T DataViewValue { set; get; }
        T DefaultValue { get; set; }
    }
    public interface IDataControlFomatter<T>
    {
        T ValueFomatted(T source, DataCellType dataFormater);
    }
    public interface IDataControl
    {
        bool ReadOnlyValue { get; set; }
        void ClearData();
        string IsValid();
        bool FillEntity(ref object Entity);
        void UnFillEntity(object Entity);
        DataCellType DataFormater { get; set; }
        string DataFiled { get; set; }
        string DataControlName { get; set; }
    }
}
