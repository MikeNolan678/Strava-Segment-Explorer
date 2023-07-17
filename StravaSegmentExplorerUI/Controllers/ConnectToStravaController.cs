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

        public ActionResult Milestone()
        {
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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _milestone = milestoneFilter.Milestone;
            var _sport = milestoneFilter.Sport;

            try
            {
                List<ActivityModel> output = new List<ActivityModel>();

                var activityList = await _stravaAPIAccess.GetListOfActivities(currentUserId);
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
            bool isStravaConnected = IsConnectedToStrava();
            ViewData["IsStravaConnected"] = isStravaConnected;

            return View();
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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isConnected = await _stravaAPIAccess.ConnectToStravaApi(Request, currentUserId);

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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            SQLOperations sqlOperations = new SQLOperations();

            if (sqlOperations.IsConnectedToStrava(currentUserId, _identityDbConnection))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

