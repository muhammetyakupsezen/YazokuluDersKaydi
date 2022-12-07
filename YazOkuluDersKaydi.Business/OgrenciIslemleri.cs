using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkuluDersKaydi.Data;

namespace YazOkuluDersKaydi.Business
{
    public class OgrenciIslemleri
    {
        private DbYazOkuluDersKaydıEntities2 Context;

        public OgrenciIslemleri()
        {
            Context = new DbYazOkuluDersKaydıEntities2();
        }


        public bool OgrenciEkle(TblOgrenci Ogrenci, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";


            try
            {
                if (Ogrenci != null)
                {
                    var SecilenOgrenci = (from d in Context.TblOgrenci where d.OgrenciId == Ogrenci.OgrenciId select d).SingleOrDefault();


                    if (SecilenOgrenci != null)
                    {
                        Mesaj = "Ogrenci zaten kayıtli ";
                    }
                    else
                    {
                        Context.TblOgrenci.Add(Ogrenci);
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



        public List<TblOgrenci> GetOgrenciListesi()
        {
            List<TblOgrenci> Liste;

            Liste = (from d in Context.TblOgrenci orderby d.OgrenciAdi select d).ToList();


            return Liste;
        }

        public TblOgrenci GetOgrenciListesi(int OgrenciId)
        {
            
            var SeciliOgrenci = (from d in Context.TblOgrenci where d.OgrenciId == OgrenciId select d).FirstOrDefault();

         
            return SeciliOgrenci;
        }



        public bool Update(TblOgrenci Ogrenci, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";

            try
            {

                if (Ogrenci != null)
                {
                    var SeciliOgrenci = (from d in Context.TblOgrenci where d.OgrenciId == Ogrenci.OgrenciId select d).FirstOrDefault();

                    if (SeciliOgrenci == null)
                    {
                        Mesaj = "Guncellenecek ogrenci bulunamadı";
                    }
                    else
                    {
                        SeciliOgrenci.OgrenciAdi = Ogrenci.OgrenciAdi;
                        SeciliOgrenci.OgrenciSoyadi = Ogrenci.OgrenciSoyadi;
                        SeciliOgrenci.OgrenciNumarasi = Ogrenci.OgrenciNumarasi;
                        SeciliOgrenci.Aktif = Ogrenci.Aktif;
                        SeciliOgrenci.KayitTarihi = Ogrenci.KayitTarihi;

                        result = true;

                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }








            return result;
        }




        public bool Delete(TblOgrenci Ogrenci, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";

            var SeciliOgrenci = (from d in Context.TblOgrenci where d.OgrenciId == Ogrenci.OgrenciId select d).FirstOrDefault();

            if (SeciliOgrenci == null)
            {
                Mesaj = "Silinecek Ogrenci bulunamadı";
            }
            else
            {
               // Context.TblOgrenci.Remove(Ogrenci);
                Context.Database.ExecuteSqlCommand("delete from TblOgrenci where OgrenciId='" + Ogrenci.OgrenciId.ToString() + "'");
                Context.SaveChanges();
             //   GetOgrenciListesi();
                result = true;
            }


            return result;
        }



    }
}
