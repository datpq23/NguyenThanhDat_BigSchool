using Microsoft.AspNet.Identity;
using NguyenThanhDat_BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NguyenThanhDat_BigSchool.Controllers
{
    public class FollowingsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            //User login là người theo dõi, follow.FolloweeId là người được theo dõi
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return BadRequest("Please login first!");
            }
            if (userID == follow.FolloweeId)
            {
                return BadRequest("Can't follow myself!");
            }
            BigSchoolContext context = new BigSchoolContext();
            //Kiểm tra xem mã userID đã được theo dõi chưa
            Following find = context.Followings.FirstOrDefault(p => p.FollowerId == userID
            && p.FolloweeId == follow.FolloweeId);
            if (find != null)
            {
                //return BadRequest("The already following exists!");
                context.Followings.Remove(context.Followings.SingleOrDefault
                    (p => p.FollowerId == userID && p.FolloweeId == follow.FolloweeId));
                context.SaveChanges();
                return Ok("cancel");
            }
            //Set object follow
            follow.FollowerId = userID;
            context.Followings.Add(follow);
            context.SaveChanges();
            return Ok();
        }
    }
}
