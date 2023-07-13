using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using StravaSegmentExplorerDataAccess.API;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerDataAccess.SQLServer;
using System.Security.Claims;

namespace StravaSegmentExplorerUI.Controllers
{
    public class ConnectToStravaController : Controller
    {
        private readonly StravaAPIDataAccess _stravaAPIAccess;

        public ConnectToStravaController(StravaAPIDataAccess stravaAPIAccess)
        {
            _stravaAPIAccess = stravaAPIAccess;
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

        public ActionResult ConnectionError()
        {
            return View();
        }

        public ActionResult ConnectionSuccess()
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
    }
}

