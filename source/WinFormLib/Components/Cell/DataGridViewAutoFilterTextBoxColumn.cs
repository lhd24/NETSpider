using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WinFormLib.Core;
using System.Drawing;

namespace WinFormLib.Components.Cell
{
    [ToolboxBitmap(typeof(DataGridViewTextBoxColumn))]
    /// <summary>
    /// Represents a DataGridViewTextBoxColumn with a drop-down filter list accessible from the header cell.  
    /// </summary>
    public class DataGridViewAutoFilterTextBoxColumn : DataGridViewColumn
    {
        /// <summary>
        /// Initializes a new instance of the DataGridViewAutoFilterTextBoxColumn class.
        /// </summary>
        public DataGridViewAutoFilterTextBoxColumn()
            : base(new DataGridViewAutoFilterTextBoxCell())
        {
            base.DefaultHeaderCellType = typeof(DataGridViewAutoFilterColumnHeaderCell);
            base.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        #region public properties that hide inherited, non-virtual properties: DefaultHeaderCellType and SortMode

        /// <summary>
        /// Returns the AutoFilter header cell type. This property hides the 
        /// non-virtual DefaultHeaderCellType property inherited from the 
        /// DataGridViewBand class. The inherited property is set in the 
        /// DataGridViewAutoFilterTextBoxColumn constructor. 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Type DefaultHeaderCellType
        {
            get
            {
                return typeof(DataGridViewAutoFilterColumnHeaderCell);
            }
        }
        private DataCellType _dataFormater = DataCellType.None;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataCellType DataCellType
        {
            get { return _dataFormater; }
            set
            {
                _dataFormater = value;
            }
        }
        [Localizable(true)]
        [Description("格式化格式")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public string DataCellTypeFormater
        {
            get;
            set;
        }

        public override object Clone()
        {
            DataGridViewAutoFilterTextBoxColumn column1 = (DataGridViewAutoFilterTextBoxColumn)base.Clone();
            column1.DataCellType = this._dataFormater;
            column1.DataCellTypeFormater = this.DataCellTypeFormater;
            column1.CellTemplate = new DataGridViewAutoFilterTextBoxCell();
            return column1;
        }


        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewAutoFilterTextBoxCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewAutoFilterTextBoxCell");
                }

                base.CellTemplate = value;
            }
        }
        /// <summary>
        /// Gets or sets the sort mode for the column and prevents it from being 
        /// set to Automatic, which would interfere with the proper functioning 
        /// of the drop-down button. This property hides the non-virtual 
        /// DataGridViewColumn.SortMode property from the designer. The inherited 
        /// property is set in the DataGridViewAutoFilterTextBoxColumn constructor.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        [DefaultValue(DataGridViewColumnSortMode.Programmatic)]
        public new DataGridViewColumnSortMode SortMode
        {
            get
            {
                return base.SortMode;
            }
            set
            {
                if (value == DataGridViewColumnSortMode.Automatic)
                {
                    throw new InvalidOperationException(
                        "A SortMode value of Automatic is incompatible with " +
                        "the DataGridViewAutoFilterColumnHeaderCell type. " +
                        "Use the AutomaticSortingEnabled property instead.");
                }
                else
                {
                    base.SortMode = value;
                }
            }
        }

        #endregion

        #region public properties: FilteringEnabled, AutomaticSortingEnabled, DropDownListBoxMaxLines

        /// <summary>
        /// Gets or sets a value indicating whether filtering is enabled for this column. 
        /// </summary>
        [DefaultValue(true)]
        public Boolean FilteringEnabled
        {
            get
            {
                // Return the header-cell value.
                return ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .FilteringEnabled;
            }
            set
            {
                // Set the header-cell property. 
                ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .FilteringEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether automatic sorting is enabled for this column. 
        /// </summary>
        [DefaultValue(true)]
        public Boolean AutomaticSortingEnabled
        {
            get
            {
                // Return the header-cell value.
                return ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .AutomaticSortingEnabled;
            }
            set
            {
                // Set the header-cell property.
                ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .AutomaticSortingEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum height of the drop-down filter list for this column. 
        /// </summary>
        [DefaultValue(20)]
        public Int32 DropDownListBoxMaxLines
        {
            get
            {
                // Return the header-cell value.
                return ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .DropDownListBoxMaxLines;
            }
            set
            {
                // Set the header-cell property.
                ((DataGridViewAutoFilterColumnHeaderCell)HeaderCell)
                    .DropDownListBoxMaxLines = value;
            }
        }

        #endregion public properties

        #region public, static, convenience methods: RemoveFilter and GetFilterStatus

        /// <summary>
        /// Removes the filter from the BindingSource bound to the specified DataGridView. 
        /// </summary>
        /// <param name="dataGridView">The DataGridView bound to the BindingSource to unfilter.</param>
        public static void RemoveFilter(DataGridView dataGridView)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView);
        }

        /// <summary>
        /// Gets a status string for the specified DataGridView indicating the 
        /// number of visible rows in the bound, filtered BindingSource, or 
        /// String.Empty if all rows are currently visible. 
        /// </summary>
        /// <param name="dataGridView">The DataGridView bound to the 
        /// BindingSource to return the filter status for.</param>
        /// <returns>A string in the format "x of y records found" where x is 
        /// the number of rows currently displayed and y is the number of rows 
        /// available, or String.Empty if all rows are currently displayed.</returns>
        public static String GetFilterStatus(DataGridView dataGridView)
        {
            return DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridView);
        }

        #endregion
    }
}
