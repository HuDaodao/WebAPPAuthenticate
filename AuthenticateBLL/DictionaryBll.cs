using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

using AuthenticateDAL;
using AuthenticateModel;

namespace AuthenticateBLL
{
    public class DictionaryBll
    {
        #region 数据字典主表
        /// <summary>
        /// 返回数据字典主表数据
        /// </summary>
        /// <returns>数据字典主表List</returns>
        public List<MainDictionary> GetAllDiction()
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.MainDictionary.ToList();
            }
        }

        /// <summary>
        /// 保存数据字典主表
        /// </summary>
        /// <param name="mainDic">数据字典主表实体</param>
        public void SaveMainDiction(MainDictionary mainDic)
        {
            using (AuthentContext db = new AuthentContext())
            {
                if (mainDic.Id == 0)
                {
                    db.MainDictionary.Add(mainDic);
                }
                else
                {
                    MainDictionary mainDiction = new MainDictionary();
                    mainDiction = db.MainDictionary.Where(m => m.Id == mainDic.Id).FirstOrDefault();
                    mainDiction.ChineseName = mainDic.ChineseName;
                    mainDiction.Description = mainDic.Description;
                    mainDiction.EnglishName = mainDic.EnglishName;
                    mainDiction.LastChangeTime = mainDic.LastChangeTime;
                    mainDiction.LastChangeUser = mainDic.LastChangeUser;
                    mainDiction.ReadOnly = mainDic.ReadOnly;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 通过id获取一条数据字典主表信息
        /// </summary>
        /// <param name="id">数据字典主表ID</param>
        /// <returns></returns>
        public MainDictionary GetMainDictioById(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.MainDictionary.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 删除一个数据字典主表信息
        /// </summary>
        /// <param name="id">数据字典主表ID</param>
        public void DeleteMain(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                MainDictionary mainDic = db.MainDictionary.Where(m=>m.Id==id).FirstOrDefault();
                db.MainDictionary.Remove(mainDic);
                db.SaveChanges();
            }
        }
        #endregion

        #region 数据字典项
        /// <summary>
        /// 获取数据字典项列表
        /// </summary>
        /// <param name="id">所属数据字典ID</param>
        /// <param name="pageStart">开始条数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="sortCol">排序字段</param>
        /// <param name="sortDir">排序方式</param>
        /// <param name="total">总共用户条数</param>
        /// <returns></returns>
        public List<Dictionary> GetPageDictions(int id,int pageStart,int pageSize,int sortCol,string sortDir,out int total)
        {
            using (AuthentContext db = new AuthentContext())
            {
                IQueryable<Dictionary> dictionaries=db.Dictionary.Where(d=>d.MainDictionaryId==id);
                total = dictionaries.Count();
                if (total != 0)
                {
                    if (sortCol == 2 && sortDir == "desc")
                    {
                        dictionaries = dictionaries.OrderByDescending(d => d.Order);
                    }
                    else
                    {
                        dictionaries = dictionaries.OrderBy(d => d.Order);
                    }
                    return dictionaries.Skip(pageStart).Take(pageSize).ToList();
                }
                return dictionaries.OrderBy(d=>d.Order).Skip(pageStart).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 通过id获取一条数据字典项
        /// </summary>
        /// <param name="id">数据字典主表ID</param>
        /// <returns></returns>
        public Dictionary GetDictioById(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                return db.Dictionary.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 保存数据字典项
        /// </summary>
        /// <param name="diction">数据字典主表实体</param>
        public void SaveDiction(Dictionary diction)
        {
            using (AuthentContext db = new AuthentContext())
            {
                if (diction.Id == 0)
                {
                    db.Dictionary.Add(diction);
                }
                else
                {
                    Dictionary dictionary = db.Dictionary.Where(d => d.Id == diction.Id).FirstOrDefault();
                    dictionary.ChineseName = diction.ChineseName;
                    dictionary.Description = diction.Description;
                    dictionary.EnglishName = diction.EnglishName;
                    dictionary.LastChangeTime = diction.LastChangeTime;
                    dictionary.LastChangeUser = diction.LastChangeUser;
                    dictionary.Order = diction.Order;
                    dictionary.Status = diction.Status;
                    dictionary.MainDictionaryId = diction.MainDictionaryId;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一个数据字典项
        /// </summary>
        /// <param name="id">数据字典主表ID</param>
        public void DeleteDic(int id)
        {
            using (AuthentContext db = new AuthentContext())
            {
                Dictionary dic = db.Dictionary.Where(m => m.Id == id).FirstOrDefault();
                db.Dictionary.Remove(dic);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
