using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }
        private void SearchProducts(string key)
        {
            dgwProducts.DataSource = _productDal.GetByName(key);
        }
        private Product SearchProducts(int id)
        {
            var result = _productDal.GetById(id);
            return result;
        }
        private void SearchByMinPriceProducts(int price)
        {
            dgwProducts.DataSource = _productDal.GetByUnitPrice(price);
        }
        private void SearchByMaxPriceProducts(int min, int max)
        {
            dgwProducts.DataSource = _productDal.GetByUnitPrice(min, max);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = txtName.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                StockAmount = Convert.ToInt32(txtStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Added with EntityFramework");
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpdateName.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            txtUpdatePrice.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            txtUpdateStock.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value.ToString()),
                Name = txtUpdateName.Text,
                UnitPrice = Convert.ToDecimal(txtUpdatePrice.Text),
                StockAmount = Convert.ToInt32(txtUpdateStock.Text)
            });
            LoadProducts();
            MessageBox.Show("Updated!");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
            });
            LoadProducts();
            MessageBox.Show("Deleted!");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(txtSearch.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SearchByMinPriceProducts(Convert.ToInt32(trackBar1.Value));
            lblMinPrice.Text = trackBar1.Value.ToString();

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            SearchByMaxPriceProducts(Convert.ToInt32(trackBar1.Value), Convert.ToInt32(trackBar2.Value));
            lblMaxPrice.Text = trackBar2.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = SearchProducts(Convert.ToInt32(txtById.Text));
            lblSearchId.Text = s.Name;
        }
    }
}
