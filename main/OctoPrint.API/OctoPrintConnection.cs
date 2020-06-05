using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using OctoPrint.API.Models;

namespace OctoPrint.API
{
    public class OctoPrintConnection
    {
        private static readonly Version supportedAPIVersion = new Version(0, 1);
        private static readonly Version supportServerVersion = new Version(1, 4, 0);

        private string _baseURL = string.Empty;
        private string _accessToken = string.Empty;

        public OctoPrintConnection(string baseUrl, string apiToken)
        {
            _baseURL = baseUrl;
            _accessToken = apiToken;
        }

        #region Get

        public CurrentUserModel GetCurrentUser()
        {
            try
            {
                return CurrentUserModel.FromJson(HTMLGet($"{_baseURL}api/currentuser"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VersionModel GetVersion()
        {
            try
            {
                return VersionModel.FromJson(HTMLGet($"{_baseURL}api/version"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ConnectionModel GetConnectionDetails()
        {
            try
            {
                return ConnectionModel.FromJson(HTMLGet($"{_baseURL}api/connection"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public FilesModel GetFiles(bool rootOnly = false, string location = "", string fileName = "")
        {
            try
            {
                return FilesModel.FromJson(string.IsNullOrEmpty(location)
                ? HTMLGet(HTMLGet($"{_baseURL}api/files?recursive=" + rootOnly))
                : string.IsNullOrEmpty(fileName) ?
                    HTMLGet(HTMLGet($"{_baseURL}api/files/" + location + "?recursive=" + rootOnly)) :
                    HTMLGet(HTMLGet($"{_baseURL}api/files/" + location + "/" + fileName + "?recursive=" + rootOnly)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JobModel GetJob()
        {
            try
            {
                return JobModel.FromJson(HTMLGet($"{_baseURL}api/job"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LanguagesModel GetLanguages()
        {
            try
            {
                return LanguagesModel.FromJson(HTMLGet($"{_baseURL}api/languages"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterModel GetPrinter()
        {
            try
            {
                return PrinterModel.FromJson(HTMLGet($"{_baseURL}api/printer"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterToolModel GetPrinterTool()
        {
            try
            {
                return PrinterToolModel.FromJson(HTMLGet($"{_baseURL}api/printer/tool"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterBedModel GetPrinterBed()
        {
            try
            {
                return PrinterBedModel.FromJson(HTMLGet($"{_baseURL}api/printer/bed"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterChamberModel GetPrinterChamber()
        {
            try
            {
                return PrinterChamberModel.FromJson(HTMLGet($"{_baseURL}api/printer/chamber"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterSdModel GetPrinterSD()
        {
            try
            {
                return PrinterSdModel.FromJson(HTMLGet($"{_baseURL}api/printer/sd"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterCustomCommandModel GetCustomCommands()
        {
            try
            {
                return PrinterCustomCommandModel.FromJson(HTMLGet($"{_baseURL}api/printer/command/custom"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PrinterProfilesModel GetPrinterProfiles(string id = "")
        {
            try
            {
                return PrinterProfilesModel.FromJson(string.IsNullOrEmpty(id)
                ? HTMLGet($"{_baseURL}api/printerprofiles")
                : HTMLGet($"{_baseURL}api/printerprofiles/" + id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SettingsModel GetSettings()
        {
            try
            {
                return SettingsModel.FromJson(HTMLGet($"{_baseURL}api/settings"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SettingsTemplatesModel GetSettingsTemplates()
        {
            try
            {
                return SettingsTemplatesModel.FromJson(HTMLGet($"{_baseURL}api/settings/templates"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SlicingModel GetSlicing(string profile = "")
        {
            try
            {
                return SlicingModel.FromJson(string.IsNullOrEmpty(profile)
                ? HTMLGet($"{_baseURL}api/slicing")
                : HTMLGet($"{_baseURL}api/slicing/" + profile + "/profiles"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public SpecificSlicingModel GetSpecificSlicing(string slicer, string key)
        //{
        //    return SpecificSlicingModel.FromJson(HTMLGet($"{_baseURL}api/settings/" + slicer + "/profiles/" + key));
        //}

        public SystemCommandsModel GetSystemCommands()
        {
            try
            {
                return SystemCommandsModel.FromJson(HTMLGet($"{_baseURL}api/system/commands"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemCommandsModel GetSystemCommandFromSource(string source)
        {
            try
            {
                return SystemCommandsModel.FromJson(HTMLGet($"{_baseURL}api/system/commands/" + source));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TimelapseModel GetTimelapse()
        {
            try
            {
                return TimelapseModel.FromJson(HTMLGet($"{_baseURL}api/timelapse"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PermissionsModel GetPermissions()
        {
            try
            {
                return PermissionsModel.FromJson(HTMLGet($"{_baseURL}api/access/permissions"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AccessGroupsModel GetAccessGroups()
        {
            try
            {
                return AccessGroupsModel.FromJson(HTMLGet($"{_baseURL}api/access/groups"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AccessGroupModel GetAccessGroup(string key)
        {
            try
            {
                return AccessGroupModel.FromJson(HTMLGet($"{_baseURL}api/access/groups/" + key));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UsersModel GetUsers()
        {
            try
            {
                return UsersModel.FromJson(HTMLGet($"{_baseURL}api/access/users"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserModel GetUser(string username)
        {
            try
            {
                return UserModel.FromJson(HTMLGet($"{_baseURL}api/access/users/" + username));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserSettingsModel GetUserSettings(string username)
        {
            try
            {
                return UserSettingsModel.FromJson(HTMLGet($"{_baseURL}api/access/users/" + username + "/settings"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Get

        #region BaseMethods

        /// <summary>
        /// Add headers to Get
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string HTMLGet(string path)
        {
            Dictionary<string, string> headers = null;

            if (_accessToken != string.Empty)
            {
                headers = new Dictionary<string, string>
                {
                    { "X-Api-Key", _accessToken }
                };
            }

            return Get(path, headers);
        }

        private string HTMLGetFile(string path, string targetPath)
        {
            Dictionary<string, string> headers = null;

            if (_accessToken != string.Empty)
            {
                headers = new Dictionary<string, string>
                {
                    { "X-Api-Key", _accessToken }
                };
            }

            return GetFile(path, targetPath, headers);
        }

        /// <summary>
        /// Add headers to Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="objectData"></param>
        /// <returns></returns>
        private string HTMLPost<T>(string path, T objectData)
        {
            if (JSONSerializer.Serialize<T>(objectData, out string jsonString))
            {
                Dictionary<string, string> headers = null;
                if (_accessToken != string.Empty)
                {
                    headers = new Dictionary<string, string>
                    {
                        { "X-Api-Key", _accessToken }
                    };
                }
                return Post(path, jsonString, headers);
            }
            return string.Empty;
        }

        /// <summary>
        /// Send get request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private string Get(string url, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = "OctoPrintC#Client/1.0";
                if (headers != null)
                {
                    foreach (string name in headers.Keys)
                    {
                        httpWebRequest.Headers.Add(name, headers[name]);
                    }
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                else
                    result = ex.ToString();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Send get request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private string GetFile(string url, string targetPath, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = "OctoPrintC#Client/1.0";
                if (headers != null)
                {
                    foreach (string name in headers.Keys)
                    {
                        httpWebRequest.Headers.Add(name, headers[name]);
                    }
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (!Directory.Exists(Directory.GetParent(targetPath).FullName))
                {
                    Directory.CreateDirectory(Directory.GetParent(targetPath).FullName);
                }

                using (Stream output = File.OpenWrite(targetPath))
                {
                    using (Stream input = httpResponse.GetResponseStream())
                    {
                        input.CopyTo(output);
                    }
                }

                return "";
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                else
                    result = ex.ToString();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Send post request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private string Post(string url, string json, Dictionary<string, string> headers = null)
        {
            string result = string.Empty;
            HttpWebResponse httpResponse = null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.UserAgent = "OctoPrintC#Client/1.0";
                if (headers != null)
                {
                    foreach (string name in headers.Keys)
                    {
                        httpWebRequest.Headers.Add(name, headers[name]);
                    }
                }

                if (json != string.Empty)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            if (error != string.Empty)
                            {
                                return error;
                            }
                        }
                    }
                }
                return wex.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return result;
        }

        #endregion BaseMethods
    }
}