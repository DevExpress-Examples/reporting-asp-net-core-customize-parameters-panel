using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Security.Resources;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDocumentViewerCustomizeParametersPanel.Services;
using WebDocumentViewerCustomizeParametersPanel.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDevExpressControls();
//builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
builder.Services.AddScoped<IReportProvider, CustomReportProvider>();
builder.Services.AddMvc();
builder.Services.ConfigureReportingServices(configurator => {
    if(builder.Environment.IsDevelopment()) {
        configurator.UseDevelopmentMode();
    }
    configurator.ConfigureReportDesigner(designerConfigurator => {
    });
    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
        viewerConfigurator.UseCachedReportSourceBuilder();
        viewerConfigurator.RegisterConnectionProviderFactory<CustomSqlDataConnectionProviderFactory>();
    });
});
builder.Services.AddDbContext<ReportDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

var app = builder.Build();
using(var scope = app.Services.CreateScope()) {    
    var services = scope.ServiceProvider;    
    services.GetService<ReportDbContext>().InitializeDatabase();
}
var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "Content")).FullName);
AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());
app.UseDevExpressControls();
System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
} else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();