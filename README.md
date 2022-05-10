<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/474049810/2021.2)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1077916)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Reporting for ASP.NET Core - Customize the Document Viewer's Parameters Panel

This example uses the [ParameterPanelFluentBuilder](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Parameters.ParameterPanelFluentBuilder) class to customize the [Document Viewer's](https://docs.devexpress.com/XtraReports/400248/web-reporting/asp-net-core-reporting/document-viewer-in-asp-net-core-applications) **Parameters** panel as follows:

1. Unite report parameters into groups and place parameters side-by-side.
2. Place a label and editor vertically for each parameter.
3. Add a separator between parameters inside a group.

| Default panel | Customized panel |
| :-: | :-: |
| ![Default panel](Images/DefaultParametersPanel.png) | ![Customized panel](Images/CustomizedParametersPanel.png) |

The example also specifies an [expression](https://docs.devexpress.com/XtraReports/120091/detailed-guide-to-devexpress-reporting/use-expressions) for the [Enabled](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraReports.Parameters.Parameter.Enabled) property to enable/disable a parameter's editor based on a value of another parameter.

The following code sample demonstrates how to transform the default panel above to the customized panel:

```
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
    new BasicExpressionBinding() {
        PropertyName = "Enabled",
        Expression = "!IsNullOrEmpty(?company)",
    }
);
```

To customize the **Parameters** panel in an ASP.NET Core Reporting application, do the following:

1. Create an instance of a report whose **Parameters** panel you want to customize.
2. Pass the instance to the **ParameterPanelFluentBuilder** class **Begin** method and call customization methods.
3. Open the report preview as described in the following topic: [Open a Report in ASP.NET Core Application](https://docs.devexpress.com/XtraReports/402505/web-reporting/asp-net-core-reporting/document-viewer-in-asp-net-applications/open-a-report).

<!-- default file list -->
## Files to Look At

- [CustomReportProvider.cs](./CS/Services/CustomReportProvider.cs#L17)

<!-- default file list end -->

## Documentation

- [ParameterPanelFluentBuilder Class](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Parameters.ParameterPanelFluentBuilder)
- [The Parameters Panel](https://docs.devexpress.com/XtraReports/402960/detailed-guide-to-devexpress-reporting/use-report-parameters/parameters-panel)
