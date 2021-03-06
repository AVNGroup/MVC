﻿using System;
using System.Web.Mvc;
using System.Threading;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;
using WebApplication3.ConnectedAzure;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.Globalization;
using System.Web;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // Parse the connection string and return a reference to the storage account
        // public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));
        //  CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        [HttpGet]
        public ActionResult Index(/*string language*/)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Login, string Password) {
            //HttpContext.Response.Cookies["id"].Value = Login;

            if (await RegistrationLogin.IsLoginAndPasswordCorrect(ConnectedAzureServises.tableClient, "IdentityTable", Login, Password))
            {
                HttpContext.Response.Cookies["Login"].Value = Login;
                HttpContext.Response.Cookies["Password"].Value = Password;
                return Redirect("/MyPage/Index");
            }
            else
            {
                return Redirect("/Home/NotSuccessEnter");
            }
        }

        /*public ActionResult Change(String LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return View("Index");
        }*/

        [HttpGet]
        public ActionResult SuccessEnter () {
            return View();
        }

        public ActionResult NotSuccessEnter() {
            return View();
        }

        [HttpPost]
        public ActionResult SuccessEnter(string action) {
            if (action == "a") {
                return Redirect("/Home/ON_viev");
            }
            else {
                return Redirect("/Home/SuccessEnter");
            }
        }
        public ViewResult Index_ru()
        {
            return View();
        }
        public ViewResult Index_ua()
        {
            return View();
        }
        public ViewResult personal_account()
        {
            return View();
        }
        public ViewResult personal_account_ru()
        {
            return View();
        }
        public ViewResult personal_account_ua()
        {
            return View();
        }

        //The DownloadToStream method was used to download the contents of a blob as a text string
        public ActionResult downloadBlobs()
        {

            // Retrieve storage account from connection string.

            // Create the blob client.
            CloudBlobClient blobClient = ConnectedAzureServises.storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Retrieve reference to a blob named "myblob.txt"
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("myblob.txt");

            string text;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return View();
        }

    }
}
