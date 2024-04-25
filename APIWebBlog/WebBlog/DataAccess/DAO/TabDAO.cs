using BusinessObject.Data;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class TabDAO
    {
        private static TabDAO instance = null!;
        private static readonly object iLock = new object();
        public TabDAO() { }

        public static TabDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new TabDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Tab> GetAll()
        {
            List<Tab> tabs;
            try
            {
                var context = new MyDbContext();
                tabs = context.Tabs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tabs;
        }
        public Tab GetTabsById(int id)
        {
            Tab tab = null!;
            try
            {
                var context = new MyDbContext();
                tab = context.Tabs.FirstOrDefault(b => b.tab_id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tab;
        }
        public void Add(Tab tab)
        {
            try
            {
                var context = new MyDbContext();
                context.Tabs.Add(tab);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Tab tab)
        {
            try
            {
                var id = GetTabsById(tab.tab_id);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Entry<Tab>(tab).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Tab does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int tabid)
        {
            try
            {
                var id = GetTabsById(tabid);
                if (id != null)
                {
                    var context = new MyDbContext();
                    context.Tabs.Remove(id);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Tab does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
