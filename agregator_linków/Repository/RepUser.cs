using agregator_linków.Data;
using agregator_linków.Models;
using agregator_linków.Viewmodel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace agregator_linków.Repository
{
    public class RepUser
    {
        private Dbcontext db;
       public RepUser(Dbcontext dbcontext)
        {
            db = dbcontext;
        }

        const string sessionName = "user";

     public bool AddUser(User user)
        {
            if (!db.users.Any(p => p.eamil == user.eamil))
            {
                db.users.Add(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CheckLogin(User user)
        {
            try
            {
                var dbUser = db.users.FirstOrDefault(p => p.eamil == user.eamil);
                var haskpasswd = Haskpassword(user.password, dbUser.salt);

                if (haskpasswd == dbUser.password)
                {                
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException(ex.Message);
                throw argEx;
            }
           
        }

        public bool CheckEmail(string Email)
        {

            return db.users.Any(p => p.eamil == Email);
        }


        public   string GenerateSalt()
        {
          
          return Crypto.GenerateSalt();
        }


        public int? GetID(string eamil)
        {
            try
            {
                var user = db.users.FirstOrDefault(p => p.eamil == eamil);
                return user.id;
            }
            catch
            {
                return null;
            }
        }


        public  string Haskpassword(string password, string salt)
            {           
            var pass = password + salt;
            return Crypto.Hash(pass);
            }


        public User GetUser(string email)
        {
            try
            {
                return db.users.FirstOrDefault(p => p.eamil == email);
            }
            catch(Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException(ex.Message);
                throw argEx;
            }

        }

        public User MapRegisterToUser(ViewRegister register)
        {
            User user= new User();
            user.eamil = register.email;
            user.password = register.password;
            return user;
        }

        public User MapViewUserToUser(ViewLogin viewUser)
        {
            User user = new User();
            user.eamil = viewUser.login;
            user.password = viewUser.password;
            return user;
        }

    }
}
