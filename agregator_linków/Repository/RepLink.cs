using agregator_linków.Data;
using agregator_linków.Models;
using agregator_linków.Viewmodel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Repository
{
    public class RepLink
    {
        private Dbcontext db;
        private int NumberOfViews = 5;
        public int MaxPageUser { get; private set; }
        public RepLink(Dbcontext dbcontext)
        {
            db = dbcontext;
        }


        public void EditLink(ViewEditLink viewLink)
        {
            try
            {
                Link link = db.link.FirstOrDefault(p => p.id == viewLink.id);
                link.title = viewLink.title;
                link.url = viewLink.url;
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException(ex.Message);
                throw argEx;
            }
          

        }


        public void RemoveLink(int id)
        {
            try
            {
                Link link = db.link.FirstOrDefault(p => p.id == id);
                db.link.Remove(link);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException(ex.Message);
                throw argEx;
            }
        }

        public void AddLink(ViewAddLink link)
        {
            Link newLink = ViewLinkMapToLink(link);
            db.link.Add(newLink);
            db.SaveChanges();

        }

        public ViewEditLink LinkMapToViewEditLink(Link link)
        {
            ViewEditLink viewlink = new ViewEditLink();
            viewlink.url = link.url;
            viewlink.id = link.id;
            viewlink.title = link.title;
            return viewlink;
        }


        public ViewEditLink GetViewLink(int id)
        {
           try
            {
                Link link = db.link.FirstOrDefault(p => p.id == id);
                ViewEditLink viewlink = LinkMapToViewEditLink(link);
                return viewlink;
            }
            catch (Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException(ex.Message);
                 throw argEx;
            }
        }

        public Link ViewLinkMapToLink(ViewAddLink link)
        {
            Link newLink = new Link();
            newLink.ownerid= link.user.id;
            newLink.owner = link.user;
            newLink.time = link.time;
            newLink.like = link.like;
            newLink.title = link.title;
            newLink.url = link.url;

            return newLink;
        }

        public List<ViewIndexLink> MapViewIndexLink(int userId,int page)
        {
         
            int startindex = (page - 1) * NumberOfViews;
            int endindex = startindex + NumberOfViews;
            int lenght = db.link.Count();
            if (endindex > lenght)
                endindex = lenght;
            List<ViewIndexLink> list = new List<ViewIndexLink>();
            var links = db.link.Include(p => p.owner).OrderByDescending(p=>p.like).ToList();
            for (int i= startindex;i<endindex;i++)
            {
                ViewIndexLink viewIndexLink = new ViewIndexLink();
                viewIndexLink.like = links[i].like;
                viewIndexLink.time = links[i].time;
                viewIndexLink.url = links[i].url;
                viewIndexLink.user = links[i].owner.eamil;
                viewIndexLink.title = links[i].title;
                viewIndexLink.id = links[i].id;

                if(links[i].ownerid != userId)
                {
                if(IsLike(links[i].id,userId))
                    viewIndexLink.buttonlike = "Unlike";
                else
                    viewIndexLink.buttonlike = "Like"; 
                }
                else
                viewIndexLink.buttonlike = null;
                list.Add(viewIndexLink);
            }
           
            return list;
        }

        public List<ViewIndexLink> MapViewIndexLink(int page)
        {

  
            int startindex = (page - 1) * NumberOfViews;
            int endindex = startindex + NumberOfViews;
            int lenght = db.link.Count();
            if (endindex > lenght)
                endindex = lenght;
            List<ViewIndexLink> list = new List<ViewIndexLink>();
            var links = db.link.Include(p => p.owner).OrderByDescending(p => p.like).ToList();
            for (int i = startindex; i < endindex; i++)
            {
                ViewIndexLink viewIndexLink = new ViewIndexLink();
                viewIndexLink.like = links[i].like;
                viewIndexLink.time = links[i].time;
                viewIndexLink.url = links[i].url;
                viewIndexLink.user = links[i].owner.eamil;
                viewIndexLink.title = links[i].title;
                viewIndexLink.buttonlike = null;
                viewIndexLink.id = links[i].id;
                list.Add(viewIndexLink);

            }
            return list;
        }

        public List<ViewIndexLink> ownerLinks(string email,int page)
        {

            int startindex = (page - 1) * NumberOfViews;
            int endindex = startindex + NumberOfViews;
            var links = db.link.Include(p => p.owner).Where(p => p.owner.eamil == email).OrderByDescending(p => p.like).ToList();
            int lenght = links.Count();
            double maxpage = Math.Ceiling((double)lenght / NumberOfViews);
            MaxPageUser = (int)maxpage;
            if (endindex > lenght)
                endindex = lenght;
        
            List<ViewIndexLink> list = new List<ViewIndexLink>();
         
            for (int i = startindex; i < endindex; i++)
            {     
                    ViewIndexLink viewIndexLink = new ViewIndexLink();
                    viewIndexLink.like = links[i].like;
                    viewIndexLink.time = links[i].time;
                    viewIndexLink.url = links[i].url;
                    viewIndexLink.user = links[i].owner.eamil;
                    viewIndexLink.title = links[i].title;
                    viewIndexLink.buttonlike = null;
                    viewIndexLink.id = links[i].id;
                    list.Add(viewIndexLink);
                
            }
            return list;
        }
        
        public  bool Like(int linkId, int userId)
        {
            
            try
            {
                UserLike userLike = db.userLikes.Include(p => p.link)
                    .FirstOrDefault(p => p.userid == userId && p.linkid == linkId);
                userLike.link.like--;

                db.userLikes.Remove(userLike);
                db.SaveChanges();
                return false;
            }
            catch
            {
                UserLike userLike = new UserLike();
                userLike.linkid = linkId;
                userLike.userid = userId;
                db.userLikes.Add(userLike);
                var link = db.link.FirstOrDefault(p => p.id == linkId);
                link.like++;
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }

        }

        public bool IsLike(int linkId , int userId )
        {
            return db.userLikes.Any(p => p.userid == userId && p.linkid == linkId);
        }


        public List<Link> GetLink()
        {
            return db.link.ToList();
        }

        public int CheckPage(int page)
        {
            double lenght= db.link.Count();
            double maxPage = Math.Ceiling(lenght / NumberOfViews);
            if (page <= 0)
                page = 1;
            else if (page > lenght)
                page = 1;
            else if (maxPage < page)
                page = (int)maxPage;



            return page;
        }

        public int GetMaxValuePage()
        {
            double lenght = db.link.Count();
            double maxpage = Math.Ceiling(lenght / NumberOfViews);
            return (int)maxpage;
        }


    }
}
