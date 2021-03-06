using System;
using System.Linq;
using System.Collections;
using System.Web.UI;
using System.Data;
using System.IO;


namespace Assgt1.Controllers
{
    public class CsvDataSourceView : DataSourceView
    {


        //owner parameter is required since it is a formal attribute of DataSourceView
        public CsvDataSourceView(IDataSource owner, string name) : base(owner, DefaultViewName)
        {
        }
        // The data source view is named. However, the CsvDataSource
        // only supports one view, so the name is ignored, and the
        // default name used instead.
        public static string DefaultViewName = "CommaSeparatedView";

        // The location of the .csv file.
        private string sourceFile = String.Empty;
        internal string SourceFile
        {
            get
            {
                return sourceFile;
            }
            set
            {
                sourceFile = value;
            }
        }

        // Do not add the column names as a data row. Infer columns if the CSV file does
        // not include column names.
        private bool columns = false;
        internal bool IncludesColumnNames
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        // Get data from the underlying data source.
        // Build and return a DataView, regardless of mode.
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            IEnumerable dataList = null;
            // Open the .csv file.
            if (File.Exists(this.SourceFile))
            {
                DataTable data = new DataTable();

                // Open the file to read from.
                using (StreamReader sr = File.OpenText(this.SourceFile))
                {
                    // Parse the line
                    string s = "";
                    string[] dataValues;
                    DataColumn col;

                    // Do the following to add schema.
                    dataValues = sr.ReadLine().Split(',');
                    // For each token in the comma-delimited string, add a column
                    // to the DataTable schema.
                    foreach (string token in dataValues)
                    {
                        col = new DataColumn(token, typeof(string));
                        data.Columns.Add(col);
                    }

                    // Do not add the first row as data if the CSV file includes column names.
                    if (!IncludesColumnNames)
                        data.Rows.Add(CopyRowData(dataValues, data.NewRow()));

                    // Do the following to add data.
                    while ((s = sr.ReadLine()) != null)
                    {
                        dataValues = s.Split(',');
                        data.Rows.Add(CopyRowData(dataValues, data.NewRow()));
                    }
                }
                data.AcceptChanges();
                DataView dataView = new DataView(data);
                if (!string.IsNullOrEmpty(selectArgs.SortExpression))
                {
                    dataView.Sort = selectArgs.SortExpression;
                }
                dataList = dataView;
            }
            else
            {
                throw new System.Configuration.ConfigurationErrorsException("File not found, " + this.SourceFile);
            }

            if (null == dataList)
            {
                throw new InvalidOperationException("No data loaded from data source.");
            }

            return dataList;
        }

        private DataRow CopyRowData(string[] source, DataRow target)
        {
            try
            {
                for (int i = 0; i < source.Length; i++)
                {
                    target[i] = source[i];
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                // There are more columns in this row than
                // the original schema allows.  Stop copying
                // and return the DataRow.
                return target;
            }
            return target;
        }
        // The CsvDataSourceView does not currently
        // permit deletion. You can modify or extend
        // this sample to do so.
        public override bool CanDelete
        {
            get
            {
                return false;
            }
        }
        protected override int ExecuteDelete(IDictionary keys, IDictionary values)
        {
            throw new NotSupportedException();
        }
        // The CsvDataSourceView does not currently
        // permit insertion of a new record. You can
        // modify or extend this sample to do so.
        public override bool CanInsert
        {
            get
            {
                return false;
            }
        }
        protected override int ExecuteInsert(IDictionary values)
        {
            throw new NotSupportedException();
        }
        // The CsvDataSourceView does not currently
        // permit update operations. You can modify or
        // extend this sample to do so.
        public override bool CanUpdate
        {
            get
            {
                return false;
            }
        }
        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            throw new NotSupportedException();
        }
    }
}