using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using HandyControl.Controls;
using IdentityModel.Client;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme;
using Lanpuda.Client.Theme.ACL;
using Lanpuda.Client.Theme.Services.SettingsServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.Json;
using static IdentityModel.OidcConstants;
using static System.Formats.Asn1.AsnWriter;
using IModuleManager = DevExpress.Mvvm.ModuleInjection.IModuleManager;

namespace Lanpuda.Account.UI.Login
{
    public class LoginViewModel : RootViewModelBase
    {
        private readonly IConfiguration _configuration;
        public LoginModel Model { get; set; }
        private IModuleManager _moduleManager;
        private readonly ISettingsService _settingsService;
        //private readonly IAbpApplicationConfigurationAppService _abpApplicationConfigurationAppService;
        public IJsonSerializer JsonSerializer { get; }

        public LoginViewModel(
            IConfiguration configuration,
            IModuleManager moduleManager,
            //IAbpApplicationConfigurationAppService abpApplicationConfigurationAppService,
            ISettingsService settingsService,
            IJsonSerializer jsonSerializer
            )
        {
            _configuration = configuration;
            _moduleManager = moduleManager;
            Model = new LoginModel();
            //_abpApplicationConfigurationAppService = abpApplicationConfigurationAppService;
            _settingsService = settingsService;
            JsonSerializer = jsonSerializer;
            //Model.UserName = "admin";
            //Model.Password = "1q2w3E*";
        }

        [AsyncCommand]
        public async Task LoginAsync()
        {
            try
            {
                this.IsLoading = true;
                string username = Model.UserName;
                string password = Model.Password;
                string? server = _configuration["IdentityClients:Default:Authority"];
                string? clientId = _configuration["IdentityClients:Default:ClientId"];
                string? scope = _configuration["IdentityClients:Default:Scope"];
                string? clientSecret = _configuration["IdentityClients:Default:ClientSecret"]; 

                var client = new HttpClient();
                

                var config = new DiscoveryDocumentRequest() { Address = server, Policy = new DiscoveryPolicy() { RequireHttps = false } };
                var configuration = await client.GetDiscoveryDocumentAsync(config);
                if (configuration.IsError)
                {
                    throw new Exception(configuration.Error);
                }

                var passwordTokenRequest = new PasswordTokenRequest
                {
                    Address = configuration.TokenEndpoint,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    UserName = username,
                    Password = password,
                    Scope = scope,
                };
                //passwordTokenRequest.Headers.Add("__tenant", "Default");
                var tokenResponse = await client.RequestPasswordTokenAsync(passwordTokenRequest);

                if (tokenResponse.IsError)
                {
                    throw new Exception(tokenResponse.Error +"_" +tokenResponse.ErrorDescription);
                }


                _configuration["IdentityClients:Default:UserName"] = Model.UserName;
                _configuration["IdentityClients:Default:UserPassword"] = Model.Password;

                _configuration["IdentityClients:Default:AccessToken"] = tokenResponse.AccessToken;
                _configuration["IdentityClients:Default:TokenType"] = tokenResponse.TokenType;
                _configuration["IdentityClients:Default:RefreshToken"] = tokenResponse.RefreshToken;
                _configuration["IdentityClients:Default:ExpiresIn"] = tokenResponse.ExpiresIn.ToString();


                await GetAppConfigAsync();
                //ApplicationConfigurationRequestOptions options = new ApplicationConfigurationRequestOptions();
                //options.IncludeLocalizationResources = false;
                //ApplicationConfigurationDto result = await _abpApplicationConfigurationAppService.GetAsync(options);

                _moduleManager.Remove(RegionNames.MainWindow, AccountUIViewKeys.Login);
                _moduleManager.InjectOrNavigate(RegionNames.MainWindow, ThemeViewKeys.DefaultLayout);
                _moduleManager.InjectOrNavigate(RegionNames.MenuTreeRegion, ThemeViewKeys.Menu);
                _moduleManager.InjectOrNavigate(RegionNames.MainHeaderRegion, ThemeViewKeys.Header);
                _moduleManager.InjectOrNavigate(RegionNames.MainFooterRegion, ThemeViewKeys.Footer);
                //_moduleManager.InjectOrNavigate(RegionNames.MainContentRegion, ThemeViewKeys.Welcome);

            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task GetAppConfigAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string? url = _configuration["RemoteServices:Default:BaseUrl"];
                url = url + "api/abp/application-configuration?IncludeLocalizationResources=false";
                using (var request = new HttpRequestMessage(new HttpMethod("Get"), url))
                {
                    this.IsLoading = true;
                    var currentCulture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
                    request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                    request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(currentCulture));
                    request.SetBearerToken(_configuration["IdentityClients:Default:AccessToken"]);

                    var contentList = new List<string>();
                    //contentList.Add("scope=" + scope);
                    //contentList.Add("username=" + EntityModel.UserName);
                    //contentList.Add("password=" + EntityModel.Password);
                    //contentList.Add("client_id=" + clientId);
                    //contentList.Add("clientSecret=" + clientSecret);
                    request.Content = new StringContent(string.Join("&", contentList));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                    var response = await httpClient.SendAsync(request);
                    ;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        string res = await response.Content.ReadAsStringAsync();
                        ApplicationConfigurationDto? result = JsonSerializer.Deserialize<ApplicationConfigurationDto>(res);

                        if (result != null)
                        {
                            _settingsService.SetUserId(result.CurrentUser.Id);
                            _settingsService.SetUserName(result.CurrentUser.UserName);
                            _settingsService.SetName(result.CurrentUser.Name);
                            _settingsService.SetSurname(result.CurrentUser.SurName);
                            _settingsService.SetUserEmail(result.CurrentUser.Email);
                            _settingsService.SetPhoneNumber(result.CurrentUser.PhoneNumber);
                            ClientPermissions.GrantedPolicies = result.Auth.GrantedPolicies;
                        }
                        ;
                    }
                    else 
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        LoginBadResultModel? resultModel = JsonSerializer.Deserialize<LoginBadResultModel>(res);
                        if (resultModel != null)
                        {
                            Growl.ErrorGlobal(resultModel.error_description);
                        }
                    }
                }
            }
        }




        public class LoginSuccessResultModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string? access_token { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int expires_in { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? token_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? refresh_token { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? scope { get; set; }
        }

        public class LoginBadResultModel
        {
            /// <summary>
            /// LoginFailedResultModel
            /// </summary>
            public string? error { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string? error_description { get; set; }
        }

    }
}
