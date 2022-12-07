using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkuluDersKaydi.Data;

namespace YazOkuluDersKaydi.Business
{
    public class DersIslemleri
    {
        private DbYazOkuluDersKaydıEntities2 Context;

        public DersIslemleri()
        {
            Context = new DbYazOkuluDersKaydıEntities2();
        }


        public bool DersEkle(TblDersler Ders, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";

            var SeciliDers = (from d in Context.TblDersler where d.DersId == Ders.DersId select d).FirstOrDefault();

            if (SeciliDers != null)
            {
                Mesaj = "Ders zaten ekli";

            }
            else
            {
                Context.TblDersler.Add(Ders);
                Context.SaveChanges();
                result = true;
            }


            return result;
        }


        public List<TblDersler> GetDersler()
        {

            return (from d in Context.TblDersler orderby d.DersAdi select d).ToList();

        }
        public TblDersler GetDersler(Guid DersGuid, out string Mesaj)
        {
            Mesaj = "";

            return (from d in Context.TblDersler where d.DersGuid == DersGuid select d).SingleOrDefault();

        }


        public List<TblDersler> GetDersler(int OgrenciId, out string Mesaj)
        {
            Mesaj = "";
            List<TblDersler> OgrencininDersListesi;

            OgrencininDersListesi = (from d in Context.TblDersler where d.OgrenciId == OgrenciId select d).ToList();


            return OgrencininDersListesi;

        }


        public bool Update(TblDersler Ders, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";

            try
            {
                if (Ders != null)
                {
                    var SeciliDers = (from d in Context.TblDersler where d.DersId == Ders.DersId select d).SingleOrDefault();
                    if (SeciliDers == null)
                    {
                        Mesaj = "Guncellenecek ders bulunamadı";
                    }
                    else
                    {
                        SeciliDers.DersAdi = Ders.DersAdi;
                        SeciliDers.AlanAdi = Ders.AlanAdi;
                        SeciliDers.DersGuid = Ders.DersGuid;
                        SeciliDers.Aktif = Ders.Aktif;
                        SeciliDers.DersUcreti = Ders.DersUcreti;
                        SeciliDers.Kontenjan = Ders.Kontenjan;
                        SeciliDers.OgrenciId = Ders.OgrenciId;
                        SeciliDers.OgretmenAdi = Ders.OgretmenAdi;
                        Context.SaveChanges();

                        result = true;
                    }


                }
            }
            catch (Exception ex)
            {

                Mesaj = ex.Message;
            }


            return result;
        }



        public bool Delete(TblDersler Ders, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";

            try
            {
                if (Ders != null)
                {
                    var SeciliDers = (from d in Context.TblDersler where d.DersId == Ders.DersId select d).SingleOrDefault();

                    if (SeciliDers == null)
                    {
                        Mesaj = "Silinecek ders bulunamadı";
                    }
                    else
                    {
                        Context.Database.ExecuteSqlCommand("delete from TblDersler where DersId ='" + Ders.DersId.ToString() + "'");
                        Context.SaveChanges();
                        result = true;
                        //GetDersler();

                    }

                }
            }
            catch (Exception ex)
            {

                Mesaj = ex.Message;
            }

            return result;
        }




    }
}
