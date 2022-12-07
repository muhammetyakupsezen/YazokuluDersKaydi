using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazOkuluDersKaydi.Business;
using YazOkuluDersKaydi.Data;

namespace YazokuluDersKaydi.Controllers
{
    public class OgrenciIslemleriController : Controller
    {
        private OgrenciIslemleri ogrenciIslemleri;
        private DersIslemleri dersIslemleri;
        // GET: OgrenciIslemleri

        public OgrenciIslemleriController()
        {
            ogrenciIslemleri = new OgrenciIslemleri();
        }
        public ActionResult Index()
        {
            ViewBag.OgrenciListesi = ogrenciIslemleri.GetOgrenciListesi();


            return View(ViewBag);
        }


        public ActionResult OgrenciDetay(int OgrenciId)
        {
            ViewBag.OgrenciDetay = ogrenciIslemleri.GetOgrenciListesi(OgrenciId);
            return View(ViewBag);
        }


        public ActionResult Add()
        {

            if (Request.HttpMethod == "POST")
            {

                string Mesaj = "";
                string ogrenciAdi;
                string ogrenciSoyAdi;


                ogrenciAdi = Request.Form["TxtOgrenciAdi"].ToString();
                ogrenciSoyAdi = Request.Form["TxtOgrenciSoyAdi"].ToString();

                if (ogrenciAdi == null)
                {
                    ViewBag.Message = "öğrenci adı boş bırakılamaz";
                }
                else if (ogrenciSoyAdi == null)
                {
                    ViewBag.Message = "öğrenci soyadı boş bırakılamaz";
                }
                else
                {

                    TblOgrenci tblOgrenci = new TblOgrenci();
                    tblOgrenci.OgrenciAdi = ogrenciAdi;
                    tblOgrenci.OgrenciGuid = Guid.NewGuid();
                    tblOgrenci.OgrenciSoyadi = ogrenciSoyAdi;
                    tblOgrenci.Aktif = true;


                    bool basarili = ogrenciIslemleri.OgrenciEkle(tblOgrenci, out Mesaj);

                    if (!basarili)
                    {
                        ViewBag.Message = "Ogrenci Kaydi başarısız";
                    }
                    else
                    {
                        Response.Redirect("~/OgrenciIslemleri", true);
                    }
                }



            }


            return View();

        }


        public ActionResult Delete()
        {
            ViewBag.OgrenciListesi = ogrenciIslemleri.GetOgrenciListesi();


            if (Request.HttpMethod =="POST")
            {
                string Mesaj = "";
                string Ogrenci = Request.Form["CmbOgrenciAdi"].ToString();

                TblOgrenci tblOgrenci = new TblOgrenci();
                tblOgrenci.OgrenciId = int.Parse(Ogrenci);

                if (Ogrenci != null)
                {
                    bool basarili = ogrenciIslemleri.Delete(tblOgrenci, out Mesaj);

                    if (!basarili)
                    {
                        ViewBag.Message = Mesaj;
                    }
                    else
                    {
                        Response.Redirect("~/OgrenciIslemleri", true);
                    }
                }

            }


            return View(ViewBag);
        }



        public ActionResult Update()
        {
            ViewBag.OgrenciListesi = ogrenciIslemleri.GetOgrenciListesi();

            if (Request.HttpMethod == "POST")
            {
               string OgrenciId =  Request.Form["CmbOgrenciAdi"].ToString();
                string OgrenciAdi = Request.Form["TxtYeniOgrenciAdi"].ToString();
                string OgrenciSoyadi = Request.Form["TxtYeniOgrenciSoyadi"].ToString();

                string Mesaj = "";


                if (OgrenciId != null)
                {
                    TblOgrenci tblOgrenci = new TblOgrenci();
                    tblOgrenci.OgrenciId = Int32.Parse(OgrenciId);
                    tblOgrenci.OgrenciAdi = OgrenciAdi;
                    tblOgrenci.OgrenciSoyadi = OgrenciSoyadi;
                    


                    bool basarili = ogrenciIslemleri.Update(tblOgrenci, out Mesaj);

                    if (!basarili)
                    {
                        ViewBag.Message = Mesaj;
                    }
                    else
                    {
                        Response.Redirect("~/OgrenciIslemleri", true);
                    }

                }

            }

            return View();
        }



        //string Ogrenci = Request.QueryString["CmbOgrenci"];
        //string Mesaj = "";


        //bool basarili = int.TryParse(Ogrenci, out OgrenciId);

        //        if (basarili)
        //        {
        //            ogrenciIslemleri.GetOgrenciListesi(OgrenciId, out Mesaj);
        //            Response.Redirect("~/OgrenciIslemleri/OgrenciDetay?OgrenciId=" + OgrenciId.ToString());
        //        }

    }
}