using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailAuthorAboutComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke(string AuthorName, string AuthorDescription,string AuthorImageUrl)
        {
            ViewBag.AuthorName = AuthorName;
            ViewBag.AuthorDescription = AuthorDescription;
            ViewBag.AuthorImageUrl = AuthorImageUrl;
            return View();
        }
    }
}
