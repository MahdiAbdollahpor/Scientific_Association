using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;
using Grpc.Core;
using DinkToPdf.Contracts;
using DinkToPdf;
using Mono.TextTemplating;
using System.Drawing.Printing;
using System.Drawing;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using static System.Net.Mime.MediaTypeNames;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace ClientSide.Areas.AdminPanel.Controllers
{

    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]  
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IConverter _pdfConverter;

        public AccountController(IAdminService adminService ,IConverter pdfConverter)
        {
            _adminService = adminService;
            _pdfConverter = pdfConverter;
        }

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var userList = _adminService.GetAllUserForAdmin(pageId, search);
            return View(userList);
        }
        [HttpGet]
        public  IActionResult EditUser(int id)
        {

            

            var users =  _adminService.GetUserById(id);
            if (users == null) return NotFound();

            return PartialView("_EditUserModal", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(ListUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "اطلاعات وارد شده معتبر نیست"
                });
            }

            var result = _adminService.UpdateUser(model);

            return Json(new
            {
                success = result,
                message = result ? "اطلاعات کاربر با موفقیت ویرایش شد" : "خطا در ویرایش کاربر"
            });
        }




        public IActionResult PrintUserCard(int id)
        {
            var user = _adminService.GetUserById(id);
            if (user == null)
                return NotFound();

            var htmlContent = $@"
        <!DOCTYPE html>
        <html dir='rtl'>
        <head>
            <meta charset='UTF-8'>
        </head>
        <body style='font-family:B Nazanin,Tahoma,sans-serif;background-color:#f8f9fa;padding:40px 20px;margin:0;'>
            <div style='background:white;border-radius:12px;box-shadow:0 4px 20px rgba(0,0,0,0.08);max-width:480px;margin:0 auto;overflow:hidden;'>
                <!-- هدر کارت -->
                <div style='background:linear-gradient(135deg,#2563eb 0%,#1e40af 100%);color:white;padding:24px;text-align:center;margin-bottom:16px;'>
                    <h2 style='margin:0;font-size:22px;font-weight:700;'>کارت شناسایی کاربر</h2>
                </div>

                <!-- محتوای کارت -->
                <div style='padding:0 24px 24px;'>
                    <div style='display:flex;justify-content:space-between;align-items:center;padding:14px 0;border-bottom:1px solid #f1f1f1;margin:0 0 8px 0;'>
                        <span style='color:#4b5563;font-weight:600;margin-left:12px;font-size:15px;'>نام کامل:</span>
                        <span style='color:#1f2937;font-weight:500;font-size:15px;text-align:left;direction:ltr;'>{user.firstName} {user.lastName}</span>
                    </div>

                    <div style='display:flex;justify-content:space-between;align-items:center;padding:14px 0;border-bottom:1px solid #f1f1f1;margin:0 0 8px 0;'>
                        <span style='color:#4b5563;font-weight:600;margin-left:12px;font-size:15px;'>کد ملی:</span>
                        <span style='color:#1f2937;font-weight:500;font-size:15px;text-align:left;direction:rtl;unicode-bidi:embed;'>{user.nationalCode}</span>
                    </div>

                    <div style='display:flex;justify-content:space-between;align-items:center;padding:14px 0;border-bottom:1px solid #f1f1f1;margin:0 0 8px 0;'>
                        <span style='color:#4b5563;font-weight:600;margin-left:12px;font-size:15px;'>شماره دانشجویی:</span>
                        <span style='color:#1f2937;font-weight:500;font-size:15px;text-align:left;direction:rtl;unicode-bidi:embed;'>{user.studentNumber}</span>
                    </div>

                    <div style='display:flex;justify-content:space-between;align-items:center;padding:14px 0;margin:0;'>
                        <span style='color:#4b5563;font-weight:600;margin-left:12px;font-size:15px;'>تاریخ ثبت‌نام:</span>
                        <span style='color:#1f2937;font-weight:500;font-size:15px;text-align:left;direction:rtl;unicode-bidi:embed;'>{user.CreateDate}</span>
                    </div>
                </div>
            </div>
        </body>
        </html>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                PaperSize = DinkToPdf.PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
            },
                Objects = {
                new ObjectSettings()
                {
                    HtmlContent = htmlContent,
                    WebSettings = {
                        DefaultEncoding = "utf-8",
                        EnableJavascript = false, // غیرفعال کردن JavaScript
                        LoadImages = true
                    }
                }
            }
            };

            var pdfBytes = _pdfConverter.Convert(doc);
            return File(pdfBytes, "application/pdf", $"UserCard_{user.firstName}_{user.lastName}.pdf");
        }










    }
}

