using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Lanpuda.Client.Theme.Behaviors.Print
{
    public class FlowDocumentPrintService : ServiceBase, IPrintService
    {
        public void Print()
        {
            FlowDocumentReader documentReader = (FlowDocumentReader)AssociatedObject;
            FlowDocument doc = documentReader.Document;

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }
        }
    }
}
