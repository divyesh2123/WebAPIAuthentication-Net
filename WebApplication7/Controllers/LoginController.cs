using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class LoginController : ApiController
    {
        [Route("UserLogin")]
        [HttpPost]
        public ResponseVM UserLogin(LoginVM objVM)
        {
            var data = new ApiUser();
            using (OauthApiEntities oauthApiEntities = new OauthApiEntities())
            {
                data = oauthApiEntities.ApiUsers.Where(y => y.UserName == objVM.UserName && y.UserPasswd == objVM.Password).FirstOrDefault();


            }
            
            if (data == null)
                return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
            //if (objlst.Status == 2)
            //    return new ResponseVM { Status = "Invalid", Message = "Already Logged In." };
            
            else
                return new ResponseVM { Status = "Success", Message = TokenManager.GenerateToken(objVM.UserName) };
        }
        
    }
}
