using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpUsingORM.Models;

namespace CSharpUsingORM.Controllers
{
  [HandleError]
  public class HomeController : Controller
  {

    public ActionResult Index(int? page)
    {
      // en: get a list of news for the specified page
      // ru: получем при помощи метода GetList список сущеностей News
      // ru: с разбивкой по страницам
      var news = News.GetList(10, page.GetValueOrDefault(), null, new { id_news = "DESC" });
      // en: following line of code is only requesting the required fields 
      // var news = News.GetList(10, page.GetValueOrDefault(), new string[] { "id_news", "title", "description", "date_created" }, new { id_news = "DESC" });

      // en: pass a list of news to view
      // ru: передаем список новостей в представление
      return View(news);
    }

    public ActionResult EditNews(int? id)
    {
      // en: if there is an identifier, the news will be opened for editing
      // ru: если есть идентификатор, то новость будет открыта для редактирования
      News n = null;
      if (id.HasValue)
      {
        n = new News(id.Value);
        // en: if identifier in the news instance is zero,
        // ru: если идентификатор в экземпляре новости равен нулю,
        if (n.IdNews <= 0)
        {
          // en: means the news was not found in the database
          // ru: значит новость не была найдена в базе данных,
          // ru: аннулируем объект, чтобы показать ошибку на странице
          n = null;
        }
      }
      // en: identifier is empty and will creating a new news
      // ru: если идентификатора нет, то будет создана новая новость
      else
      {
        n = new News();
      }

      // en: pass news to view
      // ru: передаем новость в представление
      return View(n);
    }
    [HttpPost]
    public ActionResult EditNews(int? id, News model)
    {
      if (ModelState.IsValid)
      {
        model.Save();
        return RedirectToAction("Index");
      }

      // en: pass news to view
      // ru: передаем новость в представление
      return View(model);
    }

    public ActionResult ShowNews(int id)
    {
      // en: get news from the database of the specified ID
      // ru: получаем новость по указанному идентификатору
      News n = new News(id);
      // en: if identifier in the news instance is zero,
      // ru: если идентификатор в экземпляре новости равен нулю,
      if (n.IdNews <= 0)
      {
        // en: means the news was not found in the database
        // ru: значит новость не была найдена в базе данных,
        // ru: аннулируем объект, чтобы показать ошибку на странице
        n = null;
      }
      // en: pass news to view
      // ru: передаем новость в представление
      return View(n);
    }

    public ActionResult DeleteNews(int id)
    {
      // en: get news from the database of the specified ID
      // ru: получаем новость по указанному идентификатору
      News n = new News(id);
      // en: if identifier in the news instance is zero,
      // ru: если идентификатор в экземпляре новости равен нулю,
      if (n.IdNews <= 0)
      {
        // en: means the news was not found in the database
        // ru: значит новость не была найдена в базе данных
        return Content("News not found.", "text/plain");
      }
      // en: delete news
      // ru: удаляем новость
      n.Delete();

      // en: returns the user to the list of news
      // ru: возвращаем пользователя к списку новостей
      return RedirectToAction("Index");
    }

    public ActionResult About()
    {
      return View();
    }
  
  }
}
