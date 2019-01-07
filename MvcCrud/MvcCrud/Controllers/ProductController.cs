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

        [HttpGet]
        public ActionResult Create()
        {
                return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connection))
                {
                    sqlcon.Open();
                    string query = "INSERT INTO Product VALUES(@ProductName,@Price,@Cost)";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                    sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                    sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                    sqlcmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM PRODUCT where ProductID=@ProductID", sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtblProduct);             
            }
            if(dtblProduct.Rows.Count==1)
            {
                productModel.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connection))
                {
                    sqlcon.Open();
                    string query = "UPDATE Product SET ProductName=@ProductName, Price=@Price,Count=@Count WHERE ProductID=@ProductID";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                    sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                    sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                    sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                    sqlcmd.ExecuteNonQuery();
                }

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
