using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Autofac.Features.ResolveAnything;
using AutoUpdaterDotNET;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.ModuleInjection;
using HandyControl.Tools;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Start.Cores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;


namespace Lanpuda.Client.Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IAbpApplicationWithInternalServiceProvider? _abpApplication;

        public App()
        {
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

            try
            {
                Log.Information("Starting WPF host.");

                _abpApplication = await AbpApplicationFactory.CreateAsync<ClientStartModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                });
                await _abpApplication.InitializeAsync();


                //UI线程未捕获异常处理事件
                DispatcherUnhandledException += App_DispatcherUnhandledException;
                //Task线程内未捕获异常处理事件
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
                //非UI线程未捕获异常处理事件
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                //设置HandyControl 语言
                ConfigHelper.Instance.SetLang("zh-cn");

                #region 国际化

                Dispatcher.Thread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
                #endregion

                #region 设置客户端自动更新

                IConfiguration _configuration = _abpApplication.Services.GetRequiredService<IConfiguration>();
                string appvVersion = _configuration["AppUpdate:CurrentVersion"] ?? "1.0";
                string updateUrl = _configuration["AppUpdate:UpdateUrl"] ?? "";

                AutoUpdater.InstalledVersion = new Version(appvVersion);
                AutoUpdater.ShowSkipButton = false;
                AutoUpdater.ShowRemindLaterButton = false;
                AutoUpdater.Start(updateUrl);

                #endregion


                System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;

                ModuleManager.DefaultManager.GetEvents(regionName: RegionNames.MainContentRegion).Navigation += OnRegionANavigationChanged;
                _abpApplication.Services.GetRequiredService<MainWindow>()?.Show();
                ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainWindow, ClientStartViewKeys.SplashScreen);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Information("CurrentDomain_UnhandledException:" + e.ExceptionObject);
            ;
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Information("CurrentDomain_UnhandledException:" + e.Exception.Message);
            ;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Information("CurrentDomain_UnhandledException:" + e.Exception.Message);
            ;
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            if (_abpApplication != null)
            {
                await _abpApplication.ShutdownAsync();
                Log.CloseAndFlush();
            }
        }


        protected void OnRegionANavigationChanged(object? sender, NavigationEventArgs e)
        {
            //RegionViewModelBase? regionViewModelBase = (RegionViewModelBase)e.NewViewModel;
            //if (regionViewModelBase != null)
            //{
            //    regionViewModelBase.Key = e.NewViewModelKey;
            //    if (e.OldViewModelKey != null)
            //    {
            //        ModuleManager.DefaultManager.Remove(RegionNames.MainContentRegion, e.OldViewModelKey);
            //    }
            //}
        }
    }

}
