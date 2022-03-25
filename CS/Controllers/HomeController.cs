using Microsoft.AspNetCore.Mvc;

namespace WebDocumentViewerCustomizeParametersPanel.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public IActionResult Viewer() {
            return View();
        }
    }
}
