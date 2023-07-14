﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using StravaSegmentExplorerDataAccess.API;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerDataAccess.SQLServer;
using StravaSegmentExplorerUI.Models;
using System.Security.Claims;

namespace StravaSegmentExplorerUI.Controllers
{
    public class ConnectToStravaController : Controller
    {
        private readonly StravaAPIDataAccess _stravaAPIAccess;
        private readonly string _identityDbConnection;
        private readonly string _stravaDbConnection;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly bool _isConnectedToStrava;
        private readonly string _userId;
        public const string _userID = "";

        public ConnectToStravaController(StravaAPIDataAccess stravaAPIAccess, IConfiguration configuration)
        {
            _stravaAPIAccess = stravaAPIAccess;

            var configSettings = SQLConfigurationService.GetConfigurationSettings(configuration);

            _identityDbConnection = configSettings.IdentityDbConnection;
            _stravaDbConnection = configSettings.StravaDbConnection;
            _clientId = configSettings.ClientId;
            _clientSecret = configSettings.ClientSecret;

            //bool isConnectedToStrava = IsConnectedToStrava();

            //_isConnectedToStrava = isConnectedToStrava;

        }

        // GET: ConnectToStravaController
        public ActionResult Index()
        {
 

            return View();
        }

        // GET: ConnectToStravaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConnectToStravaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConnectToStravaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConnectToStravaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConnectToStravaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConnectToStravaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult Milestone()
        {
            bool isStravaConnected = _isConnectedToStrava;
            //ViewData["IsStravaConnected"] = isStravaConnected;

            return View();
        }

        public ActionResult Segments()
        {
            bool isStravaConnected = _isConnectedToStrava;
            //ViewData["IsStravaConnected"] = isStravaConnected;

            return View();
        }


        // POST: ConnectToStravaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Authorize()
        {
            var redirectUri = Url.Action("ConnectToStrava", "ConnectToStrava", null, Request.Scheme);
            string encodedRedirectUri = Uri.EscapeDataString(redirectUri);

            string fullEncodedRedirectUri = _stravaAPIAccess.AuthorizeWithStrava(encodedRedirectUri);

            return Redirect(fullEncodedRedirectUri);
        }

        public async Task<ActionResult> ConnectToStrava()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isConnected = await _stravaAPIAccess.ConnectToStravaApi(Request, userId);

            try
            {
                if (isConnected)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("ConnectionError");
                }
            }
            catch (Exception)
            {

                return View("ConnectionError");
            }
        }

        public AppUserModel UserInfo { get; set; }

        public bool IsConnectedToStrava()
        {
            string userId = _userID;

            SQLOperations sqlOperations = new SQLOperations();

            if (sqlOperations.IsConnectedToStrava(userId, _identityDbConnection))
            {
                var userData = sqlOperations.GetCurrentUserData(userId, _stravaDbConnection);

                UserInfo = userData;

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

