using ClosedXML.Excel;

namespace LR7.V3.Services {
    public class GenerateMethodService : IGenerateMethodService {

        public byte[] GenerateExcelFile() {
            using (var ms = new MemoryStream()) {
                using (var wb = new XLWorkbook()) {
                    var ws = wb.Worksheets.Add("Sheet1");
                    ws.Cell("A1").Value = "Hello, my name is";
                    ws.Cell("B1").Value = "Max Bukator";
                    wb.SaveAs(ms);

                    return ms.ToArray();
                }
            }
        }
    }
}
