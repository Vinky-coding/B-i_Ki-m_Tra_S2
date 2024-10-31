using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BaiKTso2.Form1;
using System.Globalization;

namespace BaiKTso2
{
    public partial class Form1 : Form
    {
        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string Image { get; set; }

            public Product(string name, decimal price, int quantity, string image)
            {
                Name = name;
                Price = price;
                Quantity = quantity;
                Image = image;
            }
        }
        public class ShoppingCart
        {
            public List<Product> Items { get; private set; } = new List<Product>();

            public void AddProduct(Product product)
            {
                var existingProduct = Items.FirstOrDefault(p => p.Name == product.Name);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += product.Quantity;
                }
                else
                {
                    Items.Add(product);
                }
            }

            public void RemoveProduct(Product product)
            {
                Items.Remove(product);
            }

            public decimal CalculateTotal()
            {
                return Items.Sum(item => item.Price * item.Quantity);
            }

            public void Clear()
            {
                Items.Clear();
            }
        }

        public Form1()
        {
            InitializeComponent();
            LoadProductData();
            UpdateCartDisplay();
        }
        private void LoadProductData()
        {
            
            dataGridView1.Rows.Add("Img1", "Sản phẩm 1", 100000, 1);
            dataGridView1.Rows.Add("Img2", "Sản phẩm 2", 150000, 1);
            dataGridView1.Rows.Add("Img3", "Sản phẩm 3", 200000, 1);
            dataGridView1.Rows.Add("Img4", "Sản phẩm 4", 150000, 1);
            dataGridView1.Rows.Add("Img5", "Sản phẩm 5", 210000, 1);
        }


        private ShoppingCart shoppingCart = new ShoppingCart();
        private void UpdateCartDisplay()
        {
            dataGridView2.Rows.Clear();
            foreach (var item in shoppingCart.Items)
            {
                dataGridView2.Rows.Add(item.Image, item.Name, item.Price.ToString("N0") + " đ", item.Quantity);
            }
            textBox1.Text = $" {shoppingCart.CalculateTotal().ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"))} đ";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Xác nhận thanh toán đơn hàng?", "Thanh toán", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                shoppingCart.Clear();
                UpdateCartDisplay();
                MessageBox.Show("Thanh toán thành công!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string name = selectedRow.Cells["ProductName"].Value.ToString();
                decimal price = Convert.ToDecimal(selectedRow.Cells["ProductPrice"].Value);
                string image = selectedRow.Cells["ProductImage"].Value.ToString();

                Product selectedProduct = new Product(name, price, 1, image);
                shoppingCart.AddProduct(selectedProduct);
                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để thêm vào giỏ hàng.");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Columns.Add("ProductImage", "Ảnh");
            dataGridView2.Columns.Add("ProductName", "Tên sản phẩm");
            dataGridView2.Columns.Add("ProductPrice", "Giá");
            dataGridView2.Columns.Add("Quantity", "Số lượng");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView2.SelectedRows[0];
                string name = selectedRow.Cells["ProductName1"].Value.ToString();
                var productToRemove = shoppingCart.Items.FirstOrDefault(p => p.Name == name);

                if (productToRemove != null)
                {
                    shoppingCart.RemoveProduct(productToRemove);
                    UpdateCartDisplay();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa khỏi giỏ hàng.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
