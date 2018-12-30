using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcCrud.Models;

namespace MvcCrud.Controllers
{
    public class ProductController : Controller
    {
        string connection = @"Data Source= AJ_BHAGAT-PC; Initial Catalog= MvcCrudDB; Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM PRODUCT", sqlcon);
                sqlDa.Fill(dtblProduct);
            }
                return View(dtblProduct);
        }

        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                sqlcon.Open();
                string query = "INSERT INTO Product VALUES(@ProductName,@Price,@Cost)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlcmd.Parameters.AddWithValue("@Cost", productModel.Count);
                sqlcmd.ExecuteNonQuery();
            }
               return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
