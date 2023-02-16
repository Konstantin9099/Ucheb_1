using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ucheb_1
{
    public partial class AddItems : Form
    {
        public AddItems()
        {
            InitializeComponent();
        }

        //Функция, позволяющая отправить команду на сервер БД для оптимизации кода.
        public void Action(string query)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                cmDB.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
            }
        }



        // Добавить.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "" || textBox3.Text == null || textBox3.Text == "" || comboBox1.Text.Equals("") || comboBox2.Text.Equals(""))
            {
                MessageBox.Show(
                    "Должны быть заполнены все поля ввода.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Добавить новый товар?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "INSERT INTO items (item_name, item_desc, item_cat, item_warehouse, item_amount) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', (select cat_id from category where cat_name ='" + comboBox1.Text + "'), (select warehouse_id from warehouse where warehouse_adress='" + comboBox2.Text + "'), '" + textBox3.Text + "'); ";
                    MySqlConnection conn = DBUtils.GetDBConnection();
                    MySqlCommand cmDB = new MySqlCommand(query, conn);
                    try
                    {
                        conn.Open();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка!" + Environment.NewLine + ex.Message);
                    }
                    Action(query);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.ResetText();
                    comboBox1.SelectedIndex = -1;
                    comboBox2.ResetText();
                    comboBox2.SelectedIndex = -1;
                    MessageBox.Show("Новый товар добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Назад.
        private void button2_Click(object sender, EventArgs e)
        {
            Items items = new Items();
            items.Owner = this;
            this.Hide();
            items.Show();
        }

        private void AddItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Выгрузка выпадающих списков категорий товаров и складов в элементы comboBox из БД.
        private void AddItems_Load(object sender, EventArgs e)
        {
            string query = "SELECT category.cat_name FROM category ORDER BY cat_name;";
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["cat_name"].ToString());
            }
            reader.Close();

            string query1 = "SELECT warehouse.warehouse_adress FROM warehouse ORDER BY warehouse_adress;";
            MySqlConnection conn1 = DBUtils.GetDBConnection();
            conn1.Open();
            MySqlCommand command1 = new MySqlCommand(query1, conn1);
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox2.Items.Add(reader1["warehouse_adress"].ToString());
            }
            reader1.Close();
        }
    }
}
