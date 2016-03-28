using OpenCRM.DB.DomainObjects;
using OpenCRM.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OpenCRM.Web.API
{
    public class AuthorizedUserAttribute : AuthorizeAttribute
    {
        private readonly IRepository<User> _userRepo;
        
        public AuthorizedUserAttribute()
        {
            _userRepo = ObjectFactory.Container.GetInstance<IRepository<User>>();            
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var token = GetToken();
            var user = _userRepo.Get(x => x.AuthToken == token);
            
            if (user != null)
                return true;

            return false;
        }
        
        private string GetToken()
        {
            try
            {
                if (HttpContext.Current.Request.Headers["Authorization"].Count() > 0)
                {
                    string token = HttpContext.Current.Request.Headers["Authorization"].ToString();
                    return token;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}