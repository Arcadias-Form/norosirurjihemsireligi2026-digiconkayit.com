using ClosedXML.Excel;
using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace ArcForm_Web_v2
{
    public partial class ExcelForm : Page
    {
        IList<string> segment;

        SurecVeriModel<DataTable> SDataModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                segment = Request.GetFriendlyUrlSegments();

                if (segment.Count.Equals(2) && segment.First().Equals("info@arkadyas.com", StringComparison.OrdinalIgnoreCase))
                {
                    switch (segment[1])
                    {
                        case "tr":
                        case "en":
                            SDataModel = new OdemeTablosuIslemler().Rapor(segment[1]);

                            if (SDataModel.Sonuc.Equals(Sonuclar.Basarili))
                            {
                                using (XLWorkbook xlWorkbook = new XLWorkbook())
                                {
                                    IXLWorksheet xlWorksheet = xlWorkbook.Worksheets.Add(SDataModel.Veriler);
                                    xlWorksheet.SheetView.FreezeRows(1);
                                    xlWorksheet.SheetView.FreezeColumns(1);
                                    xlWorkbook.SaveAs(Response.OutputStream);
                                }
                            }
                            break;
                        default:
                            Response.Redirect("~/tr");
                            break;
                    }
                }
            }
        }
    }
}