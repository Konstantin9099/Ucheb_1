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
    public partial class ChangeWarehouse : Form
    {
        public ChangeWarehouse()
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

        // Изменение данных склада.
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, чтобы были заполнены поля ввода/вывода данных.
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show(
                    "Внесите необходимые изменения.",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Изменить данные склада?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(Sklad.Change1);
                    string query = "UPDATE warehouse SET warehouse_adress='" + textBox1.Text + "', warehouse_owner='" + textBox2.Text + "' WHERE  warehouse_id='" + id + "'; ";
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
                    MessageBox.Show("Данные склада изменены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Назад.
        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse();
            warehouse.Owner = this;
            this.Hide();
            warehouse.Show();
        }

        private void ChangeWarehouse_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Запоелнение полей textBox данными, переданными с формы Warehouse.
        private void ChangeWarehouse_Load(object sender, EventArgs e)
        {
            textBox1.Text = Sklad.Change2;
            textBox2.Text = Sklad.Change3;
        }
    }
}
