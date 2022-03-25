using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;
using WebDocumentViewerCustomizeParametersPanel.PredefinedReports;
using WebDocumentViewerCustomizeParametersPanel.Data;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.Expressions;

namespace WebDocumentViewerCustomizeParametersPanel.Services
{
    public class CustomReportProvider : IReportProvider {
        public XtraReport GetReport(string id, ReportProviderContext context)
        {
            if (id == "TestReport") {
                var report = new TestReport();
                ParameterPanelFluentBuilder.Begin(report)
                    .AddGroupItem(g0 => g0
                        .WithTitle("Select dates")
                        .AddParameterItem(report.Parameters[0], p0 => p0
                            .WithLabelOrientation(Orientation.Vertical)))
                    .AddGroupItem(g1 => g1
                        .WithTitle("Select a customer")
                        .WithOrientation(Orientation.Horizontal)
                        .WithShowExpandButton(true)
                        .AddParameterItem(report.Parameters[1], p1 => p1
                            .WithLabelOrientation(Orientation.Vertical))
                        .AddSeparatorItem()
                        .AddParameterItem(report.Parameters[2], p2 => p2
                            .WithLabelOrientation(Orientation.Vertical)))
                .End();

                report.Parameters["customer"].ExpressionBindings.Add(
                    new BasicExpressionBinding()
                    {
                        PropertyName = "Enabled",
                        Expression = "!IsNullOrEmpty(?company)",
                    }
                );
                return report;
            }
            return new XtraReport();
        }
    }
}
