using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NguyenThanhDat_BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenThanhDat_BigSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BigSchoolContext context = new BigSchoolContext();
            var upCommingCourse = context.Course.Where(p => p.DateTime > DateTime.Now).OrderBy(p => p.DateTime).ToList();

            //Lấy User Login hiện tại
            var userID = User.Identity.GetUserId();
            foreach(Course i in upCommingCourse)
            {
                //Tìm Name của User từ Lectureid
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager
                    <ApplicationUserManager>().FindById(i.LecturerId);
                i.LecturerId = user.Name;

                //Lấy danh sách khoá học
                if(userID != null)
                {
                    i.isLogin = true;
                    //Kiểm tra User đó chưa tham gia khoá học 
                    Attendance find = context.Attendance.FirstOrDefault(p => p.CourseId == i.Id && p.Attendee == userID);
                    if(find == null)
                    {
                        i.isShowGoing = true;
                    }

                    //Kiểm tra User đã theo dõi giảng viên của khoá học?
                    Following findFollow = context.Following.FirstOrDefault(p => p.FollowerId == userID 
                    && p.FolloweeId == i.LecturerId);
                    if(findFollow == null)
                    {
                        i.isShowFollow = true;
                    }    
                }    
            }    
            return View(upCommingCourse);
        }

    }
}