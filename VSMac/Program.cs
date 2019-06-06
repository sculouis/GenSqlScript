using System;
using System.Collections;

namespace VSMac
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "/Users/louischen/Downloads/上課.xlsx";
            var tables = new ArrayList();
            tables.Add("ClassMaster");
            ExcelOper.genSql(fileName,tables);
            Console.WriteLine("Hello World!");
        }
    }
}
