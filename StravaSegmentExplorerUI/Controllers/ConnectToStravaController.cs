using Microsoft.AspNetCore.Http;
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
        private string _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConnectToStravaController(StravaAPIDataAccess stravaAPIAccess, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _stravaAPIAccess = stravaAPIAccess;

            var configSettings = SQLConfigurationService.GetConfigurationSettings(configuration);

            _identityDbConnection = configSettings.IdentityDbConnection;
            _stravaDbConnection = configSettings.StravaDbConnection;
            _clientId = configSettings.ClientId;
            _clientSecret = configSettings.ClientSecret;

            _httpContextAccessor = httpContextAccessor;

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
            _userId = _httpContextAccessor.HttpContext.Session.GetString("UserIdKey");

            bool isStravaConnected = IsConnectedToStrava();
            ViewData["IsStravaConnected"] = isStravaConnected;

            return View();
        }

        [HttpGet]
        public IActionResult MilestoneFilterResult()
        {

            return RedirectToAction("Milestone");
        }

        [HttpPost]
        public async Task<IActionResult> MilestoneFilterResult(MilestoneModel milestoneFilter)
        {
            _userId = _httpContextAccessor.HttpContext.Session.GetString("UserIdKey");
            var _milestone = milestoneFilter.Milestone;
            var _sport = milestoneFilter.Sport;

            try
            {
                List<ActivityModel> output = new List<ActivityModel>();

                var activityList = await _stravaAPIAccess.GetListOfActivities(_userId);
                milestoneFilter.ResultIsReady = true;

                output = MilestoneResultsController.GetMilestoneResultsActivityList(activityList, _milestone, _sport);

                if (output != null)
                {
                    milestoneFilter.Results.AddRange(output);
                }

            }
            catch (Exception e)
            {

                throw new Exception($"{e.Message}, {e.StackTrace}");
            }

            return View(milestoneFilter);
        }

        public ActionResult Segments()
        {
            _userId = _httpContextAccessor.HttpContext.Session.GetString("UserIdKey");

            bool isStravaConnected = IsConnectedToStrava();
            ViewData["IsStravaConnected"] = isStravaConnected;

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

            _userId = _httpContextAccessor.HttpContext.Session.GetString("UserIdKey");

            bool isConnected = await _stravaAPIAccess.ConnectToStravaApi(Request, _userId);

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
            _userId = _httpContextAccessor.HttpContext.Session.GetString("UserIdKey");

            SQLOperations sqlOperations = new SQLOperations();

            if (sqlOperations.IsConnectedToStrava(_userId, _identityDbConnection))
            {
                var userData = sqlOperations.GetCurrentUserData(_userId, _stravaDbConnection);

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

