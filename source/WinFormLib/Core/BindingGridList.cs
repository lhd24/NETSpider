using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq.Expressions;

namespace WinFormLib.Core
{
    public class BindingGridList
    {
        internal static object GetTypeList(Type p_Type)
        {
            Assembly _Assembly = Assembly.Load("WinFormLib.Core");
            Type _TypeList = _Assembly.GetType("WinFormLib.Core.BindingGridList`1[[" + p_Type.FullName + "," + p_Type.Assembly.FullName + "]]");
            object _List = System.Activator.CreateInstance(_TypeList);
            return _List;
        }
        public static object GetTypeList(Type p_Type, IList list)
        {
            Assembly _Assembly = Assembly.Load("WinFormLib.Core");
            Type _TypeList = _Assembly.GetType("WinFormLib.Core.BindingGridList`1[[" + p_Type.FullName + "," + p_Type.Assembly.FullName + "]]");
            object _List = System.Activator.CreateInstance(_TypeList, list);
            return _List;
        }
    }

    public interface IBindingGridListView : IBindingListView
    {
        bool RaiseListChangedEvents { get; set; }
        Expression ConditionExpression { get; set; }
        Type DataSourceType { get; }
        void EndEdit();
    }
    public class BindingGridList<T> : BindingList<T>, IBindingGridListView
    {
        IList<T> _list; IList<T> _bindList;
        public BindingGridList(IList<T> list)
            : base(list)
        {
            this._list = list;
            this._bindList = list;

        }
        public BindingGridList()
            : base()
        {
            this._bindList = this._list = new List<T>();
        }
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        private bool isSortedCore = true;
        protected override bool IsSortedCore
        {
            get
            {
                return isSortedCore;
            }
        }

        private ListSortDirection sortDirectionCore = ListSortDirection.Ascending;
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return sortDirectionCore;
            }
        }

        private PropertyDescriptor sortPropertyCore = null;
        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return sortPropertyCore;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;
            if (items != null)
            {
                ObjectPropertyCompare<T> pc = new ObjectPropertyCompare<T>(prop, direction);
                items.Sort(pc);
                isSortedCore = true;
                sortDirectionCore = direction;
                sortPropertyCore = prop;
            }
            else
            {
                isSortedCore = false;
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            isSortedCore = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        public void ApplySort(ListSortDescriptionCollection sorts)
        {

        }
        private string _filter = string.Empty;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                if (this.RaiseListChangedEvents)
                {
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1) { });
                    this.ResetBindings();
                }
            }
        }



        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
            if (this._list != null && !string.IsNullOrEmpty(this.Filter) && this.ConditionExpression != null)
            {
                Expression<Func<T, bool>> xExpr = this.ConditionExpression as Expression<Func<T, bool>>;

                //var a = this._list.Where(xExpr.Compile());
               
            }
        }
        public void RemoveFilter()
        {

        }
        public void EndEdit()
        {

        }
        public ListSortDescriptionCollection SortDescriptions
        {
            get
            {
                ListSortDescriptionCollection colList = new ListSortDescriptionCollection() { };
                return colList;
            }
        }

        public bool SupportsAdvancedSorting
        {
            get { return true; }
        }

        public bool SupportsFiltering
        {
            get { return true; }
        }


        public Expression ConditionExpression
        {
            get;
            set;
        }

        public Type DataSourceType
        {
            get { return typeof(T); }
        }
    }

    class ObjectPropertyCompare<T> : System.Collections.Generic.IComparer<T>
    {

        private PropertyDescriptor _property;
        public PropertyDescriptor property
        {
            get { return _property; }
            set { _property = value; }
        }
        private ListSortDirection _direction;
        public ListSortDirection direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public ObjectPropertyCompare()
        { }

        public ObjectPropertyCompare(PropertyDescriptor prop, ListSortDirection direction)
        {
            _property = prop;
            _direction = direction;
        }

        public int Compare(T x, T y)
        {

            object xValue = x.GetType().GetProperty(property.Name).GetValue(x, null);
            object yValue = y.GetType().GetProperty(property.Name).GetValue(y, null);

            int returnValue;

            if (xValue == null && yValue == null)
            {
                returnValue = 0;
            }
            else if (xValue == null)
            {
                returnValue = -1;
            }
            else if (yValue == null)
            {
                returnValue = 1;
            }
            else if (xValue is IComparable)
            {
                returnValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (xValue.Equals(yValue))
            {
                returnValue = 0;
            }
            else
            {
                returnValue = xValue.ToString().CompareTo(yValue.ToString());
            }

            if (direction == ListSortDirection.Ascending)
            {
                return returnValue;
            }
            else
            {
                return returnValue * -1;
            }

        }
    }
}
