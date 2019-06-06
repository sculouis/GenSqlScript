using System;
using System.Data;

    public partial class TableSchema
    {
        public string TableName { get; set; }

        public DataTable Mdata { get; set; }

        public TableSchema(DataTable data)
        {
            this.Mdata = data;
        }


    }
