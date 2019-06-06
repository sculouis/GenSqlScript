using System;
using System.Collections;

namespace VSMac
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "/Users/louischen/Downloads/上課.xlsx";
            var excelObj = new ExcelOper(fileName);
            var TableName = "ClassMaster";
            var dt = excelObj.GenSql(TableName);
            var schemaObj = new TableSchema(dt);
            schemaObj.TableName = TableName;
            string content = schemaObj.TransformText();
            System.IO.File.WriteAllText("test.sql", content);
        }
    }
}
