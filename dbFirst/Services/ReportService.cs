using dbFirst.Models;
using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Style;
using dbFirst.Models;

namespace dbFirst.Services
{
    public class ReportService 
    {
        public void Create(List<CountFli> ob)
        {
            FileInfo test = new FileInfo("sample1.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(test))
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("CountFligth");
                //Add the headers
                worksheet.Cells[1, 1].Value = "AirportCode";
                worksheet.Cells[1, 2].Value = "CountFlight";
                int i = 1;
                foreach (var item in ob) 
                {
                    i++;
                    
                    //Add some items...
                    worksheet.Cells[$"A{i}"].Value = item.AirportCode;
                    worksheet.Cells[$"B{i}"].Value = item.CountFlight;
                    
                }
                

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                // Change the sheet view to show it in page layout mode
                worksheet.View.PageLayoutView = true;

             

                var xlFile = new FileInfo("sample1.xlsx");
                // save our new workbook in the output directory and we are done!
                package.SaveAs(xlFile);
                //return xlFile.FullName;
            }
        }
    }
}
