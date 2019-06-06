using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace VSMac
{
    public class ExcelOper
    {
        public ExcelOper()
        {
        }

        /// <summary>
        /// 產生sql script語法,依據SheetName
        /// </summary>
        /// <param name="fileName">輸入檔名不包含路徑</param>
        public static void genSql(string fileName, ArrayList tables)
        {
            //序號 Key 欄位英文名稱 欄位中文名稱  資料型態 長度  Null 預設值 物件Mapping 備註  資料源(系統)
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    IWorkbook wb = new XSSFWorkbook(fs);
                    ISheet sheet;

                    //產生drop table script
                    //DropTableScript(tables);
                    foreach (var table in tables)
                    {
                        var tableName = table.ToString();          //取得資料表的名稱
                        sheet = wb.GetSheet(tableName);
                        var tableData1 = SheetData(sheet, 3, 2);
                        try
                        {
                            //GenDetail(tables, tableName, tableData1, file);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            throw;
                        }
                    }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="sheet">Sheet.</param>
        /// <param name="startRow">Start row.</param>
        /// <param name="titleRowNo">Title row no.</param>
        private static DataTable SheetData(ISheet sheet, int startRow, int titleRowNo)
        {
            DataTable table = new DataTable();

            //由第一列取標題做為欄位名稱
            var headerRow = sheet.GetRow(titleRowNo);
            int cellCount = headerRow.LastCellNum;
            table.TableName = sheet.SheetName;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                //以欄位文字為名新增欄位，此處全視為字串型別以求簡化
                table.Columns.Add(
                    new DataColumn(headerRow.GetCell(i).StringCellValue));

            //略過第三列(標題列)，一直處理至最後一列
            for (int i = (sheet.FirstRowNum + startRow); i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;
                DataRow dataRow = table.NewRow();
                //依先前取得的欄位數逐一設定欄位內容
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        //如要針對不同型別做個別處理，可善用.CellType判斷型別
                        //再用.StringCellValue, .DateCellValue, .NumericCellValue...取值
                        //此處只簡單轉成字串

                        if (row.GetCell(j).CellType == CellType.Formula)
                        {
                            if (row.GetCell(j).CachedFormulaResultType == CellType.Numeric)
                            {
                                dataRow[j] = row.GetCell(j).NumericCellValue;
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).StringCellValue;
                            }
                        }
                        else
                        {
                            Debug.WriteLine(row.GetCell(j).CellType);
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }




    }
}
