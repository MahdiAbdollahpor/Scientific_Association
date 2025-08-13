using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;

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




        [HttpGet]
        public IActionResult GetUserCardHtml(int id)
        {
            var user = _adminService.GetUserById(id);
            if (user == null) return NotFound();

            var expiryDate = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd");

            var htmlContent = $@"
    <!DOCTYPE html>
    <html lang='fa' dir='rtl'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>کارت شناسایی کاربر</title>
        <style>
            @font-face {{
            font-family: 'IRANSans';
            src: url('fonts/IRANSansWeb.ttf') format('truetype');
        }}
        
        body {{
            font-family: 'IRANSans', sans-serif;
            background-color: #e8f4f8;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            padding: 20px;
        }}
        
 
        
        .microbiology-card {{
            width: 360px;
            height: 230px;
            background: linear-gradient(to bottom right, #005b96, #003d66);
            border-radius: 12px;
            box-shadow: 0 8px 25px rgba(0, 91, 150, 0.3);
            padding: 20px;
            color: white;
            position: relative;
            overflow: hidden;
            border: 1px solid rgba(255, 255, 255, 0.2);
        }}
        
        .card-pattern {{
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-image: url('data:image/svg+xml;utf8,<svg xmlns=""http://www.w3.org/2000/svg"" width=""100"" height=""100"" viewBox=""0 0 100 100""><circle cx=""20"" cy=""20"" r=""2"" fill=""rgba(255,255,255,0.05)""/><circle cx=""50"" cy=""50"" r=""3"" fill=""rgba(255,255,255,0.05)""/><circle cx=""80"" cy=""30"" r=""1.5"" fill=""rgba(255,255,255,0.05)""/><circle cx=""30"" cy=""70"" r=""2.5"" fill=""rgba(255,255,255,0.05)""/><circle cx=""70"" cy=""80"" r=""1"" fill=""rgba(255,255,255,0.05)""/></svg>');
            opacity: 0.6;
        }}
        
        .card-header {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
            position: relative;
            z-index: 2;
        }}
        
        .logo {{
            font-weight: bold;
            font-size: 18px;
            display: flex;
            align-items: center;
        }}
        
        .logo-icon {{
            margin-left: 8px;
            font-size: 22px;
        }}
        
        .card-type {{
            background-color: rgba(255, 255, 255, 0.15);
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 12px;
            backdrop-filter: blur(5px);
        }}
        
        .card-body {{
            display: flex;
            position: relative;
            z-index: 2;
        }}
        
        .photo-container {{
            width: 90px;
            height: 120px;
            background-color: #f8f9fa;
            border-radius: 8px;
            display: flex;
            justify-content: center;
            align-items: center;
            margin-left: 15px;
            overflow: hidden;
            border: 2px solid rgba(255, 255, 255, 0.3);
        }}
        
        .photo-placeholder {{
            color: #666;
            font-size: 12px;
            text-align: center;
            padding: 10px;
        }}
        
        .member-info {{
            flex: 1;
        }}
        
        .info-row {{
            margin-bottom: 12px;
        }}
        
        .label {{
            font-size: 11px;
            opacity: 0.8;
            margin-bottom: 4px;
            color: #a8d0e6;
        }}
        
        .value {{
            font-size: 14px;
            font-weight: bold;
        }}
        
        .card-footer {{
            position: absolute;
            bottom: 15px;
            left: 20px;
            right: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 10px;
            opacity: 0.7;
            z-index: 2;
        }}
        
        .micro-icon {{
            position: absolute;
            right: -20px;
            bottom: -20px;
            font-size: 120px;
            opacity: 0.08;
            z-index: 1;
        }}
        
        .bacteria-shape {{
            position: absolute;
            width: 80px;
            height: 80px;
            border-radius: 50%;
            background: rgba(168, 208, 230, 0.1);
            top: -30px;
            left: -30px;
        }}
        
        .dna-pattern {{
            position: absolute;
            width: 100%;
            height: 20px;
            bottom: 40px;
            left: 0;
            background: linear-gradient(90deg, 
                transparent 0%, 
                transparent 20%, 
                rgba(255,255,255,0.2) 21%, 
                rgba(255,255,255,0.2) 22%, 
                transparent 23%, 
                transparent 43%, 
                rgba(255,255,255,0.2) 44%, 
                rgba(255,255,255,0.2) 45%, 
                transparent 46%, 
                transparent 66%, 
                rgba(255,255,255,0.2) 67%, 
                rgba(255,255,255,0.2) 68%, 
                transparent 69%, 
                transparent 89%, 
                rgba(255,255,255,0.2) 90%, 
                rgba(255,255,255,0.2) 91%, 
                transparent 92%, 
                transparent 100%);
            opacity: 0.3;
        }}
        </style>
        <script>
            window.onload = function() {{
                window.print();
                setTimeout(function() {{
                    window.close();
                }}, 1000);
            }};
        </script>
    </head>
    <body>
        <div class='microbiology-card'>
            <div class='card-pattern'></div>
            <div class='bacteria-shape'></div>
            <div class='dna-pattern'></div>
            <div class='micro-icon'>🦠</div>
            
            <div class='card-header'>
                <div class='logo'>
                    <span>انجمن میکروبیولوژی</span>
                    <span class='logo-icon'>🧫</span>
                </div>
                <div class='card-type'>عضو رسمی</div>
            </div>
            
            <div class='card-body'>
                <div class='photo-container'>
                    <div class='photo-placeholder'>
                        تصویر 3×4
                    </div>
                </div>
                
                <div class='member-info'>
                    <div class='info-row'>
                        <div class='label'>نام و نام خانوادگی</div>
                        <div class='value'>{user.firstName} {user.lastName}</div>
                    </div>
                    
                    <div class='info-row'>
                        <div class='label'>کد دانشجویی/پرسنلی</div>
                        <div class='value'>{user.studentNumber}</div>
                    </div>
                    
                    <div class='info-row'>
                        <div class='label'>تاریخ عضویت</div>
                        <div class='value'>{user.CreateDate}</div>
                    </div>
                </div>
            </div>
            
            <div class='card-footer'>
                <div>این کارت مالکیت انحصاری عضو است</div>
                <div>اعتبار: {DateTime.Now.AddYears(2).ToString("yyyy/MM/dd")}</div>
            </div>
        </div>
    </body>
    </html>";

            return Content(htmlContent, "text/html");
        }







    }
}

